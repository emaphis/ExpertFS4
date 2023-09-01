// Chapter 5
// Understanding Types in Functional 
// Programming


#load "Library.fs"
open Chapter05

Say.hello "Charley";;
Useful.getWords "Hello FSharp World!";;


// Exploring Some Simple Type Definition

// Defining Type Abbreviations

type index = int
type flags = int64
type results = string * System.TimeSpan * int * int

//type index = int
//type flags = int64
//type results = string * System.TimeSpan * int * int


// Defining Record Types

open System

type Person =
    { Name : string
      DateOfBirth : DateTime }


{ Name = "Bill"; DateOfBirth = DateTime(1962, 09, 02) }

//val it: Person =
//  { Name = "Bill"
//    DateOfBirth = 9/2/1962 12:00:00 AM {Date = 9/2/1962 12:00:00 AM;
//    ... }


// Record values are often used to return results from functions

type PageStats =
    { Site : string
      Time : TimeSpan
      Length : int
      NumWords : int
      NumHRefs : int }


//Using the time, http and getWords functions from Chapter 3.
let stats site=
    let url = "https://" + site
    let html, t = Useful.time (fun () -> Useful.http url)
    let words = html |> Useful.getWords
    let hrefs = words |> Array.filter (fun s -> s = "href")
    { Site = site
      Time = t
      Length = html.Length
      NumWords = words.Length
      NumHRefs = hrefs.Length }
//val stats: site: string -> PageStats

stats "www.google.com";;
//val it:
//PageStats = { Site = "www.google.com"
//              Time = 00:00:00.3264960 {Days = 0; ...
//              Length = 55497
//              NumWords = 3218
//              NumHRefs = 29 }


// Handling Non-Unique Record Field Names

//type Person =
//    { Name : string
//      DateOfBirth : DateTime }

type Company =
    { Name : string
      Address : string }

// When record names are non-unique, constructions of record values may need
// to use object expressions in order to indicate the name of the record type,

type Dot = { X : int; Y : int }
type Point = { X : int; Y : int }

let coords1 (p:Point) = (p.X, p.Y);;
//val coords1: p: Point -> int * int

let coords2  (d:Dot) = (d.X, d.Y);;
//val coords2: d: Dot -> int * int

// use of X and Y implies type "Point"
let dist p = (p.X * p.X + p.Y * p.Y);;
//val dist: p: Point -> int


let anna = ({Name = "Anna"; DateOfBirth = new System.DateTime(1968, 07, 23)} : Person )


// Cloning Records

type Point3D = { X : float; Y : float; Z : float }

let p1 = { X = 3.0; Y = 4.0; Z = 5.0 }
let p2 = { p1 with Y = 0.0; Z = 0.0 }
//val p1: Point3D = { X = 3.0; Y = 4.0; Z = 5.0 }
//val p2: Point3D = { X = 3.0; Y = 0.0; Z = 0.0 }


// Defining Discriminated Unions

type Route = int
type Make = string
type Model = string
type Transport =
    | Car of Make * Model
    | Bicycle
    | Bus of Route


let ian = Car("BMW", "369");;
//val ian: Transport = Car ("BMW", "369")

let don = [Bicycle; Bus 8];;
//val don: Transport list = [Bicycle; Bus 8]

let peter = [Car("Ford", "Fiesta"); Bicycle];;
//val peter: Transport list = [Car ("Ford", "Fiesta"); Bicycle]

// You can also use discriminators in pattern matching
let averageSpeed (tr: Transport) =
    match tr with
    | Car _  -> 35
    | Bicycle -> 16
    | Bus _  -> 24
// val averageSpeed: tr: Transport -> int

// Discriminated unions can include recursive references (the same is true of
// records and other type definitions)

type Proposition =
    | True
    | And of Proposition * Proposition
    | Or of Proposition * Proposition
    | Not of Proposition

// Recursive functions can be used to traverse such a type:

let rec eval (p : Proposition) =
    match p with
    | True  -> true
    | And (p1, p2) -> eval p1 && eval p2
    | Or (p1, p2) -> eval p1 || eval p2
    | Not (p1)  -> not (eval p1)
//val eval: p: Proposition -> bool

// the option type
type 'T option =
    | None
    | Some of 'T

type 'T list =
    | ([])
    | (::) of 'T * 'T list


// tree-like data structures are conveniently represented as discriminated unions. 

type Tree<'T> =
    | Tree of 'T * Tree<'T> * Tree<'T>
    | Tip of 'T

// use recursive functions to compute properties of trees

let rec sizeOfTree tree =
 match tree with
 | Tree(_, l, r) -> 1 + sizeOfTree l + sizeOfTree r
 | Tip _ -> 1
// val sizeOfTree: tree: Tree<'a> -> int


let smallTree = Tree ("1", Tree ("2", Tip "a", Tip "b"), Tip "c");;
//val smallTree: Tree<string> =
//  Tree ("1", Tree ("2", Tip "a", Tip "b"), Tip "c")

sizeOfTree smallTree;;
//val it: int = 5


// Using Discriminated Unions as Records

type Point3D' = Vector3D of float * float * float 

let origin = Vector3D(0., 0., 0.)
let unitX = Vector3D(1., 0., 0.)
let unitY = Vector3D(0., 1., 0.)
let unitZ = Vector3D(0., 0., 1.)

let length (Vector3D(dx, dy, dz)) =
    sqrt(dx * dx + dy * dy + dz * dz)
//val length: Point3D' -> float


// Defining Multiple Types Simultaneously
// using 'and'

type Node =
    { Name : string
      Links : Link list }
and Link =
    | Dangling
    | Link of Node


// Understanding Generics

// Type abbreviations can also be generic;

type StringMap<'T> = Map<string, 'T>
type Projections<'T, 'U> = ('T -> 'U) * ('U -> 'T)


let fetch url = (url, Useful.http url)
//val fetch: url: string -> string * string


let rec map (f : 'T -> 'U) (l : 'T list) =
    match l with
    | h :: t -> f h :: map f t
    | [] -> []
//val map: f: ('T -> 'U) -> l: 'T list -> 'U list


//If you want, you can also write the type parameters explicitly on a declaration. 

let rec map2<'T, 'U> (f : 'T -> 'U) (l : 'T list) =
    match l with
    | h :: t -> f h :: map2 f t
    | [] -> []
//val map2: f: ('T -> 'U) -> l: 'T list -> 'U list


// Writing Generic Functions

// automatic type parameters

let getFirst (a, b, c) = a
//val getFirst: a: 'a * b: 'b * c: 'c -> 'a


// Automatic generalization is particularly useful when taking functions as inputs
// The function takes two functions as inputs and applies them to each side of a tuple:

let mapPair f g (x, y) = (f x, g y)
//val mapPair: f: ('a -> 'b) -> g: ('c -> 'd) -> x: 'a * y: 'c -> 'b * 'd


// Generic Comparison

compare  // val it: ('a -> 'a -> int) when 'a: comparison
(=)   // val it: ('a -> 'a -> bool) when 'a: equality
(<)   // val it: ('a -> 'a -> bool) when 'a: comparison
(<=)  // val it: ('a -> 'a -> bool) when 'a: comparison
(>)   // val it: ('a -> 'a -> bool) when 'a: comparison
(>=)  // val it: ('a -> 'a -> bool) when 'a: comparison
(min) // val it: ('a -> 'a -> 'a) when 'a: comparison
(max) // val it: ('a -> 'a -> 'a) when 'a: comparison


// You can also use the comparison operators on most structured types

// lexical ordering
("abc", "def") < ("abc", "xyz");;
//al it: bool = true

compare (10, 30) (10, 20);;
//val it: int = 1

// you can use generic comparison with list and array values:

compare [10; 30] [10; 20];;
//val it: int = 1

compare [10; 20] [10; 30];;
//val it: int = -1

compare [|10; 30|] [|10; 20|];;
//val it: int = 1

compare [|10; 20|] [|10; 30|];;
//val it: int = -1


// Generic Hashing

hash
//val it: ('a -> int) when 'a: equality

hash 100;;
//val it: int = 100

hash "abc";;
//val it: int = 1099313834

hash (100, "abc");;
//val it: int = 1099316814


// Generic Pretty-Printing - using "%A"

sprintf "result = %A" ([1], [true]);;
//val it: string = "result = ([1], [true])"


// Generic Boxing and Unboxing

box
//val it: ('a -> obj)

unbox
//val it: ('a -> 'b)

box 1;;
//val it: obj = 1

box "abc";;
//val it: obj = 1

let stringObj = box "abc";;
//val stringObj: obj = "abc"

unbox<string> stringObj;;
//val stringObj: obj = "abc"

(unbox stringObj : string);;
//va it: string = "abc"


// Generic Binary Serialization via the .NET Libraries

open System.IO
open System.Runtime.Serialization.Formatters.Binary;

let writeValue outputStream x =
    let formatter = new BinaryFormatter()
    formatter.Serialize(outputStream, box x)

let readValue inputStream =
    let formatter = new BinaryFormatter()
    let res = formatter.Deserialize(inputStream)
    unbox res

//val writeValue: outputStream: Stream -> x: 'a -> unit
//val readValue: inputStream: Stream -> 'a


let address =
    Map.ofList [ "Jeff", "123 Main Street, Redmond, WA 98052";
                          "Fred", "987 Pinek Phila., PA 19166";
                          "Mary", "PO Box 112233, Palo Alto, CA 94301" ]

let fsOut = new FileStream("Data.dat", FileMode.Create)
writeValue fsOut address
fsOut.Close()

//val address: Map<string,string> =
//  map
//    [("Fred", "987 Pinek Phila., PA 19166");
//     ("Jeff", "123 Main Street, Redmond, WA 98052");
//     ("Mary", "PO Box 112233, Palo Alto, CA 94301")]
//val fsOut: FileStream


let fsIn = new FileStream("Data.dat", FileMode.Open)
let inData: Map<string,string> = readValue fsIn
printfn "Addresses = %A" inData
fsIn.Close()

//Addresses = map
//    [("Fred", "987 Pinek Phila., PA 19166");
//     ("Jeff", "123 Main Street, Redmond, WA 98052");
//     ("Mary", "PO Box 112233, Palo Alto, CA 94301")]
//val it: unit = ()
 

// Making Things Generic

// Generic Algorithms through Explicit Arguments

/// Highest common factor
let rec hcf a b =
    if a = 0 then b
    elif a < b then hcf a (b - a)
    else hcf (a - b) b
//val hcf: a: int -> b: int -> int

hcf 18 12;;
//val it: int = 6

hcf 33 24;;
//val it: int = 3


/// Highest common factor - generic
let hcfGeneric (zero, sub, lessThan) =
    let rec hcf a b =
        if a = zero then b
        elif lessThan a b then hcf a (sub b a)
        else hcf (sub a b) b
    hcf
//val hcfGeneric:
//    zero: 'a * sub: ('a -> 'a -> 'a) * lessThan: ('a -> 'a -> bool) ->
//        ('a -> 'a -> 'a) when 'a: equality

let hcfInt = hcfGeneric (0, (-), (<))
let hcfInt64 = hcfGeneric (0L, (-), (<))
let hcfBigInt = hcfGeneric (0I, (-), (<))

//val hcfInt: (int -> int -> int)
//val hcfInt64: (int64 -> int64 -> int64)
//val hcfBigInt:
//  (System.Numerics.BigInteger ->
//     System.Numerics.BigInteger -> System.Numerics.BigInteger)

hcfInt 18 12;;
//val it: int = 6

hcfBigInt 1810287116162232383039576I 1239028178293092830480239032I;;
//val it: System.Numerics.BigInteger = 33224


// Generic Algorithms through Function Parameters

type Numeric<'T> =
    { Zero : 'T
      Subtract : ('T -> 'T -> 'T)
      LessThan : ('T -> 'T -> bool) }

let intOps = {Zero = 0; Subtract = (-); LessThan = (<) }
let bigIntOps = {Zero = 0I; Subtract = (-); LessThan = (<) }
let int64Ops = {Zero = 0L; Subtract = (-); LessThan = (<) }

let hcfGeneric2 (ops : Numeric<'T>) =
    let rec hcf a b =
        if a = ops.Zero then b
        elif ops.LessThan a b then hcf a (ops.Subtract b a)
        else hcf (ops.Subtract a b) b
    hcf
//al hcfGeneric2: ops: Numeric<'T> -> ('T -> 'T -> 'T) when 'T: equality

let hcfInt = hcfGeneric2 intOps
let hcfBigInt = hcfGeneric2 bigIntOps

hcfInt 18 12;;
//val it: int = 6

hcfBigInt 1810287116162232383039576I 1239028178293092830480239032I;;
//val it: System.Numerics.BigInteger = 33224


type INumeric<'T> =
    abstract Zero : 'T
    abstract Subtract : ('T -> 'T -> 'T)
    abstract LessThan : ('T -> 'T -> bool)

let intOps =
    { new INumeric<int> with
        member ops.Zero = 0
        member ops.Subtract = fun x y -> x - y
        member ops.LessThan = fun x y -> x < y }


let hcfGeneric3 (ops : INumeric<'T>) =
    let rec hcf a b =
        if a = ops.Zero then b
        elif ops.LessThan a b then hcf a (ops.Subtract b a)
        else hcf (ops.Subtract a b) b
    hcf
//val hcfGeneric3: ops: INumeric<'T> -> ('T -> 'T -> 'T) when 'T: equality


// Generic Algorithms through Inlining

let convertToFloat x = float 
//val convertToFloat : x:int -> float


float 3.0 + float 1 + float 3L;;
//val it: float = 7.0

// make this code more generic

let inline convertToFloatAndAdd x y = float x + float y
//val inline convertToFloatAndAdd:
//x: ^a -> y: ^b -> float
//  when ^a: (static member op_Explicit: ^a -> float) and
//       ^b: (static member op_Explicit: ^b -> float)


// avoid passing the INumeric “dictionary” of operations explicitly by providing
// a simple wrapper that delegates to a non-inlined generic routine.

let inline hcf1 a b =
    hcfGeneric3{
        new INumeric<'T> with
            member ops.Zero = LanguagePrimitives.GenericZero<'T>
            member ops.Subtract = fun x y -> x - y
            member ops.LessThan = fun x y -> x < y }
        a b
//val inline hcf1:
//    a: ^T -> b: ^T -> ^T
//        when ^T: (static member Zero: ^T) and
//             ^T: (static member (-) : ^T * ^T -> ^T) and ^T: comparison


hcf1 18 12;;
//val it: int = 6

hcf1 18L 12L;;
//val it: int64 = 6L

hcf1 18I 12I;;
//val it: System.Numerics.BigInteger = 6


// Understanding Subtyping

// Casting Up Statically
// :>  - Upcast cohersion

let xobj = (1 :> obj);;
//val xobj: obj = 1

let sobj = ("abc" :> obj);;
//val sobj: obj = "abc"


// Casting Down Dynamically
// :?>

let boxedObject = box "abc";;
//val boxedObject: obj = "abc"

let downcastString = (boxedObject :?> string);;
//val downcastString: string = "abc"

// Downcasts are checked at runtime, hence the '?'
// The operator :?> raises an exception if the object isn’t of a suitable type:

// let xobj = box 1;;
// same 'let xobj = (1 :> obj)' ???

let x = (xobj :?> string);;
//System.InvalidCastException: Unable to cast object of type 'System.Int32' to type 'System.String'.


// Performing Type Tests via Pattern Matching
// performing dynamic type tests uses type-test patterns

let checkObject (x : obj) =
     match x with
     | :? string -> printfn "The object is a string"
     | :? int -> printfn "The object is an integer"
     | _ -> printfn "The input is something else"
//val checkObject: x: obj -> unit

checkObject (xobj);;
//The object is an integer

checkObject (box "abc");;
//The object is a string

// Such a pattern may also bind the matched value at its more specific type

let reportObject (x : obj) =
    match x with
    | :? string as s  -> printfn "The input is string '%s'" s
    | :? int as d -> printfn "The input is the integer '%d'" d
    | _ -> printfn "the input is something else"
// val reportObject: x: obj -> unit

reportObject (box 17);;
//val reportObject: x: obj -> unit


// Knowing When Upcasts Are Applied Automatically

open System
open System.Net

let dispose (c : IDisposable) = c.Dispose()

let obj1 = new WebClient()
let obj2 = new HttpListener()

dispose obj1
dispose obj2

//val dispose: c: System.IDisposable -> unit
//val obj1: System.Net.WebClient = System.Net.WebClient
//val obj2: System.Net.HttpListener

// Sometimes explicit upcasts are needed.

open System
open System.IO

let textReader =
     if DateTime.Today.DayOfWeek = DayOfWeek.Monday
     then Console.In
     else File.OpenText("input.txt")


let textReader =
     if DateTime.Today.DayOfWeek = DayOfWeek.Monday
     then Console.In
     else (File.OpenText("input.txt") :> TextReader)

let getTextReader () : TextReader = (File.OpenText("input.txt") :> TextReader)
//val getTextReader: unit -> TextReader


// Flexible Types
// F# also has the notion of a flexible type constraint, 

let disposeMany (cs : seq<#IDisposable>) =
    for c in cs do c.Dispose()
//val disposeMany: cs: #IDisposable seq -> unit

Seq.concat
//val it: (#('b seq) seq -> 'b seq)


// Troubleshooting Type-Inference Problems

// Using Type Annotations


let getLengths inp = 
    inp |> Seq.map (fun y -> y.Length) ;;
// error FS0072: Lookup on object of indeterminate type based on information prior
// to this program point. A type annotation may be needed prior to this program
// point to constrain the type of the object. This may allow the lookup to be resolved.


let getLengths inp = 
    inp |> Seq.map (fun (y: string) -> y.Length) ;;
//val getLengths: inp: string seq -> int seq



let printSecondElements (inp : seq<'T * int>) =
    inp
    |> Seq.iter (fun (x, y) -> printfn "y = %d" x)
// warning FS0064: This construct causes code to be less generic than indicated by the type
// annotations. The type variable 'T has been constrained to be type 'int'.


type PingPong = Ping | Pong

let printSecondElements (inp : seq<'T * int>) =
    inp
    |> Seq.iter (fun (x, y) -> printfn "y = %d" y)


// Understanding the Value Restriction

//The code attempts to create an array of empty lists

let empties0 = Array.create 100 [];;
// error FS0030: Value restriction. The value 'empties0' has been inferred to have generic type
//               val empties0: '_a list array    
// Either define 'empties0' as a simple data term, make it a function with explicit arguments or,
// if you do not intend for it to be generic, add a type annotation.


// Value restriction doesn’t apply to simple immutable data constants or function definitions.
// The following declarations are all automatically generalized

let emptyList = []
let intitializeList = ([], [2])
let listOfEmptyList = [[]; []]
let makeArray () = Array.create 100 []

//val emptyList: 'a list
//val intitializeList: 'a list * int list
//val listOfEmptyList: 'a list list
//val makeArray: unit -> 'a list array


// Working Around the Value Restriction

// Technique 1: Constrain Values to Be Nongeneric
// Use an explicit type annotation

let empties1 : int list [] = Array.create 100 [];;
// val empties1: int list array =


// Technique 2: Ensure Generic Functions Have Explicit Arguments

let mapFirst0 = List.map fst;;

let mapFirst1 inp = List.map fst inp;;
//val mapFirst1: inp: ('a * 'b) list -> 'a list


mapFirst1 [(1,"a"); (2,"b"); (3,"c")]
//val it: int list = [1; 2; 3]

// when there is only one arugment
let mapFirst2 inp = inp |> List.map (fun (x, y) -> x)
//val mapFirst2: inp: ('a * 'b) list -> 'a list

mapFirst2 [(1,"a"); (2,"b"); (3,"c")];;
//val it: int list = [1; 2; 3]


// Technique 3: Add a Unit Argument to Create a Generic Function

let empties3 () = Array.create 100 []
let intEmpties : int list [] = empties3 ()
let stringEmpties : string list [] = empties3 ()
