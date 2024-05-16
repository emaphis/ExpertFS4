// F# Bindings

// Bindings are F#’s primary way of identifying values or executable code.

// `let` Bindings

let intValue = 1
let strValue = "hello"

let add a b = a + b
let sum = add 1 2

// Literals
                // bindings are more like readonly values in C#
[<Literal>]     // like a C# Constant
let FahrenheitBoilingPoint = 212


// Mutable Bindings

let mutable name = "Date"
name <- "Nadia"
printfn "%s" name

// Horrible invalid code

let addSomeNumbers nums =
     let mutable sum = 0
     let add = (fun num -> sum <- sum + num)
     Array.iter (fun num -> add num) nums
     sum

let addend = addSomeNumbers [| 1..10 |]

addend = 55


// Refernce cells
// they are to pointers what mutable bindings are to traditional variables

let cell = ref 0
cell := 50                         // use of `:=` is deprecated
printf "Cell: %i\n" !cell     // use of `!` is deprecated

cell.Value <- 100                   // do this instead
printf "Cell: %i\n" cell.Value


// use Bindings

open System

let createDisposable name =
    printfn "Creating: %s" name
    { new IDisposable with
        member x.Dispose() =
            printfn "disposing: %s" name }

let testDisposable() =
    use root = createDisposable "outer"
    for i in [1..2] do
        use nested = createDisposable (sprintf "inner %i" i)
        printfn "completing interation %i " i
    printfn "leaving function"

do testDisposable()
