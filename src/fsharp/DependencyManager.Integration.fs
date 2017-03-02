// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

/// Helper members to integrate DependencyManagers into F# codebase
module internal Microsoft.FSharp.Compiler.DependencyManagerIntegration

open System
open System.IO
open Microsoft.FSharp.Compiler.ErrorLogger

// NOTE: this contains mostly members whose intents are :
// * to keep ReferenceLoading.PaketHandler usable outside of F# (so it can be used in scriptcs & others)
// * to minimize footprint of integration in fsi/CompileOps

/// hardcoded to load the "Main" group (implicit in paket)
let scriptName = "main.group.fsx"

/// hardcoded to net461 as we don't have fsi on netcore
let targetFramework = "net461"

/// used to alter package management tool command depending runtime context (if running Mono, prefix with "mono ").
let AlterPackageManagementToolCommand command =
    if Microsoft.FSharp.Compiler.AbstractIL.IL.runningOnMono 
    then "mono " + command
    else command

/// Resolves absolute load script location: something like
/// baseDir/.paket/load/scriptName
/// or
/// baseDir/.paket/load/frameworkDir/scriptName 
let GetPaketLoadScriptLocation baseDir optionalFrameworkDir =
    ReferenceLoading.PaketHandler.GetPaketLoadScriptLocation baseDir optionalFrameworkDir scriptName

let GetCommandForTargetFramework targetFramework =
    ReferenceLoading.PaketHandler.MakeDependencyManagerCommand "fsx" targetFramework

type IDependencyManagerProvider =
    inherit System.IDisposable
    abstract Name : string
    abstract ToolName: string
    abstract Key: string

type PaketDependencyManager() =
    interface IDependencyManagerProvider with
        member __.Name = "Paket"
        member __.ToolName = "paket.exe"
        member __.Key = "paket"

    interface System.IDisposable with
        member __.Dispose() = ()

let registeredDependencyManagers = lazy (
    [ new PaketDependencyManager() :> IDependencyManagerProvider ] // TODO: Load these
    |> List.map (fun pm -> pm.Key,pm)
    |> Map.ofList
)

let RegisteredDependencyManagers() = registeredDependencyManagers.Force()

let tryFindDependencyManagerInPath (path:string) : IDependencyManagerProvider option =
    match registeredDependencyManagers.Force() |> Seq.tryFind (fun kv -> path.StartsWith(kv.Value.Key + ":" )) with
    | None -> None
    | Some kv -> Some kv.Value

let removeDependencyManagerKey (packageManagerKey:string) (path:string) = path.Substring(packageManagerKey.Length + 1).Trim()

let tryFindDependencyManagerByKey (key:string) : IDependencyManagerProvider option =
    registeredDependencyManagers.Force() |> Map.tryFind key

let resolve (packageManager:IDependencyManagerProvider) implicitIncludeDir fileName m packageManagerTextLines =
    try
        let referenceLoadingResult =
            ReferenceLoading.PaketHandler.Internals.ResolvePackages
                targetFramework
                GetCommandForTargetFramework
                (fun workDir -> GetPaketLoadScriptLocation workDir (Some targetFramework))
                AlterPackageManagementToolCommand
                Seq.empty
                implicitIncludeDir
                fileName
                packageManagerTextLines

        match referenceLoadingResult with 
        | ReferenceLoading.PaketHandler.ReferenceLoadingResult.DependencyManagerNotFound (implicitIncludeDir, userProfile) ->
            errorR(Error(FSComp.SR.packageManagerNotFound(packageManager.ToolName,implicitIncludeDir, userProfile),m))
            None
        | ReferenceLoading.PaketHandler.ReferenceLoadingResult.PackageResolutionFailed (toolPath, workingDir, msg) ->
            errorR(Error(FSComp.SR.packageResolutionFailed(toolPath, workingDir, Environment.NewLine, msg),m))
            None
        | ReferenceLoading.PaketHandler.ReferenceLoadingResult.Solved(loadScript,additionalIncludeFolders) -> 
            Some(additionalIncludeFolders,loadScript,File.ReadAllText(loadScript))
    with e ->
        errorRecovery e m
        None