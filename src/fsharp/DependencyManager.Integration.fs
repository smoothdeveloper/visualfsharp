// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

/// Helper members to integrate DependencyManagers into F# codebase
module internal Microsoft.FSharp.Compiler.DependencyManagerIntegration

open System
open System.Reflection
open System.IO
open Microsoft.FSharp.Compiler.ErrorLogger

// NOTE: this contains mostly members whose intents are :
// * to keep ReferenceLoading.PaketHandler usable outside of F# (so it can be used in scriptcs & others)
// * to minimize footprint of integration in fsi/CompileOps

/// hardcoded to net461 as we don't have fsi on netcore
let targetFramework = "net461"


module ReflectionHelper =
    let assemblyHasAttribute (theAssembly: Assembly) attributeName =
        try
            theAssembly.GetCustomAttributes false
            |> Seq.tryFind (fun a -> a.GetType().Name = attributeName)
            |> function | Some _ -> true | _ -> false
        with | _ -> false
    let getAttributeNamed (theType: Type) attributeName =
        try
            theType.GetCustomAttributes false
            |> Seq.tryFind (fun a -> a.GetType().Name = attributeName)
        with | _ -> None

    let getInstanceProperty<'treturn> (theType: Type) indexParameterTypes propertyName =
        try
            let property = theType.GetProperty(propertyName, typeof<'treturn>)
            if isNull property then
                None
            elif not (property.GetGetMethod().IsStatic)
                 && property.GetIndexParameters() = indexParameterTypes
            then
                Some property
            else
                None
        with | _ -> None

    let getInstanceMethod<'treturn> (theType: Type) (parameterTypes: Type array) methodName =
        try
            let theMethod = theType.GetMethod(methodName, parameterTypes)
            if isNull theMethod then
                None
            else
                Some theMethod
        with | _ -> None

    let implements<'timplemented> (theType: Type) =
        typeof<'timplemented>.IsAssignableFrom(theType)

(* this is the loose contract for now, just to define the shape, but this is resolved through reflection *)
type internal IDependencyManagerProvider =
    inherit System.IDisposable
    abstract Name : string
    abstract ToolName: string
    abstract Key: string
    abstract ResolveDependencies : targetFramework: string * scriptDir: string * scriptName: string * packageManagerTextLines: string seq -> string * string list

type ReflectionDependencyManagerProvider(theType: Type, nameProperty: PropertyInfo, toolNameProperty: PropertyInfo, keyProperty: PropertyInfo, resolveDeps: MethodInfo) =
    let instance = Activator.CreateInstance(theType) :?> IDisposable
    let nameProperty     = nameProperty.GetValue >> string
    let toolNameProperty = toolNameProperty.GetValue >> string
    let keyProperty      = keyProperty.GetValue >> string
    static member InstanceMaker (theType: System.Type) = 
        if not (ReflectionHelper.implements<IDisposable> theType) then None
        else
        // maybe CE might be better
        match ReflectionHelper.getAttributeNamed theType "FSharpCompilerExtensibilityAttribute" with
        | None -> None
        | Some _ ->
        match ReflectionHelper.getInstanceProperty<string> theType Array.empty "Name" with
        | None -> None
        | Some nameProperty ->
        match ReflectionHelper.getInstanceProperty<string> theType Array.empty "ToolName" with
        | None -> None
        | Some toolNameProperty ->
        match ReflectionHelper.getInstanceProperty<string> theType Array.empty "Key" with
        | None -> None
        | Some keyProperty ->
        match ReflectionHelper.getInstanceMethod<string * string list> theType [|typeof<string>;typeof<string>;typeof<string>;typeof<string seq>;|] "ResolveDependencies" with
        | None -> None
        | Some resolveDependenciesMethod ->
            Some (fun () -> new ReflectionDependencyManagerProvider(theType, nameProperty, toolNameProperty, keyProperty, resolveDependenciesMethod) :> IDependencyManagerProvider)

    interface IDependencyManagerProvider with
        member __.Name     = instance |> nameProperty
        member __.ToolName = instance |> toolNameProperty
        member __.Key      = instance |> keyProperty
        member __.ResolveDependencies(targetFramework, scriptDir, scriptName, packageManagerTextLines) =
            let arguments = [|box targetFramework; box scriptDir; box scriptName; box packageManagerTextLines|]
            resolveDeps.Invoke(instance, arguments) :?> _
    interface IDisposable with
        member __.Dispose () = instance.Dispose()
            
let registeredDependencyManagers = lazy (
    let managers =
        // TODO: need to replace this
        [Assembly.Load("FSharp.DependencyManager.Paket")]
        |> Seq.filter (fun a -> ReflectionHelper.assemblyHasAttribute a "FSharpCompilerExtensibilityAttribute")
        |> Seq.collect (fun a -> a.GetTypes())
        |> Seq.choose ReflectionDependencyManagerProvider.InstanceMaker
        |> Seq.map (fun maker -> maker ())
    
    // TODO: handle conflicting keys
    managers
    |> Seq.map (fun pm -> pm.Key, pm)
    |> Map.ofSeq
)

let RegisteredDependencyManagers() = registeredDependencyManagers.Force()

let tryFindDependencyManagerInPath m (path:string) : IDependencyManagerProvider option =
    try
        match registeredDependencyManagers.Force() |> Seq.tryFind (fun kv -> path.StartsWith(kv.Value.Key + ":" )) with
        | None -> None
        | Some kv -> Some kv.Value
    with 
    | e -> 
        errorR(Error(FSComp.SR.packageManagerError(e.Message),m))
        None

let removeDependencyManagerKey (packageManagerKey:string) (path:string) = path.Substring(packageManagerKey.Length + 1).Trim()

let tryFindDependencyManagerByKey (key:string) : IDependencyManagerProvider option =
    registeredDependencyManagers.Force() |> Map.tryFind key

let resolve (packageManager:IDependencyManagerProvider) implicitIncludeDir fileName m packageManagerTextLines =
    try
        let loadScript,additionalIncludeFolders =
            packageManager.ResolveDependencies(
                targetFramework,
                implicitIncludeDir,
                fileName,
                packageManagerTextLines)

        Some(additionalIncludeFolders,loadScript,File.ReadAllText(loadScript))
    with e ->
        errorR(Error(FSComp.SR.packageManagerError(e.Message),m))
        None