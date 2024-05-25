// Measuringm Up.

// Enforcing Measures

#load "MeasureFormulas.fsx"

[<Measure>] type ft
[<Measure>] type sqft = ft ^ 2


let getArea (w: float<ft>) (h: float<ft>) = w * h


// let area1 = getArea 10.0 10.0
// error FS0001: This expression was expected to have type 'float<ft>'    

let area2 = getArea 10.0<ft> 10.0<ft>
printfn "%A" area2


let getArea2 (w : float<ft>) (h : float<ft>) : float<sqft> = w * h;

let area3 = getArea2 10.0<ft> 10.0<ft>
printfn "%A" area3


// Ranges

let measureRange = [ 1.0<ft> .. 1.0<ft> .. 10.0<ft> ]
printfn "%A" measureRange
