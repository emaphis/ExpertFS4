// Collections - Arrays

// Arrays


// Creating Arrays

// Array Expressions

 let names = [| "Rose"; "Martha"; "Donna"; "Amy"; "Clara" |]


let lines =
    [| use r = new System.IO.StreamReader("ArnoldMovies.txt")
       while not r.EndOfStream do yield r.ReadLine() |]

// Empty Arrays

let emptyArray1 = [| |]

let emptyArray2 : int array = [| |]

// Use a type function

let emptyArray3 = Array.empty<string>


// Initializing Arrays

let stringArray1 = Array.zeroCreate<string> 5

let stringArray2 = Array.init<string> 5 (fun _ -> "")

;;

// Working with Arrays

// Accessing Elements

let movie1 = lines[3]
printfn "%s" movie1

// Update
do lines[5] <- "Barman & Robin,1997"
printfn "%s" lines[5]

// A more functional approach

let movies = [| "The Terminator"; "Predator"; "Commando" |]

do Array.set movies 1 "Batman & Robin"
do Array.get movies 1 |> printfn "%s"

let subLines = lines[1..3]
printfn "%A" subLines


// Copying Arrays

let copy1 = [| 1..10 |] |> Array.copy
printfn "%A" copy1

// Sorting Arrays

let rand = System.Random()
let ints = Array.init 5 (fun _ -> rand.Next (-100, 100))

do ints |> Array.sortInPlace
printfn "%A" ints

let movies2 =
    [| ("The Terminator", "1984")
       ("Predator", "1987")
       ("Commando", "1985")
       ("Total Recall", "1990")
       ("Conan the Destroyer", "1984") |]

do movies2 |> Array.sortInPlaceBy (fun (_, y) -> y)
printfn "%A" movies2

do movies2
  |> Array.sortInPlaceWith (fun (_, y1) (_, y2) ->
                            if y1 < y2 then -1
                            elif y1 > y2 then 1
                            else 0)

printfn "%A" movies2


// Multidimensional Arrays

let movies3 =
    array2D [| [| "The Terminator"; "1984" |]
               [| "Predator"; "1987" |]
               [| "Commando"; "1985" |]
               [| "The Running Man"; "1987" |]
               [| "True Lies"; "1994" |]
               [| "Last Action Hero"; "1993" |]
               [| "Total Recall"; "1990" |]
               [| "Conan the Barbarian"; "1982" |]
               [| "Conan the Destroyer"; "1984" |]
               [| "Hercules in New York"; "1969" |] |]

let commandoReleaseYear = movies3[2, 1]
let titles = movies3[0.., 0..0]
let releaseYear = movies3[0.., 1..1]

let justAFew = movies3[1..3, 0..]


// Jagged Arrays  - Arrays of Arrays

let movies4 = [| [| "The Terminator"; "1984"; "James Cameron" |]
                 [| "Predator"; "1987"; "John McTiernan" |]
                 [| "Commando"; "1985" |] |]

let director1 = movies4[1][2]
printfn "%s" director1
