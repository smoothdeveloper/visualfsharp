// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace FSharp.Compiler.SourceCodeServices

open FSharp.Compiler.AbstractIL.Internal.Library

module PathUtils = 
    open System.IO
    //[<Sealed>]
    //type Path =
    //    static member GetFullPathSafe path =
    //        try Path.GetFullPath path
    //        with _ -> path
    //
    //    static member GetFileNameSafe path =
    //        try Path.GetFileName path
    //        with _ -> path

    let (</>) a b = Path.Combine(a, b)

module HashDirectiveInfo =
    open System.IO
    open PathUtils
    open FSharp.Compiler.Range
    open FSharp.Compiler.Ast

    type IncludeDirective =
        | ResolvedDirectory of string

    type CodeStringLiteral = string

    type LoadFile =
        | Existing of CodeStringLiteral 
        | Unresolvable of CodeStringLiteral

    type LoadToken = CodeStringLiteral

    type LoadDirective = { token: LoadToken ; files : LoadFile list }
    
    [<NoComparison>]
    type Directive =
        | Include of IncludeDirective * range
        | Load of LoadDirective * range

    // notes: 
    // #I encountered in other loaded scripts may have an impact and this code may not resolve the same
    // /!\ @dsyme note https://github.com/Microsoft/visualfsharp/pull/4122#issuecomment-430983774 /!\
    // This looks like a partial reimplementation of aspects of the resolution logic done by the main F# compiler code.
    // The normal approach to this would be to record the resolutions in from the type-checking/analysis phase and report those resolutions here, rather than reimplementing the resolution logic.
    // In this case it's not a big problem. But there's a bit of trend toward reimplementing core compiler logic under src/fsharp/service and of course in the long term that will be a maintenance problem.
    
    /// returns an array of LoadScriptResolutionEntries
    /// based on #I and #load directives
    let getIncludeAndLoadDirectives ast =
        // the Load items are resolved using fallback resolution relying on previously parsed #I directives
        // (this behaviour is undocumented in F# but it seems to be how it works).

        // list of #I directives so far (populated while encountering those in order)
        let pushInclude, tryFindInPathsIncludedSoFar =
            let includesSoFar = ResizeArray<_>()
            
            includesSoFar.Add,
            fun fileName ->
                includesSoFar 
                |> Seq.tryPick (fun (ResolvedDirectory d) ->
                    let filePath = d </> fileName 
                    if FileSystem.SafeExists filePath then
                      Some filePath
                    else 
                      None
                )

        let getDirectoryOfFile = FileSystem.GetFullPathSafe >> FileSystem.GetDirectoryNameShim
        let makeRootedDirectoryIfNecessary baseDirectory directory =
            if not (FileSystem.IsPathRootedShim directory) then
                FileSystem.GetFullPathSafe (baseDirectory </> directory)
            else
                directory

        let parseDirectives modules file = [|
            let baseDirectory = getDirectoryOfFile file
            for (SynModuleOrNamespace (_, _, _, declarations, _, _, _, _)) in modules do
                for decl in declarations do
                    match decl with
                    | SynModuleDecl.HashDirective (ParsedHashDirective("I",[directory],range),_) ->
                        let directory = makeRootedDirectoryIfNecessary (getDirectoryOfFile file) directory

                        if FileSystem.DirectoryExistsShim directory then
                            let includeDirective = ResolvedDirectory(directory)
                            pushInclude includeDirective
                            yield Include (includeDirective, range)

                    | SynModuleDecl.HashDirective (ParsedHashDirective ("load",files,range),_) ->
                        for f in files do
                            if FileSystem.IsPathRootedShim f && FileSystem.SafeExists f then
                                // this is absolute reference to an existing script, easiest case
                                yield Load ({token = ""; files = [Existing f]}, range)
                            else
                                // I'm not sure if the order is correct, first checking relative to file containing the #load directive
                                // then checking for undocumented resolution using previously parsed #I directives
                                let fileRelativeToCurrentFile = baseDirectory </> f
                                if FileSystem.SafeExists fileRelativeToCurrentFile then
                                    // this is existing file relative to current file
                                    yield Load ({token = ""; files = [Existing fileRelativeToCurrentFile]}, range)
                                else
                                    // match file against first include which seemingly have it found
                                    match tryFindInPathsIncludedSoFar f with
                                    | None -> () // can't load this file even using any of the #I directives...
                                    | Some f -> yield Load ({token = ""; files = [Existing f]},range)
                    | _ -> ()
            |]

        match ast with
        | ParsedInput.ImplFile (ParsedImplFileInput(fn,_,_,_,_,modules,_)) -> parseDirectives modules fn
        | _ -> [||]

    /// returns the Some (complete file name of a resolved #load directive at position) or None
    let getHashLoadDirectiveResolvedPathAtPosition (pos: pos) (ast: ParsedInput) : string option =
        getIncludeAndLoadDirectives ast
        |> Array.tryPick (
            function
            | Load ({token = ""; files = [Existing f] }, range)
                // check the line is within the range
                // todo: doesn't work when there are multiple files given to a single #load directive
                when rangeContainsPos range pos
                    -> Some f
            | _     -> None
)