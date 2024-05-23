// Active Patterns

// active recognizer

// let (|CaseName1|CaseName2|...|CaseNameN|) [parameters] -> expression


let (|Fizz|Buzz|FizzBuzz|Num|) n =
    match (n % 3, n % 5) with
    | 0, 0 -> FizzBuzz
    | 0, _ -> Fizz
    | _, 0 -> Buzz
    | _  -> Num n

let fizzBuzz =
     function
     | Fizz -> "Fizz"
     | Buzz -> "Buzz"
     | FizzBuzz -> "FizzBuzz"
     | Num n -> n.ToString()


seq { 1..100 }
    |> Seq.map fizzBuzz
    |> Seq.iter (printfn "%s")


// Partial Active Patterns

let (|Fizz|_|) n = if n % 3 = 0 then Some Fizz else None
let (|Buzz|_|) n = if n % 5 = 0 then Some Buzz else None

let fizzBuzz2 =
    function
    | Fizz & Buzz -> "FizzBuzz"
    | Fizz -> "Fizz"
    | Buzz -> "Buzz"
    | n -> n.ToString()

seq [ 1..100 ]
|> Seq.map fizzBuzz2
|> Seq.iter (printfn "%s")


// Parameterized Active Patterns

let (|DivisibleBy|_|) d n = if n % d = 0 then Some DivisibleBy else None

let fizzBuzz3 =
    function
    | DivisibleBy 3 & DivisibleBy 5 -> "FizzBuzz"
    | DivisibleBy 3  -> "Fizz"
    | DivisibleBy 5  -> "Buzz"
    | n -> n.ToString()

seq [ 1..100 ]
|> Seq.map fizzBuzz3
|> Seq.iter (printfn "%s")
