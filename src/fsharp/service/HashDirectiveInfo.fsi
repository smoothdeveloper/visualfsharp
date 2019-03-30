// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace FSharp.Compiler.SourceCodeServices

module HashDirectiveInfo =
    open FSharp.Compiler.Range
    open FSharp.Compiler.Ast

    /// Include directive (#I)
    type IncludeDirective =
        | ResolvedDirectory of directory: string
    
    /// Load directive (#load)
    type LoadDirective =
        | ExistingFile of filename: string
        | UnresolvableFile of filename: string * previousIncludes : string array
        
    /// Directive information
    [<NoComparison>]
    type Directive =
        | Include of IncludeDirective * range
        | Load of LoadDirective * range

    /// returns an array of LoadScriptResolutionEntries
    /// based on #I and #load directives
    val getIncludeAndLoadDirectives : ParsedInput -> Directive array

    /// returns Some (complete file name of a resolved #load directive at position) or None
    val getHashLoadDirectiveResolvedPathAtPosition : pos -> ParsedInput -> string option