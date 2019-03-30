// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace FSharp.Compiler.SourceCodeServices

open FSharp.Compiler.AbstractIL.Internal.Library

module HashDirectiveInfo =
    open FSharp.Compiler.Range
    open FSharp.Compiler.Ast

    type IncludeDirective =
        | ResolvedDirectory of directory: string

    type LoadDirective =
        | ExistingFile of filename: string
        | UnresolvableFile of filename: string * previousIncludes : string array

    [<NoComparison>]
    type Directive =
        | Include of IncludeDirective * range
        | Load of LoadDirective * range

    // /!\ @dsyme note https://github.com/Microsoft/visualfsharp/pull/4122#issuecomment-430983774 /!\
    // This looks like a partial reimplementation of aspects of the resolution logic done by the main F# compiler code.
    // The normal approach to this would be to record the resolutions in from the type-checking/analysis phase and report those resolutions here, rather than reimplementing the resolution logic.
    // In this case it's not a big problem. But there's a bit of trend toward reimplementing core compiler logic under src/fsharp/service and of course in the long term that will be a maintenance problem.

    // todo: investigate where those "SynModuleDecl.HashDirective (ParsedHashDirective(...))" come from and attempt to attach range with each item

    /// returns an array of LoadScriptResolutionEntries
    /// based on #I and #load directives
    let getIncludeAndLoadDirectives ast =
        // the Load items are resolved using fallback resolution relying on previously parsed #I directives
        // (this behaviour is undocumented in F# but it seems to be how it works).
        // list of #I directives so far (populated while encountering those in order)
        let includesSoFar = ResizeArray<_>()
        let tryFindInPathsIncludedSoFar fileName =
            includesSoFar 
            |> Seq.tryPick (fun (ResolvedDirectory d) ->
                let filePath = System.IO.Path.Combine(d, fileName)
                if FileSystem.SafeExists filePath then
                  Some filePath
                else 
                  None
            )

        let getDirectoryOfFile = FileSystem.GetFullPathSafe >> FileSystem.GetDirectoryNameShim

        let makeRootedDirectoryIfNecessary baseDirectory directory =
            if not (FileSystem.IsPathRootedShim directory) then
                FileSystem.GetFullPathSafe (System.IO.Path.Combine(baseDirectory, directory))
            else
                directory

        let parseDirectives modules file = 
            // note: the returned range is for the whole directive, which may contain several directories, files;
            // those are not precisely located in the text source
            let baseDirectory = getDirectoryOfFile file
            modules
            |> List.map (fun (SynModuleOrNamespace (_, _, _, declarations, _, _, _, _)) -> declarations)
            |> List.concat
            |> List.map (function 
                | SynModuleDecl.HashDirective (ParsedHashDirective("I", directories, range),_) ->

                    let resolvedDirectories =
                        directories 
                        |> List.map (makeRootedDirectoryIfNecessary (getDirectoryOfFile file))
                        |> List.filter FileSystem.DirectoryExistsShim
                        |> List.map ResolvedDirectory
                              
                    [ for dir in resolvedDirectories do
                        // push list of included dirs so far
                        includesSoFar.Add dir 
                        yield Include (dir, range) ]

                | SynModuleDecl.HashDirective (ParsedHashDirective("load", files, range),_) ->
                    files
                    |> List.map (fun file ->
                        // absolute file paths, to existing file, easiest case
                        if FileSystem.IsPathRootedShim file && FileSystem.SafeExists file then
                              ExistingFile file
                        else
                            // I'm not sure if the order is correct, first checking relative to file containing the #load directive
                            // then checking for undocumented resolution using previously parsed #I directives
                            let fileRelativeToCurrentFile = System.IO.Path.Combine(baseDirectory, file)
                            if FileSystem.SafeExists fileRelativeToCurrentFile then ExistingFile fileRelativeToCurrentFile
                            else
                                // match file against first include which seemingly have it found
                                match tryFindInPathsIncludedSoFar file with
                                | None ->
                                    // can't load this file even using any of the #I directives...
                                    UnresolvableFile (file, [| for (ResolvedDirectory(dir)) in includesSoFar -> dir |])
                                | Some resolvedFile ->
                                    ExistingFile resolvedFile 
                    )
                    |> List.filter (function ExistingFile _ -> true | _ -> false)
                    |> List.map (fun file -> Load (file, range))
                | _ -> List.empty
            )
            |> List.concat
            |> List.toArray

        match ast with
        | ParsedInput.ImplFile (ParsedImplFileInput(fn,_,_,_,_,modules,_)) -> parseDirectives modules fn
        | _ -> [||]

    /// returns the Some (complete file name of a resolved #load directive at position) or None
    let getHashLoadDirectiveResolvedPathAtPosition (pos: pos) (ast: ParsedInput) : string option =
        getIncludeAndLoadDirectives ast
        |> Array.tryPick (
            function
            | Load (ExistingFile f,range)
                // check the line is within the range
                // (doesn't work when there are multiple files given to a single #load directive)
                when rangeContainsPos range pos
                    -> Some f
            | _     -> None
        )