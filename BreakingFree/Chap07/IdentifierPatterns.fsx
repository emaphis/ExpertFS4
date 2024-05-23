// Patterns - Identifier Patterns

// Identifier Patterns

// Matching Union Cases

type Shape =
| Circle of float
| Rectangle of float * float
| Triangle of float * float * float


let getPerimeter =
    function
    | Circle(r) -> 2.0 * System.Math.PI * r
    | Rectangle(w,h) -> 2.0 * (w + h)
    | Triangle(l1, l2, l3) -> l1 + l2 + l3


let per1 = getPerimeter (Circle(10.0))
do printfn "Circle = %f" per1

let per2 = getPerimeter (Rectangle(2.0, 4.0))
do printfn "Rectangle = %f" per2

let per3 = getPerimeter (Triangle (1.0, 2.0, 3.0))
printfn "Triangle = %f" per3


// Matching Literals

[<LiteralAttribute>]
let Zero = 0
[<LiteralAttribute>]
let One = 1
[<LiteralAttribute>]
let Two = 2
[<LiteralAttribute>]
let Three = 3
[<LiteralAttribute>]
let Four = 4

let numberToString =
 function
 | Zero -> "zero"
 | One -> "one"
 | Two -> "two"
 | Three -> "three"
 | Four -> "Four"
 | _ -> "unknown"

[ 0; 1; 2; 3] |> List.map (fun num -> numberToString num )


// Matching Nulls

let matchString =
    function
    | null
    | "" -> None
    | v -> Some(v.ToString())


let noStr1 = matchString ""
let someStr1 = matchString "A String"
let nullString = matchString null


// Matching Tuples

let point = 10, 20
let x, y = point


let locatePoint p =
    match p with
    | (0, 0) -> sprintf "%A is at the origin" p
    | (_, 0) -> sprintf "%A is on the x-axis" p
    | (0, _) -> sprintf "%A is at the y-axis" p
    | (x, y) -> sprintf "Point (%i, %i)" x y

let where1 = locatePoint point
printfn "Where = %s" where1
printfn "Where = %s" (locatePoint (0, 10))
printfn "Where = %s" (locatePoint (15, 0))
printfn "Where = %s" (locatePoint (0, 0))


// Matching Records

type Name = { First : string; Middle : string option; Last : string }

let formatName =
    function
    | { First = f; Middle = Some(m); Last = l }  -> sprintf "%s, %s %s" l f m
    | { First = f; Middle = None; Last = l }   -> sprintf "%s, %s" l f

let name1 = { First =  "Thomas"; Middle = Some("Evens"); Last = "Wade"}
let name2 = { First = "James"; Middle = None; Last = "Smith"}

let fName1 = formatName name1
printfn "%A" fName1

let fName2 = formatName name2
printfn "%A" fName2


let hasMiddleName =
    function
    | { Middle = Some(_) } -> true
    | { Middle = None }  -> false


let hasMiddleName2 =
    function
    | { Name.Middle = Some(_) } -> true
    | { Name.Middle = None } -> false
