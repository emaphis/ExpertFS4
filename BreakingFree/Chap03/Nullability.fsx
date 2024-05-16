// Options

let middleName0 : string option = None;
let middleName1 = Some "William"


type Container() =
  member x.Fill ?stopAtPercent =
    printfn "%s" <|
            match (defaultArg stopAtPercent 0.5) with
            | 1.0 -> "Filled it up"
            | stopAt -> sprintf "Filled to %s" (stopAt.ToString("P2"))

let bottle = Container()
 bottle.Fill 50


// Unit Type

let add a b = a + b
do add 2 3 |> ignore
