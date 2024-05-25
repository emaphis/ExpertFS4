// Measuring Up 

// Applying Measures

[<Measure>] type lb
[<Measure>] type ft
[<Measure>] type lbft = lb ft
[<Measure>] type m
[<Measure>] type h
[<Measure>] type mph = m / h
[<Measure>] type sqft = ft ^ 2


let length1 = 10.0<ft>
let area1 = 10.0<sqft>


[<Measure>] type dpi

let resolution = 300.0 * 1.0<dpi>

// Mover verbous example

let resolution2 = LanguagePrimitives.FloatWithMeasure<dpi> 300.0
