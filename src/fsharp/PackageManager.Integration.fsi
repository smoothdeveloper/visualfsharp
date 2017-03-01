// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

/// Helper members to integrate PackageManagers into F# codebase
module internal Microsoft.FSharp.Compiler.PackageManagerIntegration

open Microsoft.FSharp.Compiler.Range

type PackageManager = {
    Prefix: string
    ToolName: string
    Name:string }

val RegisteredPackageManagers : PackageManager list
val resolve : PackageManager -> string -> string  -> range -> string list -> (string list * string * string) option


