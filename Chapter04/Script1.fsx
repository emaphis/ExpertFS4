// Chapter 4
// Introducing Imperative Programming

// Imperative Looping and Iterating

// Simple for Loops

open System.IO

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
open Microsoft.FSharp.Collections;;

let sparseMap = new Dictionary<(int * int), float>();;
//val sparseMap: Dictionary<(int * int),float> = dict []

sparseMap[(0,2)] <- 4.0;;

sparseMap[(1021,1847)] <- 9.0;;

sparseMap.Keys;;
//val it: Dictionary`2.KeyCollection<(int * int),float> =
//   seq [(0, 2); (1021, 1847)]


// Exceptions and Controlling Them

//let req1 = System.Net.WebRequest.Create("not a URL");;
//System.UriFormatException: Invalid URI: The format of the URI could not be determined.


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
            yield line
}
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

// intermal table wordTable is computed as soon as isCapital is applied.


// not precomputed

let isCapitalSlow word = isWord ["London"; "Paris"; "Warsaw"; "Tokyo"] word;;

// is is inefficient because because isWord
// is applied to both its first argument and its
// second argument every time you use the function isCapitalSlow.


// inappropriate data structure for the lookup
let isWordSlow2 (words: string list) (word: string) =
    List.exists (fun word2 -> word = word2) words

let isCapitalSlow2 word = isWordSlow2  ["London"; "Paris"; "Warsaw"; "Tokyo"] word


// 
let isWordSlow3 (words: string list) (word: string) =
    let wordTable = Set<string>(words)
    wordTable.Contains(word)

let isCapitalSlow3 word = isWordSlow3 ["London"; "Paris"; "Warsaw"; "Tokyo"] word


// Safe because HashSet is revealed to the world
open System.Collections.Generic

let isWord1 (words: string list) =
    let wordTable = HashSet<string>(words)
    fun word -> wordTable.Contains word


// Precomputation and Objects

// Listing 4-1. Precomputing a eord table before creating an object

open System

type NameLookupService =
    abstract Contains: string -> bool

let buildSimpleNameLookup (words: string list) =
    let wordTable = HashSet<_>(words)
    {new NameLookupService with
        member t.Contains w = wordTable.Contains w}

//type NameLookupService =
//  abstract Contains: string -> bool
//val buildSimpleNameLookup: words: string list -> NameLookupService


let capitalLookup = buildSimpleNameLookup ["London"; "Paris"; "Warsaw"; "Tokyo"];;
//val capitalLookup: NameLookupService

capitalLookup.Contains "Paris";;
//val it: bool = true

capitalLookup.Contains "Pittsburg";;
//val it: bool = false


// Memoizing Computations

// From Chapter 2, but modified to use stop watch
let time f =
    let sw = System.Diagnostics.Stopwatch.StartNew()
    let res = f()
    let finish = sw.Stop()
    (res, sw.Elapsed.TotalMilliseconds |> sprintf "%f ms")
//val time: f: (unit -> 'a) -> 'a * string


let rec fibSlow n = if n <= 2 then 1 else fibSlow (n - 1) + fibSlow (n - 2)
//val fibSlow: n: int -> int

// sorta fast
time (fun () -> fibSlow 30);;
time (fun () -> fibSlow 30);;
time (fun () -> fibSlow 30);;

//val it: int * string = (832040, "3.706700 ms")
//val it: int * string = (832040, "3.341500 ms")
//val it: int * string = (832040, "3.057200 ms")

// slow...
time (fun () -> fibSlow 45);;
time (fun () -> fibSlow 45);;
time (fun () -> fibSlow 45);;

//val it: int * string = (1134903170, "3480.051600 ms")
//val it: int * string = (1134903170, "3500.174400 ms")
//val it: int * string = (1134903170, "3388.321100 ms")


// using a lookup table is much faster...

let fibFast1 =
    let t = new System.Collections.Generic.Dictionary<int, int>()
    let rec fibCached n =
        if t.ContainsKey n then t[n]
        elif n <= 2 then 1
        else let res = fibCached (n - 1) + fibCached (n - 2)
             t.Add (n, res)
             res
    fun n -> fibCached n
//val fibFast1: (int -> int)

// fast
time(fun () -> fibFast1 30);;
time(fun () -> fibFast1 30);;
time(fun () -> fibFast1 30);;

//val it: int * string = (832040, "0.081600 ms")
//val it: int * string = (832040, "0.053200 ms")
//val it: int * string = (832040, "0.054800 ms")

// still fast
time(fun () -> fibFast1 45);;
time(fun () -> fibFast1 45);;
time(fun () -> fibFast1 45);;

//val it: int * string = (1134903170, "0.070300 ms")
//val it: int * string = (1134903170, "0.055100 ms")
//val it: int * string = (1134903170, "0.079700 ms")


// Listing 4-2. A generic memoization function

open System.Collections.Generic

// warning FS0040: This and other recursive references to the object(s) being
// defined will be checked for initialization-soundness at runtime through the
// use of a delayed reference. This is because you are defining one or more
// recursive objects, rather than recursive functions. This warning may be
// suppressed by using '#nowarn "40"' or '--nowarn:40'.

#nowarn "40"

let memoize (f: 'T -> 'U) =
    let t = new Dictionary<'T, 'U>(HashIdentity.Structural)
    fun n ->
        if t.ContainsKey n then t[n]
        else 
            let res = f n
            t.Add(n, res)
            res
//val memoize: f: ('T -> 'U) -> ('T -> 'U) when 'T: equality

let rec fibFast2 =
    memoize (fun n -> if n <= 2 then 1 else fibFast2 (n - 1) + fibFast2 (n - 2))
//val fibFast2: (int -> int)


// Omit the extra argument from the application of memoize, because including it 
// would lead to a fresh memoization table being allocated each time the
// function fibNotFast was called

let rec fibNotFast n =
    memoize (fun n -> if n <= 2 then 1 else fibNotFast (n - 1) + fibNotFast (n - 2)) n
//val fibNotFast: n: int -> int


// Listing 4-3. A generic memoization service
// define a new variation on memoize that returns a Table object that supports
// both a lookup and a Discard method


open System.Collections.Generic

type Table<'T, 'U> =
    abstract Item : 'T -> 'U with get
    abstract Discard : unit -> unit

let memoizedAndPermitDiscard f =
    let lookasideTable = new Dictionary<_,_>(HashIdentity.Structural)

    {new Table<'T, 'U> with
        member _t.Item
            with get(n) =
                if lookasideTable.ContainsKey(n) then
                    lookasideTable[n]
                else
                    let res = f n
                    lookasideTable.Add(n, res)
                    res

        member _t.Discard() =
            lookasideTable.Clear() }

//type Table<'T,'U> =
//  abstract Discard: unit -> unit
//  abstract Item: 'T -> 'U with get
//val memoizedAndPermitDiscard: f: ('T -> 'U) -> Table<'T,'U> when 'T: equality


#nowarn "40"

let rec fibFast3 =
    memoizedAndPermitDiscard (fun n ->
    if n <= 2 then 1
    else fibFast3[n - 1] + fibFast3[n - 2])
//val fibFast3: Table<int,int>

fibFast3[3];;
//val it: int = 2

fibFast3[5];;
//val it: int = 5

fibFast3[45];;
//val it: int = 1134903170

fibFast3.Discard();;


// Lazy Values

let sixty = lazy (30 + 30);;
//val sixty: Lazy<int> = Value is not created.

sixty.Force();;
//val it: int = 60


// Lazy Values with side effects

let sixtyWithSideEffect = lazy (printfn "Hello world"; 30+30);;
//val sixtyWithSideEffect: Lazy<int> = Value is not created.


sixtyWithSideEffect.Force();;
//Hello world
//val it: int = 60


sixtyWithSideEffect.Force();;
//val it: int = 60


// Mutable Reference Cells

let cell4 = ref 1;;
//val cell4: int ref = { contents = 1 }

cell4.Value;;
//val it: int = 1

cell4.Value <- 3;;

cell4;;
//val it: int ref = { contents = 3 }

cell4.Value;;
//val it: int = 3


//ref
//val it: ('a -> 'a ref)

//(:=)  -- deprecated:  Change 'cell := expr' to 'cell.Value <- expr'.
//val it: ('a ref -> 'a -> unit)

//(!) -- deprecated:  Change '!cell' to 'cell.Value'.
//val it: ('a ref -> 'a)


// some definitions

//type 'T ref1 =
//    {mutable contents : 'T}
//    member cell.Value = cell.contents

//let (!) r = r.contents

//let ref1 v = {contents = v}

//type 'T ref =
//  { mutable contents: 'T }
//  member Value: 'T
//val (!) : r: 'a ref -> 'a
//val ref: v: 'a -> 'a ref


// Combining Functional and Imperative: Functional 
// Programming with Side Effects

// Consider Replacing Mutable Locals and Loops with Recursion

let factorizeImperative n =
    let mutable factor1 = 1
    let mutable factor2 = n
    let mutable i = 2
    let mutable fin = false

    while (i < n && not fin) do
        if (n % i = 0) then
            factor1 <- i
            factor2 <- n / i
            fin <- true
        i <- i + 1

    if (factor1 = 1) then None
    else Some (factor1, factor2)
//val factorizeImperative: n: int -> (int * int) option

// inner recursive function:

let factorizeRecursive n =
    let rec find i =
        if i >= n then None
        elif (n % i = 0) then Some (i, n / i)
        else find (i + 1)
    find 2
// val factorizeRecursive: n: int -> (int * int) option


// Separating Pure Computation from Side-Effecting Computations

// Separating Mutable Data Structures

// Example divides a sequence of inputs into equivalence classes

open System.Collections.Generic

/// Example divides a sequence of inputs into equivalence classes
let divideIntoEquivalenceClasses keyf seq =
    // The dictionary to hold the equivalence classes
    let dict = new Dictionary<'key, ResizeArray<'T>>()

    // Build the groupings
    seq |> Seq.iter (fun v ->
        let key = keyf v
        let ok, prev = dict.TryGetValue(key)
        if ok then prev.Add(v)
        else
            let prev = new ResizeArray<'T>()
            dict[key] <- prev
            prev.Add(v))

    // Return the sequence-of-sequences. Don't reveal the
    // internal collections: just reveal them as sequences
    dict |> Seq.map (fun group -> group.Key, Seq.readonly group.Value)

//val divideIntoEquivalenceClasses:
//   keyf: ('T -> 'key) -> seq: 'T seq -> ('key * 'T seq) seq when 'key: equality


divideIntoEquivalenceClasses (fun n -> n % 3) [0 .. 10];;
//val it: (int * int seq) seq =
//  seq [(0, seq [0; 3; 6; 9]); (1, seq [1; 4; 7; 10]); (2, seq [2; 5; 8])]


// Not All Side Effects Are Equal

// Avoid Combining Imperative Programming and Laziness
//
// But it's reasonable to read from a file lazily

// Note: create a "test.txt" file in .~/AppData/local/temp/ directory ...

open System.IO

let reader1, reader2 =
    let reader = new StreamReader(File.OpenRead("test.txt"))
    let firstReader() = reader.ReadLine();
    let secondReader() = reader.ReadLine();

    // Note: we close the stream reader here!
    // But we are returning function values that use the reader
    // This is very bad!
    reader.Close()
    firstReader, secondReader

//val reader2: (unit -> string)
//val reader1: (unit -> string)

// Note: stream reader is now closed! The next line will fail!
//let firstLine = reader1()
//let secondLine = reader2()
//firstLine, secondLine

//System.ObjectDisposedException: Cannot read from a closed TextReader.
//at System.IO.__Error.ReaderClosed()


// Corrected
open System.IO

let line1, line2 =
    let reader = new StreamReader(File.OpenRead("test.txt"))
    let firstLine = reader.ReadLine();
    let secondLine = reader.ReadLine();
    reader.Close()
    firstLine, secondLine

//val line2: string = "This is two lines."
//val line1: string = "This is one line."


let linesOfFile =
    seq { use reader = new StreamReader(File.OpenRead("test.txt"))
          while not reader.EndOfStream do
            yield reader.ReadLine() }


linesOfFile;;
//val it: string seq =
//  seq ["This is one line."; "This is two lines."; "This is three lines."]
