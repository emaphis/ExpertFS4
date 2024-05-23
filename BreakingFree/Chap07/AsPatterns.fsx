// Patterns - As Patterns

// As Patterns

// A pattern matching let binding

let x1, y1 = (10, 20)
do printfn "%i, %i" x1 y1w


let point2 =  (10, 20)
let x2, y2 = point2
do printfn "%i, %i" x2 y2


let x3, y3 as point3 = (10, 20)
do printfn "%i, %i" x3 y3
do printfn "%A" point3

let locatePoint =
     function
     | (0, 0) as p -> sprintf "%A is at the origin" p
     | (_, 0) as p -> sprintf "%A is on the X-Axis" p
     | (0, _) as p -> sprintf "%A is on the Y-Axis" p
     | (x, y) as p -> sprintf "Point (%i, %i)" x y

printfn "Where = %s" (locatePoint point3)
printfn "Where = %s" (locatePoint (0, 10))
printfn "Where = %s" (locatePoint (15, 0))
printfn "Where = %s" (locatePoint (0, 0))
