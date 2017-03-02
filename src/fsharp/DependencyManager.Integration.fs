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


type IDependencyManagerProvider =
    inherit System.IDisposable
    abstract Name : string
    abstract ToolName: string
    abstract Key: string
    abstract ResolveDependencies : string * string seq * string * string * string seq -> string * string list

type PaketDependencyManager() =
    interface IDependencyManagerProvider with
        member __.Name = "Paket"
        member __.ToolName = "paket.exe"
        member __.Key = "paket"
        member __.ResolveDependencies(targetFramework:string, prioritizedSearchPaths, scriptDir: string, scriptName: string, packageManagerTextLines: string seq) = 
            ReferenceLoading.PaketHandler.ResolveDependencies(
                targetFramework,
                prioritizedSearchPaths,
                scriptDir,
                scriptName,
                packageManagerTextLines)

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
        let loadScript,additionalIncludeFolders =
            packageManager.ResolveDependencies(
                targetFramework,
                Seq.empty,
                implicitIncludeDir,
                fileName,
                packageManagerTextLines)

        Some(additionalIncludeFolders,loadScript,File.ReadAllText(loadScript))
    with e ->
        errorRecovery e m
        None