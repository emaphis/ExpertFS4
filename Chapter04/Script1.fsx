// Chapter 4
// Introducing Imperative Programming


// Imperative Looping and Iterating

// Simple for Loops

open System.IO
open System.Net

let http (url : string) =
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html

// for loop

let repeatFetch url n =
    for i = 1 to n do
        let html = http url
        printfn "fetched <<< %s >>>\n" html
    printfn "Done!"

//val http: url: string -> string
//val repeatFetch: url: string -> n: int -> unit


// Simple While Loops

open System

let loopUntilSaturday() =
    while (DateTime.Now.DayOfWeek <> DayOfWeek.Saturday) do
        printfn "Still working!"

    printfn "Saturday at last!"


// More Iteration Loops over Sequences

for (b, pj) in [("Banana 1", false); ("Banana 2", true)] do
    if pj then
        printfn "%s is in pyjamas today!" b;;
//Banana 2 is in pyjamas today!
//val it: unit = ()


open System.Text.RegularExpressions;;

for m in Regex.Matches("All the Pretty Horses", "[a-zA-Z]+") do
    printfn "res = %s" m.Value;;
//res = All
//res = the
//res = Pretty
//res = Horses
//val it: unit = ()


// Using Mutable Records

type DiscreteEventCounter =
    { mutable Total : int
      mutable Positive : int
      Name : string }

let recordEvent (s : DiscreteEventCounter) isPositive =
    s.Total <- s.Total + 1
    if isPositive then s.Positive <- + 1
//val recordEvent: s: DiscreteEventCounter -> isPositive: bool -> unit

let reportStatus (s : DiscreteEventCounter) =
    printfn "We have %d %s out of %d" s.Positive s.Name s.Total
//val reportStatus: s: DiscreteEventCounter -> unit

let newCounter nm =
    { Total = 0;
      Positive = 0;
      Name = nm }


let longPageCounter = newCounter "long pages(s)"


let fetch url =
    let page = http url
    recordEvent longPageCounter (page.Length > 10000)
    page
//val fetch: url: string -> string


fetch "https://www.smh.com.au" |> ignore;;

fetch "https://www.theage.com.au" |> ignore;;

reportStatus longPageCounter;;
//We have 1 long pages(s) out of 2


// Avoid Aliasing

type Cell = { mutable data : int };;

let cell1 = { data = 3 };;
//val cell1: Cell = { data = 3 }

let cell2 = cell1;; // an alias
//val cell2: Cell = { data = 3 }

cell1.data <- 7;;
//val it: unit = ()

cell1;;
//val it: Cell = { data = 7 }

cell2;;
//val it: Cell = { data = 7 }


// Using Mutable let Bindings

let mutable cell3 = 1;;
//val mutable cell3: int = 1

cell3 <- 3;;

cell3;;
//val it: int = 3


// how to use a mutable location:

let sum n m =
    let mutable res = 0;
    for i = n to m do
        res <- res + i
    res

sum 3 6;;
//val it: int = 18


// Hiding Mutable Data
// - Mutable data is often hidden behind an encapsulation boundary.

let generateStamp =
    let mutable count = 0
    (fun () -> count <- count + 1; count)
//val generateStamp: (unit -> int)

generateStamp();;
//val it: int = 1

generateStamp();;
//val it: int = 2

generateStamp();;
//val it: int = 3


// UNDERSTANDING MUTATION AND IDENTIT
// 
// Mutable reference cells are different; they can reveal their identities through aliasing and mutation. Not 
// all mutable values necessarily reveal their identity through mutation

// you can detect whether two mutable values are the same object by using the function 
// System.Object.ReferenceEquals


// Working with Arrays
// Arrays a mutable and a building block of high-performance computing

let arr = [|1.0; 1.0; 1.0|]
//val arr: float array = [|1.0; 1.0; 1.0|]

arr[1];;
//val it: float = 1.0

arr[1] <- 3.0;;
//val it: unit = ()

arr;;
//val it: float array = [|1.0; 3.0; 1.0|]


// Some Important Expressions and Functions from the Array Module

Array.append
//val it: ('a array -> 'a array -> 'a array)

Array.sub
//val it: ('a array -> int -> int -> 'a array)


Array.copy
//val it: ('a array -> 'a array)

Array.iter
//val it: (('a -> unit) -> 'a array -> unit)

Array.filter
//val it: (('a -> bool) -> 'a array -> 'a array)


Array.length
//val it: ('a array -> int)

Array.map
//val it: (('a -> 'b) -> 'a array -> 'b array)

Array.fold
//val it: (('a -> 'b -> 'a) -> 'a -> 'b array -> 'a)

Array.foldBack
//val it: (('a -> 'b -> 'b) -> 'a array -> 'b -> 'b)


// A Big Array
// let bigArray = Array.zeroCreate<int> 100000000;;

// A BIG Array
// let tooBig = Array.zeroCreate<int> 1000000000;;


// Generating and Slicing Arrays

let arr2 = [| for i in 0 .. 5 -> (i, i*i) |];;
//val arr2: (int * int) array =
//  [|(0, 0); (1, 1); (2, 4); (3, 9); (4, 16); (5, 25)|]


// A convenient syntax for extracting subarrays from existing arrays, called slice notation.

arr2[1..3];;
//val it: (int * int) array = [|(1, 1); (2, 4); (3, 9)|]

arr2[..2];;
//val it: (int * int) array = [|(0, 0); (1, 1); (2, 4)|]

arr2[3..]
//val it: (int * int) array = [|(3, 9); (4, 16); (5, 25)|]


// Introducing the Imperative .NET Collections
// - System.Collections.Generic.

// Using Resizable Arrays

// List<'T>  -- Resizable Array
// type ResizeArray<'T> = System.Collections.Generic.List<'T>

let names = new ResizeArray<string>();;
//val names: ResizeArray<string>

for name in ["Claire"; "Sophie"; "Jane"] do
    names.Add(name) ;;

names.Count;;
//val it: int = 3

names[0];;
//val it: string = "Claire"

names[1];;
//val it: string = "Sophie"

names[2];;
//val it: string = "Jane"


let squares = new ResizeArray<int>(seq { for i in 0 .. 100 -> i*i});;
//val it: string = "Jane"

for x in squares do
    printfn "square: %d" x;;
// blah blah blah


// Using Dictonaries

// System.Collections.Generic.Dictionary<'Key, 'Value>

open System.Collections.Generic;;

let capitals = new Dictionary<string, string>(HashIdentity.Structural);;
//val capitals: Dictionary<string,string> = dict []

capitals["USA"] <- "Washington";;

capitals["Bangladesh"] <- "Dharka";;

capitals.ContainsKey("USA");;
//val it: bool = true

capitals.ContainsKey("Austrailia");;
//val it: bool = false

capitals.Keys;;
//val it: Dictionary`2.KeyCollection<string,string> = seq ["USA"; "Bangladesh"]

capitals["USA"];;
//val it: string = "Washington"


//Dictionaries are compatible with the type seq<KeyValuePair<'key,'value>>

for kvp in capitals do
    printfn "%s has a capital %s" kvp.Key kvp.Value

//USA has a capital Washington
//Bangladesh has a capital Dharka


// Using Dictionary’s TryGetValue

// Here’s how you do it using a mutable local:

open System.Collections.Generic

let lookupName nm (dict : Dictionary<string, string>) =
    let mutable res = ""
    let foundIt = dict.TryGetValue(nm, &res)
    if foundIt then res
    else failwithf "Didn't find %s" nm
//val lookupName: nm: string -> dict: Dictionary<string,string> -> string


let mutable res = "";;
//val mutable res: string = ""

capitals.TryGetValue("Australia", &res);;
//val it: bool = false

capitals.TryGetValue("USA", &res);;
//val it: bool = true

// retreive the value
res;;
//val it: string = "Washington"


// Don’t pass the final parameter, and instead the result is returned as 
// part of a tuple

capitals.TryGetValue("Australia");;
//val it: bool * string = (false, null)

capitals.TryGetValue("USA");;
//val it: bool * string = (true, "Washington")


// Using Dictionaries with Compound Keys

open System.Collections.Generic;;

let sparseMap = new Dictionary<(int * int), float>();;
//val sparseMap: Dictionary<(int * int),float> = dict []

sparseMap[(0,2)] <- 4.0;;

sparseMap[(1021,1847)] <- 9.0;;

sparseMap.Keys;;
//val it: Dictionary`2.KeyCollection<(int * int),float> =
//   seq [(0, 2); (1021, 1847)]


// Exceptions and Controlling Them

//let req1 = System.Net.WebRequest.Create("not a URL");;
//ystem.UriFormatException: Invalid URI: The format of the URI could not be determined.


//In F#, exceptions are commonly raised using the F# failwith function:

if false then 3 else failwith "hit the wall";;
//System.Exception: hit the wall

failwith
//System.Exception: hit the wall

raise
//val it: (Exception -> 'a)

failwithf
//val it: (Exception -> 'a)

invalidArg
//val it: (string -> string -> 'a)

// Exception raising functions can be used to fill un incomplete code

if (System.DateTime.Now > failwith "not yet decided") then
    printfn "you've run out of time"
//System.Exception: not yet decided


// Catching Exceptions

try
    raise (System.InvalidOperationException ("It is not my day"))
with
    :? System.InvalidOperationException -> printfn "caught!"
//caught!



open System.IO

let http1 (url : string) =
 try
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    html
 with
    | :? System.UriFormatException -> ""
    | :? System.Net.WebException -> ""
//val http1: url: string -> string


// All exception values support the Message property:

try
    raise (new System.InvalidOperationException ("invalid operation"))
with err -> printfn "opps, msg = '%s'" err.Message
//opps, msg = 'invalid operation'


// Using try . . . finally

let httpViaTryFinally (url: string) =
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    try
        let stream = resp.GetResponseStream()
        let reader = new StreamReader(stream)
        let html = reader.ReadToEnd()
        html
    finally
        resp.Close()
//val httpViaTryFinally: url: string -> string


// Using a use binding

let httpViaUseBinding (url: string) =
    let req = System.Net.WebRequest.Create(url)
    use resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    html
//val httpViaUseBinding: url: string -> string


// Defining New Exception Types

exception BlocketURL of string

let http2 url =
    if url = "https://www.kaos.org"
    then raise (BlocketURL(url))
    else http url
//val http2: url: string -> string

//You can extract the information from F# exception values, again using pattern matching
try
    raise (BlocketURL ("https://www.kaos.org"))
with
    BlocketURL url -> printfn "blocked! url = '%s'" url
//blocked! url = 'https://www.kaos.org'


// Having an Effect: Basic I/O

open System.IO;;

let tmpFile = Path.Combine(__SOURCE_DIRECTORY__, "temp.txt");;
//val tempFile: string = "C:\src\fsharp\ExpertFS4\Chapter04\temp.txt"

File.WriteAllLines(tmpFile, [|"This is a test file."; "It is easy to read."|]);;


// reading all the lines of a file

File.ReadAllLines tmpFile;;
//val it: string array = [|"This is a test file."; "It is easy to read."|]


File.ReadAllText tmpFile;;
//val it: string = "This is a test file.
//It is easy to read."


// Read lines on-demand, as a sequence using
// System.IO.File.ReadLines

seq { 
    for line in File.ReadLines tmpFile do     
        let words = line.Split [|' '|]
        if words.Length > 3 && words[2] = "easy" then
            yield line }
//val it: string seq = seq ["It is easy to read."]


// .NET I/O via Streams

// a simple example of using System.IO.File.CreateText to create a StreamWriter and write 
// two strings:

let outp = File.CreateText "playlist.txt";;
//val outp: StreamWriter

outp.WriteLine "Enchanted";;
outp.WriteLine "Put your records on";;

outp.Close();;


let inp = File.OpenText("playlist.txt");;
//val inp: StreamReader

inp.ReadLine();;
//val it: string = "Enchanted"

inp.Close();;    


// Some Other I/O-Related Types


// Using System.Console

System.Console.WriteLine "Hello, Console!";;
//Hello, Console!

System.Console.ReadLine();;
//"I'm still here"
//val it: string = ""I'm still here""


// Combining Functional and Imperative Efficient 
// Precomputation and Caching

// Precomputation and Partial Application

let isWord (words: string list) =
    let wordTable = Set.ofList words
    fun w -> wordTable.Contains(w)
//val isWord: words: string list -> (string -> bool)

let isCapital = isWord ["London"; "Paris"; "Warsaw"; "Tokyo"];;

isCapital "Paris";;
//val it: bool = true

isCapital "Manchester";;
//val it: bool = false


