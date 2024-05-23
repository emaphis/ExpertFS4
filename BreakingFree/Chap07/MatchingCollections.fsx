// Patterns - Matching Collections

// Matching Collections

// Array Patterns

let getLength =
    function
    | null -> 0
    | [| |] -> 0
    | [| _ |] -> 1
    | [| _; _ |] -> 2
    | [| _; _; _ |] -> 3
    | a -> a |> Array.length

do printfn "%i" (getLength [| |])
do printfn "%i" (getLength [| 1 |])
do printfn "%i" (getLength [| 1; 2 |])


// List Patterns

let getLength2 =
    function
    | []  -> 0
    | [ _ ] -> 1
    | [ _; _; ] -> 2
    | [ _; _; _ ] -> 3
    | lst -> lst |> List.length

do printfn "%i" (getLength2 [ ])
do printfn "%i" (getLength2 [ 1 ])
do printfn "%i" (getLength2 [ 1; 2 ])
do printfn "%i" (getLength2 [ 1; 2; 3; 4; 5 ])


// Cons Patterns

let getLength3 n =
    let rec loop c l =
        match l with
        | []  -> c
        | _ :: t -> loop (c + 1) t
    loop 0 n

do printfn "%i" (getLength3 [ ])
do printfn "%i" (getLength3 [ 1 ])
do printfn "%i" (getLength3 [ 1; 2 ])
