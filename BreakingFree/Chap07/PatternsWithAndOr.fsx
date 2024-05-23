// Patterns - Patterns With And Or

// Combining Patterns with AND

let locatePoint1 =
     function
     | (0, 0) as p -> sprintf "%A is at the origin" p
     | (x, y) & (_, 0) -> sprintf "(%i, %i) is on the x-axis" x y
     | (x, y) & (0, _) -> sprintf "(%i, %i) is on the y-axis" x y
     | (x, y) -> sprintf "Point (%i, %i)" x y

printfn "Where = %s" (locatePoint1 (0, 0))
printfn "Where = %s" (locatePoint1 (0, 10))
printfn "Where = %s" (locatePoint1 (15, 0))
printfn "Where = %s" (locatePoint1 (10, 20))


// Combining Patterns with OR

let locatePoint2 =
     function
     | (0, 0) as p -> sprintf "%A is at the origin" p
     | (_, 0) | (0, _) as p -> sprintf "%A is on an axis" p
     | p -> sprintf "Point %A" p

printfn "Where = %s" (locatePoint2 (0, 0))
printfn "Where = %s" (locatePoint2 (0, 10))
printfn "Where = %s" (locatePoint2 (15, 0))
printfn "Where = %s" (locatePoint2 (10, 20))


// Parentheses in Patterns

let locatePoint3 =
     function
     | (0, 0) as p -> sprintf "%A is at the origin" p
     | (x, y) & ((_, 0) | (0, _)) -> sprintf "(%i, %i) is on an axis" x y
     | p -> sprintf "Point %A" p

printfn "Where = %s" (locatePoint3 (0, 0))
printfn "Where = %s" (locatePoint3 (0, 10))
printfn "Where = %s" (locatePoint3 (15, 0))
printfn "Where = %s" (locatePoint3 (10, 20))
