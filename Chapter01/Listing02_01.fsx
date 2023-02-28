// Chapter 01 - Listing 2-1

/// Split a string into words at spaces.
let splitAtSpaces (text: string) =
    text.Split ' '
    |> Array.toList

// > splitAtSpaces "hello world";;
// val it: string list = ["hello"; "world"]


/// Analyze a string for duplicate words.
let wordCount text =
    let words = splitAtSpaces text
    let numWords = words.Length
    let distinctWords = List.distinct words
    let numDups = numWords - distinctWords.Length
    (numWords, numDups)

/// Analyze a string for duplicate words and display the results.
let showWordCount text =
     let numWords, numDups = wordCount text
     printfn "--> %d words in the text" numWords
     printfn "--> %d duplicate words" numDups

//val splitAtSpaces: text: string -> string list
//val wordCount: text: string -> int * int
//val showWordCount: text: string -> unit

// > showWordCount "Couldn't put Humpty together again";;
// --> 5 words in the text
// --> 0 duplicate words
// val it: unit = ()


// using `in` to put a whole definition of one line
let powerOfFour n =
    let nSquared = n * n in nSquared * nSquared

//    > powerOfFour 3;;
//    val it: int = 81

