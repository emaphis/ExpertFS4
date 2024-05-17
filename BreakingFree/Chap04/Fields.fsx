// Class Fields

// Fields

// `let` Bindings

type PersonH () =
    let mutable name : string = ""
    member x.Name
        with get() = name
        and  set(v) = name <- v

let personH = PersonH()
personH.Name <- "Fred"
printfn "Name = %s" personH.Name


// Explicit Fields
// Create an explicit field with the val keyword.

type PersonI () =
    [<DefaultValue>]
    val mutable n : string
    member x.Name
        with get() = x.n
        and  set(v) = x.n <- v

let personI = PersonI()
printfn "%s" personI.Name
personI.Name <- "Jerry"
printfn "%s" personI.Name
