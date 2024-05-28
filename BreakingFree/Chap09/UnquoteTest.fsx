// Testing unquote

#r "nuget: Unquote"

open Swensen.Unquote

test <@ 1 + 1 = 3 @>

/// Calculate the factorial of `n`
let rec fact n =
    match n with
    | 0 -> 1
    | n -> n * fact (n - 1)


let run() =
    printf "Testing..."
    test <@ 1 + 1 = 2 @>
    test <@ fact 0 = 1 @>
    test <@ fact 1 = 1 @>
    test <@ fact 2 = 2 @>
    test <@ fact 3 = 6 @>
    test <@ fact 4 = 24 @>
    printfn "...done"

run()
