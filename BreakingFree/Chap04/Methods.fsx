// Methods

// Instance Methods

open System

type CircleA(diameter : float) =
    member x.Diameter = diameter
    member x.GetArea() =
        let r = diameter / 2.0
        Math.PI * r * r


let cirA = CircleA 5.0
printfn "Diameter = %f" cirA.Diameter
printfn "Area = %f" (cirA.GetArea())


// Method Accessibility

type CircleB(diameter : float) =
    member private x.GetRadius() = diameter / 2.0
    member x.Diameter = diameter
    member x.GetArea() = Math.PI * (x.GetRadius() ** 2.0)

let cirB = CircleB 5.0

let areaB = cirB.GetArea()
printfn "Diameter = %f" cirB.Diameter
printfn "Area = %f" areaB

// Using a `let` Binding  - Functional Style

type CircleC(diameter : float) =
    let getRadius() = diameter / 2.0
    member x.Diameter = diameter
    member x.GetArea() = Math.PI * (getRadius() ** 2.0)

let cirC = CircleC 5.0
printfn "Diameter = %f" cirC.Diameter
printfn "Area = %f" (cirC.GetArea())


// Named Arguments  TODO:


// Overloaded Methods

open System.IO

type RepositoryA() =
    member x.Commit(files, desc, branch) =
        printfn "Committed %i files (%s) to \"%s\"" (List.length files) desc branch
    member x.Commit(files, desc) =
        x.Commit(files, desc, "default")

let files1 = [ "names.txt"; "reader.text"; "titles.txt" ]

let reptA = RepositoryA()

reptA.Commit(files1, "check-ins", "Pittsburgh")

reptA.Commit(files1, "check-outs")


// Optional Parameters

open System.IO

type RepositoryB() =
    static member Commit(files, desc, ?branch) =
        let targetBranch = defaultArg branch "default"
        printfn "Committed %i files (%s) to \"%s\"" (List.length files) desc targetBranch

RepositoryB.Commit(files1, "check-ins", "Pittsburgh")
RepositoryB.Commit(files1, "check-outs")


// Slice Expressions

type SentenceB(initial: string) =
    let words = initial.Split ' '
    let text = initial

    member x.GetSlice(lower, upper) =
       match defaultArg lower 0 with
       | l when l >= words.Length -> Array.empty<string>
       | l ->
           match defaultArg upper (words.Length - 1) with
           | u when u >= words.Length -> words[1..]
           | u -> words[1..u]

let sent1 = SentenceB "Don't forget to drink your Ovaltine"

let subStr1 = sent1[1..3]
printfn "Sub-String = %A" subStr1

let word2 = sent1[1..1]
printfn "Sub-String = %A" word2


type SentenceC(initial: string) =
    let words = initial.Split ' '
    let text = initial

    member x.GetSlice(lower, upper) =
       match defaultArg lower 0 with
       | l when l >= words.Length -> Array.empty<string>
       | l ->
           match defaultArg upper (words.Length - 1) with
           | u when u >= words.Length -> words[1..]
           | u -> words[1..u]

    member x.GetSlice(lower1, upper1, lower2, upper2) =
        x.GetSlice(lower1, upper1)
        |> Array.map
            (fun w ->
             match defaultArg lower2 0 with
             | l when l >= w.Length -> ""
             | l ->
                 match defaultArg upper2 (w.Length - 1) with
                 | u when u >= w.Length -> w.[l..]
                 | u -> w.[l..u])

let sent2 = SentenceC "Don't forget to drink your Ovaltine"

let stuff: string[] = (sent2[1..4, ..1])
