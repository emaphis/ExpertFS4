// Functional Programming  - Recursive Functions

// Recursive Functions

let rec factorialA v =
    match v with
    | 1L -> 1L
    | _ -> v * factorialA (v - 1L)


// Tail-Call Recursion

[<TailCall>]
let factorialB n =
    let rec loop v acc =
        match v with
        | 1L -> acc
        | _ -> loop (v - 1L) (v * acc)
    loop n 1L

// Mutually Recursive Functions

let fibonacci n =
    let rec f =
        function
        | 1 -> 1
        | n -> g (n - 1)
    and g =
        function
        | 1 -> 0
        | n -> g (n - 1) + f (n - 1)
    f n + g n

let fib1 = fibonacci 6
