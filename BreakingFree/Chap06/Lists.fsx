// Collections - Lists

// Lists

// Creating Lists

let names = [ "Rose"; "Martha"; "Donna"; "Amy"; "Clara" ]

let numbers = [ 1..11 ]
printfn "%A" numbers

// Working with Lists

// Accessing Elements

// List.nth is depreciated
let char1 = List.item 3 [ 'A' .. 'Z' ]
printfn "%c" char1


let head1 = List.head names
let tail1 = List.tail names

printfn "%s, %A" head1 tail1

// head and tail are useful for recursing over lists

//let rec contains (fn : 'a -> bool) (l : 'a list) : bool =
let rec contains fn l =
    if l = [] then false
    else fn(List.head l) || contains fn (List.tail l)
;;

let cont1 = names |> contains (fun name -> name = "Donna")
printfn "%b" cont1

let cont2 = [] |> contains (fun name -> name = "Rose")
printfn "%b" cont2


// Combining Lists

let newNames = "Ace" :: names

printfn "%A" newNames

let classicNames = [ "Susan"; "Barbara"; "Sarah Jane" ]
let modernNames = [ "Rose"; "Martha"; "Donna"; "Amy"; "Clara" ]

let combNames1 = classicNames @ modernNames

let combNames2 =
    List.concat  [[ "Susan"; "Sarah Jane" ]
                  [ "Rose"; "Martha" ]
                  ["Donna"; "Amy"; "Clara"]];
