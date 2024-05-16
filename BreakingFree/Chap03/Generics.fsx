// Generics

// Automatic Generalization

let toTriple a b c = (a, b, c)

let triple = toTriple "an a" "a b" "a c"

// Explicit Generalization

let toTriple' (a : 'A) (b : 'B) (c : 'C) = (a, b, c)

let triple2 = toTriple' "a" 3 "Cee"


///////////////////////////////////////
// Typing constraints in F#

// Subtype constraints

let myfunc0 (strean: 'T when 'T :> System.IO.Stream) = ()

// Nullness constraints

let inline myFunc1 (a: ^T when ^T : null) = ()

// Member constraints

// instance memeber
let inline myFunc2 (a: ^T when ^T : (member ReadLine: unit -> string)) = ()

// static member
let inline myFunc3 (a: ^T when ^T : (static member Parse : string -> ^T)) = ()

// Default constructor constraints

let myFunc4 (stream : 'T when 'T : (new : unit -> 'T)) = ()

// Value type constraints

let myFunc5 (stream : 'T when 'T : struct) = ()

// Reference type constraints

let myFunc6 (stream : 'T when 'T : not struct) = ()

// Delegate constraints

open System
let myFunc7 (stream : 'T when 'T : delegate<obj * EventArgs, unit>) = ()

// Unmanaged constraints

let myFunc8 (stream : 'T when 'T : unmanaged) = ()

// Equality constraints

let myFunc9 (stream : 'T when 'T : equality) = ()

// Comparison constraints

let myFunc10 (stream : 'T when 'T : comparison) = ()


// Flexible Types

let myFunc11 (stream : #System.IO.Stream) = ()


// Wildcard Pattern

let printList (l : List<_>) = l |> List.iter (fun i -> printfn "%O" i)

do printList [ 1; 2; 3; 4; 5 ]

// Statically Resolved Type Parameters

let inline (!**) x = x ** 2.0
