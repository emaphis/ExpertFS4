// Collections - Maps

// Maps

// Creating Maps

let stateCapitals =
     Map [("Indiana", "Indianapolis")
          ("Michigan", "Lansing")
          ("Ohio", "Columbus")
          ("Kentucky", "Frankfort")
          ("Illinois", "Springfield")]


// Working with Maps

// Finding Values

let capt1 = stateCapitals["Indiana"]
do printfn "%s" capt1

// Map.find let's us do the same thing functionally
let capt2 = stateCapitals |> Map.find "Indiana"
do printfn "%s" capt2

let bool1 = stateCapitals |> Map.containsKey "Washington"
do printfn "%b"  bool1

let capt3 = stateCapitals |> Map.tryFind "Washington"
do printfn "Not in Map %A" capt3

let capt4 = stateCapitals |> Map.tryFind "Indiana"
do printfn "%s" capt4.Value

// Finding Keys

let state1 = stateCapitals |> Map.tryFindKey (fun k v -> v = "Indianapolis")
do printfn "%s" state1.Value


let state2 = stateCapitals |> Map.tryFindKey (fun k v -> v = "Olympia")
do printfn "Not found %A" state2
