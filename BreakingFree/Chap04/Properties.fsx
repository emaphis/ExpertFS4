// Classes - Properties
// Properties

// Explicit Properties

type PersonJ () =  // No constructor
    let mutable name : string = ""
    member x.Name
        with get() = name
        and  set(value) = name <- value


let meJ = PersonJ()
meJ.Name <- "Edward"
printfn "Name = %s" meJ.Name

// Alternative syntax

type PersonK () =  // No constructor
    let mutable name : string = ""
    member x.Name  with get() = name
    member x.Name with set(value) = name <- value

let meK = PersonK()
meK.Name <- "Edward"
printfn "Name = %s" meK.Name

// Read-only Property

type PersonL(name) =
    member x.Name with get() = name

let meL = PersonL("Fred")
printfn "Name = %s" meL.Name

// Simpler Read-only Definition in F#

type PersonM(name) =
    member x.Name = name

let meM = PersonM("Fred")
printfn "Name = %s" meM.Name


// Implicit Properties  -- Added in version 3.0
// defined by `member val`

type PersonN() =
    member val Name = "" with get, set

let meN = PersonN()
meN.Name <- "George"
printfn "Name is %s" meN.Name


// Indexed Properties

type Sentence(initial: string) =
    let mutable words = initial.Split ' '
    let mutable text = initial

    member x.Item
       with get i = words[i]
       and  set i v =
           words[i] <- v
           text <- System.String.Join(" ", words)

let sent1 = Sentence "Don't forget to drink your Ovaltine"

let word1 = sent1[1]
printfn "second word = %s" word1

sent1[1] <- "remember"
printfn "second word = %s" word1


// Setting at Initialization

type PersonO() =
    member val Name = "" with get, set

let personO = PersonO(Name = "Joan")
printfn "My name is %s" personO.Name
