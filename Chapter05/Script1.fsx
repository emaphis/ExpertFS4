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

type StringMap<'T> = Map<string, 'T>
type Projections<'T, 'U> = ('T -> 'U) * ('U -> 'T)


let fetch url = (url, Useful.http url)
//val fetch: url: string -> string * string


let rec map (f : 'T -> 'U) (l : 'T list) =
    match l with
    | h :: t -> f h :: map f t
    | [] -> []


