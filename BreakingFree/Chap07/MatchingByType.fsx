// Patterns  - Matching by Type

// Matching by Type

// Type-Annotated Patterns

// Does not compile
//let startsWithUpperCase' =
//    function
//    | s when s.Length > 0 && s[0] = System.Char.ToUpper s[0] -> true
//    | _ -> false

// Type-annotation

let startsWithUpperCase =
    function
    | (s : string) when s.Length > 0 && s[0] = System.Char.ToUpper s[0] -> true
    | _ -> false


// Dynamic Type-Test Patterns

type RgbColor = { R : int; G : int; B : int }
type CmykColor = { C : int; M : int; Y : int; K : int }
type HslColor = { H : int; S : int; L : int }

let detectColorSpace (cs : obj) =
    match cs with
    | :? RgbColor -> printfn "RGB"
    | :? CmykColor -> printfn "CMYK"
    | :? HslColor -> printfn "HSL"
    | _  -> failwith "Unrecognized"
