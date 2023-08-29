// Chapter 2 - Your First F# Program: Getting 
//             Started with F#


// Creating Your First F# Program


// Listing 2-1. Analyzing a string for duplicate words

/// Split a string into words at spaces.
let splitAtSpaces (text: string) =
    text.Split ' '
    |> Array.toList


/// Analyze a string for duplicate words.
let wordCount text =
    let words = splitAtSpaces text
    let numWords = words.Length
    let distinctWords = List.distinct words
    let numDups = numWords - distinctWords.Length
    (numWords, numDups)


/// Analize a string for duplicates words and display the result
let showWordCount text =
    let numWords, numDups = wordCount text
    printfn "--> %d words in the text" numWords
    printfn "--> %d duplicate words" numDups


let (numWords', numDups') = wordCount "All the king's horses and all the king's men";;
//val numWords': int = 9
//val numDups': int = 2


showWordCount "Couldn't put Humpty together again";;
//--> 5 words in the text
//--> 0 duplicate words
//val it: unit = ()


//Understanding Types

wordCount;;
//val it: (string -> int * int) = <fun:it@43>


// Get some variables in the namespace for examples

let text = "All the king's horses and all the king's men";
// let text = "Couldn't put Humpty together again"
let words = splitAtSpaces text
let numWords = words.Length
let distinctWords = List.distinct words
let numDups = numWords - distinctWords.Length


// Calling Functions

splitAtSpaces "hello world";;
//val it: string list = ["hello"; "world"]


//Lightweight Syntax -  let x ... in ...

let powerOfFour n =
    let nSquared = n * n in nSquared * nSquared


powerOfFour 3;;
//val it: int = 81


// Understanding Scope

//let badDefinition =
//    let words = splitAtSpaces text
//    let text = "We three kings"
//    words.Length

// Script1.fsx(58,31): error FS0039: The value or constructor 'text' is not defined. Maybe you want one of the following:


// the following function computes (n*n*n*n)+ 2:
let powerOfFourPlusTwo n =
    let n = n * n
    let n = n * n
    let n = n + 2
    n

powerOfFourPlusTwo 2;;
//val it: int = 18

// is equivalent to
let powerOfFourPlusTwo' n =
    let n1 = n * n
    let n2 = n1 * n1
    let n3 = n2 + 2
    n3

powerOfFourPlusTwo' 2;;
//val it: int = 18


let powerOfFourPlusTwoTimesSix n =
    let n3 =
        let n1 = n * n
        let n2 = n1 * n1
        n2 + 2
    let n4 = n3 * 6
    n4

powerOfFourPlusTwoTimesSix 2;;
//val it: int = 108



//let invalidFunction n =
//    let n3 =
//        let n1 = n + n
//        let n2 = n1 * n1
//        n1 * n2
//    let n4 = n1 + n2 + n3     // Error! n3 is in scope, but n1 and n2 are not!
//    n4

// Script1.fsx(108,14): error FS0039: The value or constructor 'n1' is not defined.


// Using Data Structures

//let wordCount text =
// let words = splitAtSpaces text
// let distinctWords = List.distinct words
// ...


List.distinct ["b"; "a"; "b"; "b"; "c" ];;
// val it: string list = ["b"; "a"; "c"]

List.distinct (List.distinct ["abc"; "ABC"]);;
//val it: string list = ["abc"; "ABC"]


let numWords'' = List.length words
let numDups'' = numWords - distinctWords.Length
//val numWords'': int = 9
//val numDups'': int = 2


let length (inp: 'T list) = inp.Length;;

length [1; 2; 3; 4;]
//val it: int = 4


// Using Tuples

let site1 = ("www.cnn.com", 10)
let site2 = ("news.bbc.com", 5)
let site3 = ("www.msnbc.com", 4)
let sites = (site1, site2, site3)

//val site1: string * int = ("www.cnn.com", 10)
//val site2: string * int = ("news.bbc.com", 5)
//val site3: string * int = ("www.msnbc.com", 4)
//val sites: (string * int) * (string * int) * (string * int) =
//  (("www.cnn.com", 10), ("news.bbc.com", 5), ("www.msnbc.com", 4))


fst site1;;
//val it: string = "www.cnn.com"

let relevance = snd site1;;
//val relevance: int = 10


let fst' (a, _) = a
let snd' (_, b) = b

// decomposition using patterns
let url, relevance' = site1
let siteA, siteB, siteC = sites

//val url: string = "www.cnn.com"
//val relevance': int = 10
//val siteC: string * int = ("www.msnbc.com", 4)
//val siteB: string * int = ("news.bbc.com", 5)
//val siteA: string * int = ("www.cnn.com", 10)


// passing tuples

let showResults' (numWords, numDups) =
    printfn "--> %d words in the text" numWords
    printfn "--> %d duplicate words" numDups

let showWordCount' text = showResults' (wordCount text)

//al showResults': numWords: int * numDups: int -> unit
//val showWordCount': text: string -> unit

showResults' (numWords, numDups)
showWordCount' text

//--> 9 words in the text
//--> 2 duplicate words
//val it: unit = ()


// VALUES AND OBJECTS  - everything in F# is a value

// Using Imperative Code

printfn "--> %d words in the text" numWords
printfn "--> %d duplicate words" numDups

//--> 9 words in the text
//--> 2 duplicate words

// using the .Net library

System.Console.WriteLine("--> {0} words in the text", numWords)
System.Console.WriteLine("--> {0} duplicate words", numDups)

let two = (printfn "Hello World"; 1 + 1)
let four = two + two
//Hello World
//val two: int = 2
//val four: int = 4

(printfn "--> %d words in the text" numWords;
printfn "--> %d duplicate words" numDups)
//--> 9 words in the text
//--> 2 duplicate words
//val it: unit = ()


// passing a tuple to a function

let splitAtSpaces' (text : string) = 
    text.Split ' ' 
    |> Array.toList
//val splitAtSpaces': text: string -> string list

splitAtSpaces' text;;
// val it: string list =
//   ["All"; "the"; "king's"; "horses"; "and"; "all"; "the"; "king's"; "men"]



// Using Object-Oriented Libraries from F#

// Listing 2-3. Using the .NET networking libraries from F#

open System.IO
open System.Net

/// Get the contents of the URL via a web request
let http (url: string) =
    let req = WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html


http "https://news.bbc.co.uk";;

// val http: url: string -> string

// Using open to Access Namespaces and Modules
