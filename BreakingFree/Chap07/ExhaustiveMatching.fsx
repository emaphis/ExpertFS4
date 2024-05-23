// Patterns - Exhaustive Matching

// Exhaustive Matching

let numberToString1 =
    function
    | 0 -> "zero"
    | 1 -> "one"
    | 2 -> "two"
    | 3 -> "three"


do printfn "%s" (numberToString1 3)

//do printfn "%s" (numberToString1 4)


// Variable Patterns

let numberToString2 =
    function
    | 0 -> "zero"
    | 1 -> "one"
    | 2 -> "two"
    | 3 -> "three"
    | n -> sprintf "%O" n

do printfn "%s" (numberToString2 3)
do printfn "%s" (numberToString2 100)


// The Wildcard Pattern

let numberToString3 =
    function
    | 0 -> "zero"
    | 1 -> "one"
    | 2 -> "two"
    | 3 -> "three"
    | _ -> "unknown"

do printfn "%s" (numberToString3 2)
do printfn "%s" (numberToString3 100)


// Matching Constant Values

let numberToString4 =
     function
     | 0 -> "zero"
     | 1 -> "one"
     | 2 -> "two"
     | 3 -> "three"
     | _ -> "..."

do printfn "%s" (numberToString4 2)
do printfn "%s" (numberToString4 100)
