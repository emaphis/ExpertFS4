// Patterns -  Pattern-Matching Functions

// Pattern-Matching Functions

let testOption opt =
    match opt with
    | Some(v) -> printfn "Some: %A" v
    | None -> printfn "None"

do testOption (Some 100)
do testOption None

// Lambdas

fun x ->
    match x with
    | Some(v) -> printfn "Some: %A" v
    | None   -> printfn "None"

let newList =
    [ Some 10; None; Some 4; None; Some 0; Some 7 ]
    |> List.filter (function
                    | Some (_) -> true
                    | None -> false)

do printfn "List: %A" newList
