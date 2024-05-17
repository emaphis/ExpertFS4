// Classes - Structures

// Structures
// Value Objects,

[<Struct>]
type CircleD(diameter : float) =
 member x.getRadius() = diameter / 2.0
 member x.Diameter = diameter
 member x.GetArea() = System.Math.PI * (x.getRadius() ** 2.0)


let circD = CircleD 5.0
printfn "Radius = %f" (circD.getRadius())
printfn "Diameter = %f" circD.Diameter
printfn "Area = %f" (circD.GetArea())
