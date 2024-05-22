// Pattern Matching - Pattern-Matching Functions

//

let testOptions =
    function
    | Some(v)  -> printfn "Some: %A" v
    | None -> printfn "None"

do testOptions (Some "Alpha")
do testOptions None


fun x ->
    match x with
    | Some(v)  -> printfn "Some: %A" v
    | None -> printfn "None"

function
    | Some(v)  -> printfn "Some: %A" v
    | None -> printfn "None"


let list1 =
    [ Some 10; None; Some 4; None; Some 0; Some 7 ]
    |> List.filter (function
                    | Some(_) -> true
                    | None -> false)

printfn "%A" list1
