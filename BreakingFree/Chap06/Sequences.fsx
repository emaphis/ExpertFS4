// Collections - Sequences

// Sequences

// Creating Sequences

// Sequence Expressions

let lines =
    seq { use r = new System.IO.StreamReader("ArnoldMovies.txt")
    while not r.EndOfStream do yield r.ReadLine() }

lines |> printfn "%A"


// Range Expressions

seq { 0..10 }
seq { 0.0..10.0 }
seq { 'a'..'z' }
seq { 0..10..100 }
//seq { 'a'..2..'z' }

// Empty Sequences

let emptySequence = Seq.empty<string>

let emptySequence2 = Seq.empty

// Initializing a Sequence

let rand = System.Random()

let seq3 = Seq.init 10 (fun _ -> rand.Next(100))
printfn "%A" seq3


// Working with Sequences

let len1 = seq { 0..99 } |> Seq.length

let len2 =
    seq { for i in 1..10 do
                 printfn "Evaluating %i" i
                 yield i }
    |> Seq.length = 0

seq { for i in 1..10 do
        printfn "Evaluating %i" i
        yield i }
|> Seq.isEmpty

// Iterating over Sequences

let seq2 = seq { 0..99 } |> Seq.map (fun i -> i * i)
printfn "%A" seq2


// Sorting Sequences

let rand2 = System.Random()

let seq4 = Seq.init 10 (fun _ -> rand2.Next 100) |> Seq.sort
printfn "%A" seq4


// Sort Arnolds movies

let printMovies movies =
    movies
        |> Seq.iter (fun (title, year) ->
            printfn "Title: %s, year: %i" title year)

let movies =
    seq { use r = new System.IO.StreamReader(".\\Chap06\\ArnoldMovies.txt")
        while not r.EndOfStream do
            let l = r.ReadLine().Split(',')
            yield l[0], int l[1]
        }
    |> Seq.sortBy snd

printMovies movies

// Filtering Sequences

let before85 =
    movies |> Seq.filter (fun (_, year) -> year < 1985)

printMovies before85


// Aggregating Sequences

let sum1 = 
    seq { 1 .. 10 } |> Seq.fold (fun s c -> s + c) 0

printfn "%d" sum1

// using the operator directly

let sum2 =
    seq { 1 .. 10 } |> Seq.fold (+) 0

printfn "%d" sum2

// less general function

let sum3 =
    seq { 1 .. 10 } |> Seq.reduce (+)

printfn "%d" sum3

let sum4 =
    seq { 1 .. 10 } |> Seq.sum

printfn "%d" sum4


let avg1 =
    seq { 1.0 .. 10.0 } |> Seq.average

printfn "%f" avg1
