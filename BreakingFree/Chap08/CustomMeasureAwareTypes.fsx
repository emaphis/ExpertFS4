// Measuring Up.



[<Measure>] type ft
[<Measure>] type inch = static member perFoot = 12.0<inch/ft>


// Custom Measure-Aware Types

type Point< [<Measure>] 'u > = { X : float<'u>; Y : float<'u> } with
    member this.FindDistance other =
        let deltaX = other.X - this.X
        let deltaY = other.Y - this.Y
        sqrt ((deltaX * deltaX) + (deltaY * deltaY))


let p1 = { X = 10.0<inch>; Y = 10.0<inch> }

let d1 = p1.FindDistance  { X = 20.0<inch>; Y = 15.0<inch> }

printfn "%f" d1

//let d2 =  p1.FindDistance { X = 20.0<ft>; Y = 15.0<ft> };


// Unit of Measure aware class

type Point2< [<Measure>] 'u > (x : float<'u>, y : float<'u>) =
     member this.X = x
     member this.Y = y
     member this.FindDistance (other : Point2<'u>) =
         let deltaX = other.X - this.X
         let deltaY = other.Y - this.Y
         sqrt ((deltaX * deltaX) + (deltaY * deltaY))


let p2 = Point2(10.0<inch>, 10.0<inch> )

let d2 = p2.FindDistance(  Point2(20.0<inch>, 15.0<inch> ))

printfn "%f" d2
