// Functional Programming - Lambda Expressions

// Lambda Expressions

(fun degreesF -> (degreesF - 32.0) * (5.0 / 9.0)) 212.0

// Closures

let createCounter() =
    let count = ref 0
    (fun () ->
        count.Value <- count.Value + 1;
                       count.Value)

let increment = createCounter()
for i in [1..10] do printfn "%i" (increment())
