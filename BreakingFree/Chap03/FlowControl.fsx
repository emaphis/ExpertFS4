// Flow Control

// Looping

// while loops

let getInput() =
    printfn "Type something and press enter"
    let line =System.Console.ReadLine()
    line

let echoUserInput (getInput: unit -> string) =
    let mutable input = getInput()
    while not (input.ToUpper().Equals("QUIT")) do
        printfn "You entered: %s" input
        input <- getInput()

echoUserInput (getInput)

// for loop

for i = 0 to 100 do printfn "%i" i

for i = 100 downto 0 do printfn "%A" i

// enumeral for loop

for i in [0..10] do
    printfn "%A " i

// Using pattern matching in enumberable loops

type MpaaRating =
    | G
    | PG
    | PG13
    | R
    | NC17

type Movie = { Title : string; Year : int; Rating : MpaaRating option }

let movies =
  [ { Title = "The Last Witch Hunter"; Year = 2014; Rating = None }
    { Title = "Riddick"; Year = 2013; Rating = Some(R) }
    { Title = "Fast Five"; Year = 2011; Rating = Some(PG13) }
    { Title = "Babylon A.D."; Year = 2008; Rating = Some(PG13) } ]

// Pattern Matching - print out movies with ratings

for { Title = t; Year = y; Rating = Some(r) } in movies do
    printfn "%s (%i) - %A" t y r


// Branching

let isEven number =
     if number = 0 then
        printfn "zero"
     elif number % 2 = 0 then
        printfn "%i is even" number
     else
        printfn "%i is odd" number

isEven 0
isEven 1
isEven 2

let isEven' number =
     if number = 0 then
        sprintf "zero"
     elif number % 2 = 0 then
        sprintf "%i is even" number
     else
        sprintf "%i is odd" number

let s0 = isEven' 0
let s1 = isEven' 1
let s2 = isEven' 2
