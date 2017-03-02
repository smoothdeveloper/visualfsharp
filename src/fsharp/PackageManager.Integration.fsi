// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

/// Helper members to integrate PackageManagers into F# codebase
module internal Microsoft.FSharp.Compiler.PackageManagerIntegration

open Microsoft.FSharp.Compiler.Range

type IPackageManagerProvider =
    inherit System.IDisposable
    abstract Name : string
    abstract ToolName: string
    abstract Key: string

val RegisteredPackageManagers : unit -> Map<string,IPackageManagerProvider>
val tryFindPackageManagerInPath : string -> IPackageManagerProvider option
val tryFindPackageManagerByKey : string -> IPackageManagerProvider option

val removePackageManagerKey : string -> string -> string

val resolve : IPackageManagerProvider -> string -> string  -> range -> string list -> (string list * string * string) option
