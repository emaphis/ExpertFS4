// Chapter 3  - Introducing Functional Programming

#load "Library1.fs"
open Chapter03

// Starting with Numbers and Strings

// Absent typing info this function is assumed to be 'int'

let squareAndAdd a b = a * a + b;;
// val squareAndAdd : a:int -> b:int -> int


// But with typing info this is float

let squareAndAdd' (a:float) b = a * a + b;;
// val squareAndAdd': a: float -> b: float -> float


// Full type annotations for the arguments and 
// return type of a function:

let squareAndAdd'' (a:float) (b:float) : float = a * a + b;;
// val squareAndAdd'': a: float -> b: float -> float


// Arithmetic Conversions

int 17.8
//val it: int = 17

int -17.8
//val it: int = -17

string 65
//val it: string = "65"

float 65
//al it: float = 65.0

double 65
// val it: double = 65.0


// Arithmetic Comparisons

let s = "Couldn't put Humpty";;
//val s: string = "Couldn't put Humpty"

s.Length;;
//val it: int = 19

s[13];;
//val it: char = 'H'

s[13..16];;
//val it: string = "Hump"

//s[13] <- 'h';;
//Script.fsx(61,1): error FS0810: Property 'Chars' cannot be set

"Couldn't put Humpty" + " " + "together again";;
//val it: string = "Couldn't put Humpty together again"


// Working with Conditionals: && and ||

// A basic control construct in F# programming is if/then/elif/else.

let round x =
    if x >= 100 then 100
    elif x < 0 then 0
    else x
//val round: x: int -> int

// Conditionals are shorthand for pattern matching.

let round' x =
    match x with
    | _ when x >= 100 -> 100
    | _ when x < 0  -> 0
    | _  -> x
//val round: x: int -> int


// Using && and || to build conditionals

let round2 (x, y) =
    if x >= 100 || y >= 100 then 100, 100
    elif x < 0 || y < 0 then 0, 0
    else x, y
//val round2: x: int * y: int -> int * int


// Defining Recursive Functions

let rec factorial n = if n <= 1 then 1 else n * factorial (n - 1);;
//val factorial: n: int -> int

factorial 5;;
//val it: int = 120

let rec length l =
    match l with
    | []  -> 0
    | h :: t  -> 1 + length t
// val length: l: 'a list -> int

length [1; 2; 3; 4];;
//val it: int = 4


// repeatedly fetches the HTML for a particular web page, printing each time it’s fetched 

open System.IO
open System.Net


/// Get the contents f the URL via a web request.
let http (url : string) =
    let req = System.Net.WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html
//val http: url: string -> string


let rec repeatFetch url n =
    if n > 0 then
        let html = http url
        printfn "fetched <<< %s >>> on iternation %d" html n
        repeatFetch url (n - 1)
//val repeatFetch: url: string -> n: int -> unit


// efine multiple recursive functions simultaneously by separating the definitions with and. 

let rec even n = (n = 0u) || odd(n - 1u)
and odd n = (n <> 0u) && even(n - 1u)
//val even: n: uint32 -> bool
//val odd: n: uint32 -> bool


// A more efficient, nonrecursive implementation of these is available:
let even' n = (n % 2u) = 0u
let odd' n = (n % 2u) = 1u


// Lists

[]
//val it: 'a list

1 :: [2; 3]
//val it: int list = [1; 2; 3]

[1 .. 99]

['A' .. 'Z']

[for x in 1..99 -> x*x]

[1; 2] @ [3; 4]
//val it: int list = [1; 2; 3; 4]


// Basic list values
let oddPrimes = [3; 5; 7; 11]
let morePrimes = [13; 17]
let primes = 2 :: (oddPrimes @ morePrimes)
//val oddPrimes : int list = [3; 5; 7; 11]
//val morePrimes : int list = [13; 17]
//val primes: int list = [2; 3; 5; 7; 11; 13; 17]

let people = [ "Adam"; "Dominic"; "James" ];;
//val people: string list = ["Adam"; "Dominic"; "James"]

"Chris" :: people
//al it: string list = ["Chris"; "Adam"; "Dominic"; "James"]

people;;
//val it: string list = ["Adam"; "Dominic"; "James"]


// List functions

List.length
//val it: ('a list -> int)

List.head
//val it: ('a list -> 'a)

List.tail
//val it: ('a list -> 'a list)

List.append
//val it: ('a list -> 'a list -> 'a list)

List.filter
//val it: (('a -> bool) -> 'a list -> 'a list)

List.map
//val it: (('a -> 'b) -> 'a list -> 'b list)

List.iter
//val it: (('a -> unit) -> 'a list -> unit)

List.unzip
//val it: (('a * 'b) list -> 'a list * 'b list)

List.zip
//val it: ('a list -> 'b list -> ('a * 'b) list)


List.ofArray
//val it: ('a array -> 'a list)

List.toArray
//val it: ('a list -> 'a array)


// examples of how to use some of the List functions

List.head [5; 4; 3];;
//val it: int = 5

List.tail [5; 4; 3];;
//val it: int list = [4; 3]

List.map (fun x -> x * x) [1; 2; 3];;
//val it: int list = [1; 4; 9]

List.filter (fun x -> x % 3 = 0) [2; 3; 5; 7; 9];;
//val it: int list = [3; 9]


// Options

type 'T option' =
    | None'
    | Some'


// (an association list) that uses options to represent the (optional) 
// parents of some well-known mythical characters:

let people' =
     [("Adam", None);
      ("Eve" , None);
      ("Cain", Some("Adam","Eve"));
      ("Abel", Some("Adam","Eve"))]
//val people': (string * (string * string) option) list =
//  [("Adam", None); ("Eve", None); ("Cain", Some ("Adam", "Eve"));
//   ("Abel", Some ("Adam", "Eve"))]


let fetch url =
    try Some (http url)
    with :? System.Net.WebException -> None
//val fetch: url: string -> string option


match (fetch "https://www.nature.com") with
    | Some text  -> printfn "text = %s" text
    | None   -> printfn "*** no web page found"

//text = <!DOCTYPE html>
//<html lang="en" class="grade-c">
//<head>
//    <title>Nature</title>


// Getting Started with Pattern Matching

let isLikelySecretAgent url agent =
    match (url, agent) with
    | "https://www.control.org", 99 -> true
    | "https://www.control.org", 86 -> true
    | "https://www.kaos.org", _ -> true
    | _ -> false
//val isLikelySecretAgent: url: string -> agent: int -> bool


// pattern matching can be used to decompose list values from the head downward

let printFirst xs =
    match xs with
    | h :: t  -> printfn "The first item in the list is %A" h
    | [] -> printfn "No items in the list"
// al printFirst: xs: 'a list -> unit


printFirst oddPrimes;
//The first item in the list is 3
//val it: unit = ()


// pattern matching can be used to examine option values

let showParents (name, parents) =
    match parents with
    | Some (dad, mum) -> printfn "%s has father %s and mother %s" name dad mum
    | None -> printfn "%s has no parens!" name
//val showParents: name: string * parents: (string * string) option -> unit

for person in people' do showParents person;;
//Adam has no parens!
//Eve has no parens!
//Cain has father Adam and mother Eve
//Abel has father Adam and mother Eve
//val it: unit = ()


// Matching on Structured Values

// An example in which nested tuple values are matched:

let highLow a b =
    match (a, b) with
    | ("lo", lo), ("hi", hi) -> (lo, hi)
    | ("hi", hi), ("lo", lo) -> (lo, hi)
    | _  -> failwith "exprectd a both a high and low value"

highLow ("hi", 300) ("lo", 100);;
//val it: int * int = (100, 300)

highLow ("lo", 100) ("hi", 300);;
//val it: int * int = (100, 300)


let urlFilter3 url agent =
 match url,agent with
    | "https://www.control.org", 86 -> true
    | "https://www.kaos.org", _ -> false

//warning FS0025: Incomplete pattern matches on this expression. For example,
//the value '(_,0)' may indicate a case not covered by the pattern(s).

//val urlFilter3: url: string -> agent: int -> bool


// Signal illegal cases
let urlFilter4 url agent =
 match url,agent with
    | "https://www.control.org", 86 -> true
    | "https://www.kaos.org", _ -> false
    | _ -> failwith "enexpected input"

//val urlFilter4: url: string -> agent: int -> bool



let urlFilter2 url agent =
     match url,agent with
     | "http://www.control.org", _ -> true
     | "http://www.control.org", 86 -> true
     | _ -> false;;

//warning FS0026: This rule will never be matched
//val urlFilter2: url: string -> agent: int -> bool


// Guarding Rules and Combining Patterns

let sign x =
    match x with
    | _ when x < 0 -> -1
    | _ when x > 0 -> 1
    | _  -> 0
//val sign: x: int -> int


// You can combine two patterns to represent two possible paths for matching:

let getValue a =
    match a with
    | (("lo" | "low"), v)    -> v
    | ("hi", v) | ("high", v) -> v
    | _  -> failwith "expeced both a high value and low value"
//val getValue: string * 'a -> 'a

// Further Ways of Forming Patterns


// Introducing Function Values - lambdas

let sites = ["https://www.bing.com"; "https://www.google.com"];;
//val sites: string list = ["https://www.bing.com"; "https://www.google.com"]

let fetch' url = (url, http url);;
//val fetch': url: string -> string * string

List.map fetch' sites;;
// blah blaw blah

// Types are useful to learn what a function does:
List.map;;
//val it: (('a -> 'b) -> 'a list -> 'b list),


// Using Function Values

let primes' = [2; 3; 5; 7];;
//val primes': int list = [2; 3; 5; 7]

let primeCubes = List.map (fun n -> n * n * n) primes';;
//val primeCubes: int list = [8; 27; 125; 343]

// another example

let resultOfFetch = List.map (fun url -> (url, http url)) sites
//val resultOfFetch: (string * string) list =
//  [("https://www.bing.com",
//    "<!doctype html><html lang="en" dir="ltr"><head><meta name="th"+[44634 chars]);
//   ("https://www.google.com",
//    "<!doctype html><html itemscope="" itemtype="http://schema.org"+[55445 chars])]

List.map (fun (_,p) -> String.length p) resultOfFetch;;
//val it: int list = [44695; 55587]


// Computing with Collection Functions

let deliminters = [| ' '; '\n'; '\t'; '<'; '>'; '=' |]

let getWords (s: string) = s.Split deliminters

let getStats site =
    let url = "https://" + site
    let html = http url
    let hwords = html |> getWords
    let hrefs = html |> getWords |>Array.filter (fun s -> s = "href")
    (site, html.Length, hwords.Length, hrefs.Length)

//val deliminters: char array = [|' '; '\010'; '\009'; '<'; '>'; '='|]
//val getWords: s: string -> string array
//val getStats: site: string -> string * int * int * int

let sites' = ["www.bing.com"; "www.google.com"; "search.yahoo.com"];;
//val sites': string list =
//   ["www.bing.com"; "www.google.com"; "search.yahoo.com"]

sites' |> List.map getStats;;
//val it: (string * int * int * int) list =
//  [("www.bing.com", 44704, 2495, 18);
//   ("www.google.com", 55561, 3218, 29);
//   ("search.yahoo.com", 96268, 5821, 49)]


// map type

List.map
//val it: (('a -> 'b) -> 'a list -> 'b list)

Array.map
//val it: (('a -> 'b) -> 'a array -> 'b array)

Option.map
//val it: (('a -> 'b) -> 'a option -> 'b option)

Seq.map
//val it: (('a -> 'b) -> 'a seq -> 'b seq)


// Using Fluent Notation on Collections

(|>)
//val it: ('a -> ('a -> 'b) -> 'b)


// Composing Functions with >>

let google = http "https://www.google.com"
//val google: string =
//  "<!doctype html><html itemscope="" itemtype="http://schema.org"+[55498 chars]


google |> getWords |> Array.filter (fun s -> s = "href") |> Array.length
//val it: int = 29

// Rewrite using function composition

let countLinks = getWords >> Array.filter (fun s -> s = "href") >> Array.length
//val countLinks: (string -> int

google |> countLinks
//val it: int = 29

let (>>) f g x = g(f(x))
//val (>>) : f: ('a -> 'b) -> g: ('b -> 'c) -> x: 'a -> 'c


// Building Functions with Partial Application

// An example, with x and y in Cartesian coordinates

let shift (dx, dy) (px, py) = (px + dx, py + dy)
//val shift: dx: int * dy: int -> px: int * py: int -> int * in


// Building with partial application

let shiftRight = shift (1, 0)
let shiftUp = shift (0, 1)
let shiftLeft = shift (-1, 0)
let shiftDown = shift (0, -1)

//val shiftRight: (int * int -> int * int)
//val shiftUp: (int * int -> int * int)
//val shiftLeft: (int * int -> int * int)
//val shiftDown: (int * int -> int * int)


shiftRight (10, 10);;
//val it: int * int = (11, 10)

List.map (shift (2,2)) [(0,0); (1,0); (1,1); (0,1)];;
//val it: (int * int) list = [(2, 2); (3, 2); (3, 3); (2, 3)]


// Using Local Functions

open System.Drawing

let remap (r1:RectangleF) (r2:RectangleF) =
     let scalex = r2.Width / r1.Width
     let scaley = r2.Height / r1.Height
     let mapx x = r2.Left + (x - r1.Left) * scalex
     let mapy y = r2.Top + (y - r1.Top) * scaley
     let mapp (p:PointF) = PointF(mapx p.X, mapy p.Y)
     mapp

let rect1 = RectangleF(100.0f, 100.0f, 100.0f, 100.0f)
let rect2 = RectangleF(50.0f, 50.0f, 200.0f, 200.0f)

let mapp = remap rect1 rect2

//val remap: r1: RectangleF -> r2: RectangleF -> (PointF -> PointF)
//val rect1: RectangleF = {X=100,Y=100,Width=100,Height=100}
//val rect2: RectangleF = {X=50,Y=50,Width=200,Height=200}
//val mapp: (PointF -> PointF)


mapp (PointF(100.0f, 100.0f));;
mapp (PointF(150.0f, 150.0f));;
mapp (PointF(200.0f, 200.0f));;

//val it: PointF = {X=50, Y=50} 
//val it: PointF = {X=150, Y=150}
//val it: PointF = {X=250, Y=250} 


// Iterating with Functions

let sites3 = ["https://www.bing.com";
                        "https://www.google.com";
                        "https://search.yahoo.com"]

sites3 |> List.iter  (fun site -> printfn "%s, length = %d" site (http site).Length)

//https://www.bing.com, length = 44695
//https://www.google.com, length = 55482
//https://search.yahoo.com, length = 96288
//val it: unit = ()


// Abstracting Control with Functions

open System;;

let start = DateTime.Now;;
//val start: DateTime = 8/30/2023 2:03:07 PM

http "https://www.newscientist.com";
// blah blah blah

let finish = DateTime.Now
//> http "http://www.newscientist.com";

let elapsed = finish - start
//val elapsed: TimeSpan = 00:02:07.3426525


// A new control operator

open System

let time f =
    let start = DateTime.Now
    let res = f()
    let finish = DateTime.Now
    (res, finish - start)
//val time: f: (unit -> 'a) -> 'a * TimeSpan

time (fun () -> http "https://www.newscientist.com");;


// Using Object Methods as First-Class Functions

open System.IO

[ "file1.txt"; "file2.txt"; "file3.sh" ]
    |> List.map Path.GetExtension
//val it: string list = [".txt"; ".txt"; ".sh"]


// Sometimes you need to add extra type information to indicate which overload
// of the method is required.

let f = (Console.WriteLine : string -> unit);;
// A unique overload for method 'WriteLine' could not be determined based on
// type information prior to this program point. A type annotation may be needed.

//val f: arg00: string -> unit

