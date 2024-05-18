// Functional Programming - Currying

let add a b = a + b
//  val add: a: int -> b: int -> int

let ten_five = add 10 5

// a function that takes a int an returns a function that takes an int and returns an int

let add' a = fun b -> (+) a b
//  val add': a: int -> b: int -> int

let ten_five' = add' 10 5

// Partial Application

let addTen = add 10
// val addTen: (int -> int)

let ten_five'' = addTen 5

printfn "%i, %i, %i" ten_five ten_five' ten_five''


// Pipelining

// Forward Pipelining
// (|>) :: ('a -> ('a -> 'b) -> 'b)

add 2 3 |> ignore

let fahrenheitToCelsius degreesF = (degreesF - 32.0) * (5.0 / 9.0)

let marchHighTemps =
    [ 33.0; 30.0; 33.0; 38.0; 36.0; 31.0; 35.0;
      42.0; 53.0; 65.0; 59.0; 42.0; 31.0; 41.0;
      49.0; 45.0; 37.0; 42.0; 40.0; 32.0; 33.0;
      42.0; 48.0; 36.0; 34.0; 38.0; 41.0; 46.0;
      54.0; 57.0; 59.0 ]

marchHighTemps
|> List.average
|> fahrenheitToCelsius
|> printfn "March Average (C): %f"

// Backward Pipelining
// (<|) :: (('a -> 'b) -> 'a -> 'b)

printfn "March Average (F): %f" <| List.average marchHighTemps


// Noncurried Functions
// Pipelining works with non-curried functions like methods

5.0
|> System.TimeSpan.FromSeconds
|> System.Threading.Thread.Sleep


// Functional Composition

let averageInCelsius = List.average >> fahrenheitToCelsius

printfn "March Average (C): %f" <| averageInCelsius  marchHighTemps

// Composing Functions from non-curried functions

let delay = System.TimeSpan.FromSeconds >> System.Threading.Thread.Sleep

do delay 5.0
