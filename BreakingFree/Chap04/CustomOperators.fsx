// Classes  - Custom Operators

// Custom Operators

open System

type RgbColor(r, g, b) =
    member x.Red = r
    member x.Green = g
    member x.Blue = b
    override x.ToString() = sprintf "(%i, %i, %i)" r g b

// Prefix Operators

    /// Negative a color  TODO: Fix this prefix operator
    static member (~-) (r : RgbColor) =
        RgbColor (
          r.Red ^^^ 0xFF,
          r.Green ^^^ 0xFF,
          r.Blue ^^^ 0xFF
          )

// Infix Operators

    /// Add two colors
    static member (+) (l : RgbColor, r : RgbColor) =
        RgbColor(
            Math.Min(255, l.Red + r.Red),
            Math.Min(255, l.Green + r.Green),
            Math.Min(255, l.Blue + r.Blue)
           )

    /// Subtract two colors
    static member (-) (l : RgbColor, r : RgbColor) =
       RgbColor(
          Math.Max(0, l.Red - r.Red),
          Math.Max(0, l.Green - r.Green),
          Math.Max(0, l.Blue - r.Blue)
          )
// New Operators

    /// Blend two colors
    static member (+=) (l : RgbColor, r : RgbColor) =
        RgbColor(
            (l.Red + r.Red) / 2,
            (l.Green + r.Green) / 2,
            (l.Blue + r.Blue) / 2
        )


let red = RgbColor(255, 0, 0)
let green = RgbColor(0, 255, 0)
let yellow = red + green
let magenta = RgbColor(255, 0, 255)

let blue = magenta - red
let grey = yellow += blue
//let blue = ((~) yellow)
