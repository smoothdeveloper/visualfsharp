// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

/// Helper members to integrate PackageManagers into F# codebase
module internal Microsoft.FSharp.Compiler.PackageManager

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

let RegisteredPackageManagers = ["paket:"] // this should really be records with names and load script names and stuff

let resolve packageManagerPrefix implicitIncludeDir fileName m packageManagerTextLines =
    try
        if not (List.contains packageManagerPrefix RegisteredPackageManagers) then
            errorR(Error(FSComp.SR.packageManagerUnknown(packageManagerPrefix, String.Join(", ", RegisteredPackageManagers)),m))

        let referenceLoadingResult =
            ReferenceLoading.PaketHandler.Internals.ResolvePackages
                targetFramework
                GetCommandForTargetFramework
                (fun workDir -> GetPaketLoadScriptLocation workDir (Some targetFramework))
                AlterPackageManagementToolCommand
                (implicitIncludeDir, fileName, packageManagerTextLines)

        match referenceLoadingResult with 
        | ReferenceLoading.PaketHandler.ReferenceLoadingResult.PackageManagerNotFound (implicitIncludeDir, userProfile) ->
            errorR(Error(FSComp.SR.packageManagerNotFound(implicitIncludeDir, userProfile),m))
            None
        | ReferenceLoading.PaketHandler.ReferenceLoadingResult.PackageResolutionFailed (toolPath, workingDir, msg) ->
            errorR(Error(FSComp.SR.packageResolutionFailed(toolPath, workingDir, Environment.NewLine, msg),m))
            None
        | ReferenceLoading.PaketHandler.ReferenceLoadingResult.Solved(loadScript,additionalIncludeFolders) -> 
            Some(additionalIncludeFolders,loadScript,File.ReadAllText(loadScript))
    with e ->
        errorRecovery e m
        None