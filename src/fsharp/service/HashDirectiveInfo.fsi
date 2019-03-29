// Copyright (c) Microsoft Corporation.  All Rights Reserved.  See License.txt in the project root for license information.

namespace FSharp.Compiler.SourceCodeServices

module HashDirectiveInfo =
    open FSharp.Compiler.Range
    open FSharp.Compiler.Ast

    /// IncludeDirective (#I) contains the pointed directory
    type IncludeDirective =
        | ResolvedDirectory of string
    
    // todo: make this embed precise location / reuse ast stuff
    type CodeStringLiteral = string

    type LoadFile =
        | Existing of CodeStringLiteral 
        | Unresolvable of CodeStringLiteral

    type LoadToken = CodeStringLiteral

    type LoadDirective = { token: LoadToken ; files : LoadFile list }
        
    /// represents #I and #load directive information along with range
    [<NoComparison>]
    type Directive =
        | Include of IncludeDirective * range
        | Load of LoadDirective * range


    /// returns an array of LoadScriptResolutionEntries
    /// based on #I and #load directives
    val getIncludeAndLoadDirectives : ParsedInput -> Directive array

    /// returns Some (complete file name of a resolved #load directive at position) or None
    val getHashLoadDirectiveResolvedPathAtPosition : pos -> ParsedInput -> string option