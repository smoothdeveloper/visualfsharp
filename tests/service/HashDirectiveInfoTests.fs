module Tests.Service.HashDirectiveInfoTests

open System.IO
open NUnit.Framework
open FSharp.Compiler.Range
open FSharp.Compiler.Service.Tests.Common
open FSharp.Compiler.SourceCodeServices.HashDirectiveInfo
open FSharp.Compiler.AbstractIL.Internal.Library // FileSystem shims

let (</>) a b = Path.Combine(a,b)

[<Literal>]
let dataFolderName = __SOURCE_DIRECTORY__ + "/data/"

let getTestFilename filename =
    dataFolderName </> "ParsedLoadDirectives" </> filename

let canonicalize filename = FileSystem.GetFullPathSafe filename

let getAst filename = 
  filename 
  |> getTestFilename
  |> File.ReadAllText
  |> parseSource

let getDirectives filename =
  filename 
  |> getAst 
  |> getIncludeAndLoadDirectives

let getLoadDirectiveForExistingFileNamesOrNone filename =
  filename
  |> getDirectives
  |> Array.choose (function | Load (loadDirective,range) -> Some (loadDirective,range) | _ -> None)
  |> Array.map (function | (ExistingFile filename,range) -> range, Some (canonicalize filename) | (_, range) -> range, None)

// checks #load resolves the same files, in same order, for the given cursor locations
let checkRangesAndLoadFilesAreMatching filename expected =
    let actuals = getLoadDirectiveForExistingFileNamesOrNone filename
    Assert.AreEqual (expected, actuals)  
  
// checks #load resolves the same existing or unresolved files, in same order; it doesn't check the ranges
let checkLoadFilesAreMatching filename expected =
    let actuals = getLoadDirectiveForExistingFileNamesOrNone filename |> Array.map snd
    let expected = expected |> Array.map (Option.map canonicalize)
    Assert.AreEqual (expected, actuals)  

[<Test>]
let ``test1.fsx: verify parsed #load directives``() =
    checkLoadFilesAreMatching 
        "test1.fsx"
        [| Some "includes/a.fs"
           Some "includes/b.fs"
           Some "includes/b.fs" |]
   

[<Test>]
let ``test1.fsx: verify parsed position lookup of individual #load directives``() =
    checkRangesAndLoadFilesAreMatching
        "test1.fsx"
        [| mkPos 1 1  , Some "includes/a.fs"
           mkPos 1 5  , Some "includes/a.fs"
           mkPos 2 1  , Some "includes/b.fs"
           mkPos 2 5  , Some "includes/b.fs"
           mkPos 3 108, None
           mkPos 4 5  , Some "includes/b.fs" |]

[<Test;Ignore("this doesn't work for now")>]
let ``single.load.multiple.files.fsx: can pinpoint exact file in a multiple files #load directive``() =
    checkRangesAndLoadFilesAreMatching
        "single.load.multiple.files.fsx"
        [| mkPos 1 1  , None 
           mkPos 1 108, None 
           mkPos 2 1  , None 
           mkPos 2 2  , None 
           mkPos 2 3  , Some "test1.fsx"
           mkPos 2 13 , Some "test1.fsx"
           mkPos 2 14 , Some "test1.fsx"
           mkPos 2 15 , Some "includes/a.fs"
           mkPos 2 29 , Some "includes/a.fs"
           mkPos 2 30 , Some "includes/a.fs"
           mkPos 2 31 , Some "includes/b.fs"
           mkPos 2 46 , Some "includes/b.fs"
           mkPos 2 108, None 
           mkPos 3 0  , None |]
           // todo: continue?
