// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

/// Helper members to integrate PackageManagers into F# codebase
module internal Microsoft.FSharp.Compiler.PackageManager

open Microsoft.FSharp.Compiler.Range

val RegisteredPackageManagers : string list
val resolve : string -> string -> string  -> range -> string list -> (string list * string * string) option