// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

/// Helper members to integrate PackageManagers into F# codebase
module internal Microsoft.FSharp.Compiler.PackageManagerIntegration

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
    ReferenceLoading.PaketHandler.MakePackageManagerCommand "fsx" targetFramework

type PackageManager = {
    Prefix: string
    ToolName: string
    Name:string }

let RegisteredPackageManagers = lazy (
    [ { Prefix = "paket:"
        ToolName = "paket.exe"
        Name = "Paket" }] 
)

let resolve (packageManager:PackageManager) implicitIncludeDir fileName m packageManagerTextLines =
    try
        let registered = RegisteredPackageManagers.Force()
        if not (List.contains packageManager registered) then
            errorR(Error(FSComp.SR.packageManagerUnknown(packageManager.Name, String.Join(", ", registered |> List.map (fun pm -> pm.Name))),m))

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
        | ReferenceLoading.PaketHandler.ReferenceLoadingResult.PackageManagerNotFound (implicitIncludeDir, userProfile) ->
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