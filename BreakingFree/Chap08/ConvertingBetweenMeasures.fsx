// Measuring Up.

// Converting Between Measures

// Static Conversion Factors

[<Measure>] type ft
[<Measure>] type inch = static member perFoot = 12.0<inch/ft>

let inches1 = 2.0<ft> * inch.perFoot
printfn "%f" inches1

let feet1 = 36.0<inch> / inch.perFoot
printfn "%f" feet1


// Static Conversion Functions
[<Measure>]
type f =
    static member toCelsius (t: float<f>) = ((float t - 32.0) * (5.0/9.0)) * 1.0<c>
    static member fromCelsius (t: float<c>) = ((float t * (9.0/5.0)) + 32.0) * 1.0<f>
and
    [<Measure>]
    c =
        static member toFahrenheit = f.fromCelsius
        static member fromFahrenheit = f.toCelsius

let fTemp1 = 32.0<f>
let cTemp1 = f.toCelsius fTemp1
