// Measuring Up


[<Measure>] type ft
[<Measure>] type inch = static member perFoot = 12.0<inch/ft>


// Generic Measures

let square (v : float<_>) = v * v

let area1 = square 10.0<inch>
let area2 = square 10.0<ft>
let area3 = square 10.0


printfn "%f" area1
printfn "%f" area2
printfn "%f" area3
