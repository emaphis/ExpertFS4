// Patterns - Match Expressions

// Match Expressions

let testOptions opt =
    match opt with
    | Some(v)  -> printfn "Some: %A" v
    | None -> printfn "None"

do testOptions (Some "Alpha")
do testOptions None


// Guard Clauses

let testNumber value =
    match value with
    | v when v < 0 -> printfn "%i is negative" v
    | v when v > 0 -> printfn "%i is positive" v
    | v -> printfn "%d is zero" v

do testNumber 0
do testNumber -100
do testNumber 55

// Combining test clauses with `&`

let testNumber2 value =
    match value with
    | v when v > 0 && v % 2 = 0 -> printfn "%i is positive and even" v
    | v -> printfn "%i is zero, negative, or odd" v

do testNumber2 20
do testNumber2 0
do testNumber2 -100
do testNumber2 55
