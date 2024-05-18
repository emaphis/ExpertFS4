// Functional Programming - Functional Types

// Functional Types

// Tuples

let point1 = 10.0, 10.0

let point2 = (20.0, 20.0)

let point3 : float * float = 0.0, 0.0

// Extracting Values

let slopeA p1 p2 =
    let x1 = fst p1
    let y1 = snd p1
    let x2 = fst p2
    let y2 = snd p2
    (y1 - y2) / (x1 - x2)

let slp1 = slopeA (13.0, 8.0) (1.0, 2.0)

// Tuple patterns

let slopeB p1 p2 =
    let x1, y1 = p1
    let x2, y2 = p2
    (y1 - y2) / (x1 - x2)

let slp2 = slopeB (13.0, 8.0) (1.0, 2.0)

// extract 'z'
let _, _, x = (10.0, 10.0, 10.0)

// Patterns for parameters

let slopeC (x1, y1) (x2, y2) = (y1 - y2) / (x1 - x2)

let slp3 = slopeC (13.0, 8.0) (1.0, 2.0)

// Equality Semantics

let bl1 = (1, 2) = (1, 2)

let bl2 = (2, 1) = (1, 2)

// Syntatic Tuples

//System.String.Format "hello {0}" "Dave"
// error FS0003: This value is not a function and cannot be applied.

let str1 = System.String.Format("hello {0}", "Dave")

// Out Parameters

// C#
// u int v;
// var r = System.Int32.TryParse("10", out v);

// F#
let r, v = System.Int32.TryParse "10"
printfn "parsed: %b, %i" r v


// Record Types

// Defining Record Types

type rgbColor = { R : byte; G : byte; B : byte }

// Creating Records

let red = { R = 255uy; G = 0uy; B = 0uy }

let red' =
    { R = 255uy
      G = 0uy
      B = 0uy }

// Avoiding Naming Conflicts

type rgbColorB = { R : byte; G : byte; B : byte }
type color = { R : byte; G : byte; B : byte }

let redB = { R = 255uy; G = 0uy; B = 0uy }  // most recent definition
let redB' : rgbColorB = { R = 255uy; G = 0uy; B = 0uy }


// Copying Records
let redC = { R = 255uy; G = 0uy; B = 0uy }
let yellowC = { redC with G = 255uy }


// Mutability

type rgbColorC =
    { mutable R1 : byte
      mutable G1 : byte
      mutable B1 : byte }

let myColorC = { R1 = 255uy; G1 = 255uy; B1 = 255uy }


// Additional Members

open System

type rgbColorD =
    { R2 : byte; G2 : byte; B2 : byte }
    member x.ToHexString() =
        sprintf "#%02X%02X%02X" x.R2 x.G2 x.B2
    static member Red = { R2 = 255uy; G2 = 0uy; B2 = 0uy }
    static member Green = { R2 = 0uy; G2 = 255uy; B2 = 0uy }
    static member Blue = { R2 = 0uy; G2 = 0uy; B2 = 255uy }
    static member (+) (l : rgbColorD, r : rgbColorD) =
         { R2 = Math.Min(255uy, l.R2 + r.R2)
           G2 = Math.Min(255uy, l.G2 + r.G2)
           B2 = Math.Min(255uy, l.B2 + r.B2) }

let redD = { R2 = 255uy; G2 = 0uy; B2 = 0uy }
let hexS1 = redD.ToHexString()

let hexS2 = rgbColorD.Red.ToHexString()

let yellowD =
   { R2 = 255uy; G2 = 0uy; B2 = 0uy } +
       { R2 = 0uy; G2 = 255uy; B2 = 0uy }
