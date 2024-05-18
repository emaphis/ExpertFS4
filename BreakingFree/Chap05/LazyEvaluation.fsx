// Functional Programming -- Lazy Evaluation

// Lazy Evaluation

let lazyOperation =
    lazy (printfn "evaluating lazy expresson"
          System.Threading.Thread.Sleep(1000)
          42)

do lazyOperation.Force() |> printfn "Result: %i"

let result1 = lazyOperation.Value

do printfn "%i" result1


// Timing

#time "on"

let lazyOperation2 = lazy (System.Threading.Thread.Sleep(1000); 42)

do lazyOperation2.Force() |> printfn "Result: %i"

do lazyOperation2.Force() |> printfn "Result: %i"

#time "off"
