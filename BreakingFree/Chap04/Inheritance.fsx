// Classes - Inheritance.

// Inheritance

type BaseType() =
    member x.SayHello name =
        printfn "Hello, %s" name

type DerivedType() =
    inherit BaseType()

let base1 : BaseType = DerivedType()
do base1.SayHello("Burkwald")


type WorkItem(summary: string, desc: string) =
    member val Summary = summary
    member val Description = desc

    // Override
    override x.ToString() = sprintf "%s" x.Summary

type Defect(summary, desc, severity: int) =
    inherit WorkItem(summary, desc)
    member val Severity = severity

    override x.ToString() = sprintf "%s (%i)" (base.ToString()) x.Severity

type Enhancement(summary, desc, requestedBy: string) =
    inherit WorkItem(summary, desc)
    member val RequesteBy = requestedBy

    override x.ToString() = sprintf "%s Requested by: %s" (base.ToString()) x.RequesteBy


// Casting

// Upcasting
// :> Upcasting Operator

let work1: WorkItem = Defect("Incompatibility detected", "Delete", 1)
let work2 = Defect("Wrong implementation", "Delete", 1) :> WorkItem
let work3 = Enhancement("Error 101", "Repair now", "Fred Burd")

// Downcasting
// :?> Dynamic Cast Operator

let defect1 = work1 :?> Defect

let enhancement1 = work2 :?> Enhancement


// Overriding Members

let work4 = WorkItem("Take out the trash", "It's overflowing!")
let msg4 = work4.ToString()  // not very informative
printfn "message = %s" msg4

printfn "message = %s" (work2.ToString())
printfn "message = %s" (work3.ToString())


// Abstract Classes

[<AbstractClass>]
type NodeA(name:string, ?content: NodeA list) =
    member x.Name = name
    member x.Content = content

// Cannot be created
// let node1 = NodeA("top", [])


// Abstract Members

// Abstract Properties

[<AbstractClass>]
type AbstractBaseClassA() =
    abstract member SomeData:  string with get, set

type BindingBackeClassA() =
    inherit AbstractBaseClassA()
    let mutable someData = ""

    override x.SomeData
      with get() = someData
      and  set(v) = someData <- v

type DictionaryBackedClassA() =
    inherit AbstractBaseClassA()
    let dict =
        System.Collections.Generic.Dictionary<string, string>()
    [<Literal>]
    let SomeDataKey = "SomeData"

    override x.SomeData
       with get() =
           match dict.TryGetValue(SomeDataKey) with
           | true, v -> v
           | _, _ -> ""
       and set(v) =
           match System.String.IsNullOrEmpty(v) with
           | true when dict.ContainsKey(SomeDataKey) ->
               dict.Remove(SomeDataKey) |> ignore
           | _ -> dict[SomeDataKey] <- v


// Abstract Methods

open System

[<AbstractClass>]
type ShapeD() =
    abstract member GetArea : unit -> float


type CircleD(r: float) =
    inherit ShapeD()
    member val Radius = r
    override x.GetArea() =
        Math.Pow(Math.PI * r, 2.0)

type RectangleD(w: float, h: float) =
    inherit ShapeD()
    member val Width = w
    member val Height = h
    override x.GetArea() = w * h


// Virtual Members

open System
open System.Collections.Generic

type NodeB(name: string) =
    let children = List<NodeB>()
    member x.Childern with get() = children.AsReadOnly()
    abstract member AddChild : NodeB -> unit
    abstract member RemoveChild : NodeB -> unit
    default x.AddChild(n) = children.Add n
    default x.RemoveChild(n) = children.Remove n |> ignore


type TerminalNode(name: string) =
    inherit NodeB(name)
    [<Literal>]
    let notSupportedMsg = "Cannot add or remove children"
    override x.AddChild(n) =
        raise (NotSupportedException(notSupportedMsg))
    override x.RemoveChild(n) =
        raise (NotSupportedException(notSupportedMsg))


// Sealed Classes

[<Sealed>]
type NotInheritable() = class end

//type WontWork() =
//   inherit NotInheritable()
