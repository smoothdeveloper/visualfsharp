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

let canonicalizeFilename filename = FileSystem.GetFullPathSafe filename

let getAst filename = filename |> File.ReadAllText |> parseSource

[<Test>]
let ``test1.fsx: verify parsed #load directives``() =
   let ast = getAst (getTestFilename "test1.fsx")
   let directives = getIncludeAndLoadDirectives ast
   
   let expectedMatches =
       [
       Some (FileInfo(getTestFilename "includes/a.fs").FullName)
       Some (FileInfo(getTestFilename "includes/b.fs").FullName)
       Some (FileInfo(getTestFilename "includes/b.fs").FullName)
       ]
   
   let results =
       directives
       |> Seq.map (function
          | Load(ExistingFile(filename), _) -> Some ((new FileInfo(filename)).FullName)
          | _ -> None
       )
       |> Seq.filter (Option.isSome)
       |> Seq.toList
   
   Assert.AreEqual(expectedMatches, results)

[<Test>]
let ``test1.fsx: verify parsed position lookup of individual #load directives``() =
    let ast = getAst (getTestFilename "test1.fsx")
    
    let expectations = [
      (mkPos 1 1,       Some (FileInfo(getTestFilename "includes/a.fs").FullName))
      (mkPos 1 5,       Some (FileInfo(getTestFilename "includes/a.fs").FullName))
      (mkPos 2 1,       Some (FileInfo(getTestFilename "includes/b.fs").FullName))
      (mkPos 2 5,       Some (FileInfo(getTestFilename "includes/b.fs").FullName))
      (mkPos 3 1000,    None)
      (mkPos 4 5,       Some (FileInfo(getTestFilename "includes/b.fs").FullName))
    ]
    
    let results =
        expectations
        |> Seq.map fst
        |> Seq.map (fun pos -> 
           let result = getHashLoadDirectiveResolvedPathAtPosition pos ast
           match result with
           | None      -> pos, None
           | Some path -> pos, Some (canonicalizeFilename path)
        )
        |> Seq.toList
    
    Assert.AreEqual(expectations, results)

[<Test(*;Ignore("this doesn't work for now")*)>]
let ``single #load multiple files``() =
    let ast = getAst (getTestFilename "single.load.multiple.files.fsx")

    let expectations = [
        (mkPos 1 1  , None)
        (mkPos 1 108, None)
        (mkPos 2 1  , None)
        (mkPos 2 2  , None)
        (mkPos 2 3  , Some (FileInfo(getTestFilename "test1.fsx")))
        (mkPos 2 13 , Some (FileInfo(getTestFilename "test1.fsx")))
        (mkPos 2 14 , Some (FileInfo(getTestFilename "test1.fsx")))
        (mkPos 2 15 , Some (FileInfo(getTestFilename "includes/a.fs")))
        (mkPos 2 29 , Some (FileInfo(getTestFilename "includes/a.fs")))
        (mkPos 2 30 , Some (FileInfo(getTestFilename "includes/a.fs")))
        (mkPos 2 31 , Some (FileInfo(getTestFilename "includes/b.fs")))
        (mkPos 2 46 , Some (FileInfo(getTestFilename "includes/b.fs")))
        (mkPos 2 108, None)
        (mkPos 3 0  , None)
        // todo: continue?
    ]
    
    let results =
        expectations
        |> Seq.map fst
        |> Seq.map (fun pos -> 
           let result = getHashLoadDirectiveResolvedPathAtPosition pos ast
           match result with
           | None      -> pos, None
           | Some path -> pos, Some (canonicalizeFilename path)
        )
        |> Seq.toList
    
    Assert.AreEqual(expectations, results) 
