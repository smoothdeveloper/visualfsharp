// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

/// Helper members to integrate ReferenceLoading.PaketHandler into F# codebase
module internal Microsoft.FSharp.Compiler.ReferenceLoading.PaketHandler

open Microsoft.FSharp.Compiler.Range

val resolvePaket : string -> string  -> range -> string list -> (string list * string * string) option