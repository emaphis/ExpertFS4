// Chapter 6
// Programming with Objects

module Script1

// Getting Started with Objects and Members

// Listing 6-1. A Vector2D record type with object members

/// Two-dimensional vectors
type Vector2D =
    { DX : float; DY : float }

    /// Get the length of the vector
    member v.Length = sqrt(v.DX * v.DX + v.DY * v.DY)

    /// Return a vector scaled by the given factor
    member v.Scale k = { DX = k * v.DX; DY = k * v.DY }

    /// Return a vector shifted by the given delta in the X coordinate
    member v.ShiftX x = { v with DX = v.DX + x }
    
    /// Return a vector shifted by the given delta in the Y coordinate
    member v.ShiftY y = { v with DY = v.DY + y }

    // --- it’s conventional for methods to take their arguments in tupled form
    /// Return a vector shifted by the given delta in both coordinates
    member v.ShiftXY (x, y) = { DX = v.DX + x; DY = v.DY + y }

    /// Get the zero vector
    static member Zero = { DX = 0.0; DY = 0.0 }

    /// Return a constant vector along the X axis
    static member ConstX dx = { DX = dx; DY = 0.0 }

    /// Return a constant vector along the Y axis
    static member ConstY dy = { DX = 0.0; DY = dy } 

//type Vector2D =
//  {
//    DX: float
//    DY: float
//  }
//  member Scale: k: float -> Vector2D
//  member ShiftX: x: float -> Vector2D
//  member ShiftXY: x: float * y: float -> Vector2D
//  member ShiftY: y: float -> Vector2D
//  static member ConstX: dx: float -> Vector2D
//  static member ConstY: dy: float -> Vector2D
//  member Length: float
//  static member Zero: Vector2D


// use this as such:

//let v1 = { DX = 3.0; DY = 4.0 };;
//v1.Length;;
//v1.Scale(2.0).Length;;
//Vector2D.ConstX(3.0);;

//val v1: Vector2D = { DX = 3.0
//                     DY = 4.0 }

//val it: float = 5.0

//val it: float = 10.0

//val it: Vector2D = { DX = 3.0
//                     DY = 0.0 }



// Discrimated unions can also act a classes with members

/// A type of binary tree, generic in the type of values carried at nodes
type Tree<'T> =
    | Node of 'T * Tree<'T> * Tree<'T>
    | Tip

    /// Compute the number of values in the tree
    member t.Size =
        match t with
        | Node(_, l, r) -> 1 + l.Size + r.Size
        | Tip -> 0

//type Tree<'T> =
//  | Node of 'T * Tree<'T> * Tree<'T>
//  | Tip
//  member Size: int


// Using Classes - the class type

/// Two-dimensional vectors
type Vector2D'(dx : float, dy : float) =

    let len = sqrt(dx * dx + dy * dy)

    /// Get the X component of the vector
    member v.DX = dx

    /// Get the Y component of the vector
    member v.DY = dy

    /// Get the length of the vector
    member v.Length = len

    /// Return a vector scaled by the given factor
    member v.Scale(k) = Vector2D'(k * dx, k * dy)

    /// Return a vector shifted by the given delta in the X coordinate
    member v.ShiftX x = Vector2D'(dx = dx + x, dy = dy)

    /// Return a vector shifted by the given delta in the Y coordinate
    member v.ShiftY y = Vector2D'(dx = dx, dy = dy + y)

    // --- it’s conventional for methods to take their arguments in tupled form
    /// Return a vector shifted by the given delta in both coordinates
    member v.ShiftXY (x, y) = Vector2D'(dx = dx + x, dy = dy + y)

    /// Get the zero vector
    static member Zero = Vector2D'(dx = 0.0, dy = 0.0)

    /// Return a constant vector along the X axis
    static member OneX  = Vector2D'( dx = 1.0, dy = 0.0 )

    /// Return a constant vector along the Y axis
    static member OneY  = Vector2D'(dx = 0.0, dy = 0.0)

//type Vector2D' =
//  new: dx: float * dy: float -> Vector2D'
//  member Scale: k: float -> Vector2D'
//  member ShiftX: x: float -> Vector2D'
//  member ShiftXY: x: float * y: float -> Vector2D'
//  member ShiftY: y: float -> Vector2D'
//  member DX: float
//  member DY: float
//  member Length: float
//  static member OneX: Vector2D'
//  static member OneY: Vector2D'
//  static member Zero: Vector2D'
 
// Use the type as follows

let v2 = Vector2D'(3.0, 4.0);;
v2.Length;;
v2.Scale(2.0).Length;;

//val v2: Vector2D'
//val it: float = 5.0
//val it: float = 10.0


// Using constructor sequences to enforce invariants
// Define a vector type that checks that its length is close to 1.0 and 
// refuses to construct an instance of the value if not:

/// Vectors whose length is chekced to be close to 1.
type UnitVector2D (dx, dy) =
    let tolerance = 0.000001

    let length = sqrt(dx * dx + dy * dy)
    do if abs (length - 1.0) > tolerance then failwith "not a unit vector"

    member v.DX = dx

    member v.DY = dy

    new() = UnitVector2D (1.0, 0.0)

//type UnitVector2D =
//  new: unit -> UnitVector2D + 1 overload
//  member DX: float
//  member DY: float


/// A class including some static bindings
type Vector2Ds(dx : float, dy : float) =

    static let zero = Vector2Ds(0.0, 0.0)
    static let onex = Vector2Ds(1.0, 0.0)
    static let oney = Vector2Ds(0.0, 1.0)

    /// Get the zero vector
    static member Zero = zero

    /// Get a constant vector along the x axis of length
    static member OneX = onex

    /// Get a constant vector along the Y axis of length one
    static member OneY = oney


// Adding Further Object Notation to Your Types

// Working with Indexer Properties

open System.Collections.Generic

type SparseVector(items : seq<int * float>) =
    let elems = new SortedDictionary<_,_>()
    do items |> Seq.iter (fun (k, v) -> elems.Add(k, v))

    /// This defines the indexer property
    member t.Item
        with get(idx) =
            if elems.ContainsKey(idx) then elems.[idx]
            else 0.0

//type SparseVector =
//  new: items: (int * float) seq -> SparseVector
//  member Item: idx: int -> float with get


let v3 = SparseVector [(3, 547.0)];;
//val v3: SparseVector

v3.[4];;
//val it: float = 0.0

v3.[3];;


// Adding Overloaded Operators

type Vector2DWithOperators(dx : float, dy : float) =
    member x.DX = dx
    member x.DY = dy

    static member (+) (v1 : Vector2DWithOperators, v2 : Vector2DWithOperators) =
        Vector2DWithOperators(v1.DX + v2.DX, v1.DY + v2.DY)

    static member (-) (v1 : Vector2DWithOperators, v2 : Vector2DWithOperators) =
        Vector2DWithOperators (v1.DX - v2.DX, v1.DY - v2.DY)

//type Vector2DWithOperators =
//  new: dx: float * dy: float -> Vector2DWithOperators
//  static member
//    (+) : v1: Vector2DWithOperators * v2: Vector2DWithOperators ->
//            Vector2DWithOperators
//  static member
//    (-) : v1: Vector2DWithOperators * v2: Vector2DWithOperators ->
//            Vector2DWithOperators
//  member DX: float
//  member DY: float


let v4 = new Vector2DWithOperators (3.0, 4.0);;
//val v4: Vector2DWithOperator

v4 + v4;;
//val it: Vector2DWithOperators = FSI_0032+Vector2DWithOperators
//    {DX = 6.0;
//    DY = 8.0;}

v4 - v4;;
//val it: Vector2DWithOperators = FSI_0032+Vector2DWithOperators
//    {DX = 0.0;
//    DY = 0.0;}


// Using Named and Optional Arguments

open System.Drawing

type LabelInfo(?text : string, ?font : Font) =
    let text = defaultArg text ""
    let font = match font with
                    | None -> new Font(FontFamily.GenericSansSerif, 12.0f)
                    | Some v -> v
    member x.Text = text
    member x.Font = font


// Adding Method Overloading

// Listing 6-3. An Interval type with overloaded methods

/// Interval(lo,hi) represents the range of numbers from lo to hi,
/// but not including either lo or hi.
type Interval(lo, hi) =
    member r.Lo = lo
    member r.Hi = hi
    member r.IsEmpty = hi <= lo
    member r.Contains  v = lo < v && v < hi

    static member Empty = Interval(0.0, 0.0)

    /// Return the smallest interval that covers both the intervals
    static member Span (r1: Interval, r2: Interval) =
        if r1.IsEmpty then r2 else
        if r2.IsEmpty then r1 else
        Interval(min r1.Lo r2.Lo, max r1.Hi r2.Hi)

    /// Return the smallest interval that covers all the intervals
    static member Span(ranges : seq<Interval>) =
        Seq.fold (fun r1 r2 -> Interval.Span(r1, r2)) Interval.Empty ranges

//type Interval =
//  new: lo: float * hi: float -> Interval
//  member Contains: v: float -> bool
//  static member Span: ranges: Interval seq -> Interval + 1 overload
//  member Hi: float
//  member IsEmpty: bool
//  member Lo: float
//  static member Empty: Interval


// multiple methods can also have the same number of arguments and be overloaded by type

type Vector =
    { DX : float; DY : float }
    member v.Length = sqrt( v.DX * v.DX + v.DY * v.DY)


type Point =
    { X : float; Y : float }

    static member (-) (p1 : Point, p2 : Point) =
        { DX = p1.X - p2.X; DY = p1.Y - p2.Y}

    static member (-) (p: Point, v : Vector) =
        { X = p.X - v.DX; Y = p.Y - v.DY }


// Defining Object Types with Mutable State

// Listing 6-4. An Object Type with State

type MutableVector2D(dx : float, dy : float) =
    let mutable currDX = dx
    let mutable currDY = dy

    member vec.DX with get() = currDX and set v = currDX <- v
    member vec.DY with get() = currDY and set v = currDY <- v

    member vec.Length
        with get () = sqrt (currDX * currDX + currDY * currDY)
        and set theta =
            let len = vec.Length
            currDX <- cos theta * len
            currDY <- sin theta * len

    member vec.Angle
        with get () = atan2 currDY currDX
        and set theta =
            let len = vec.Length
            currDX <- cos theta * len
            currDY <- sin theta * len

//type MutableVector2D =
//      new: dx: float * dy: float -> MutableVector2D
//      member Angle: float with get, set
//      member DX: float with get, set
//      member DY: float with get, set
//      member Length: float with get, set


let v5 = MutableVector2D(3.0, 4.0);;
//val v5: MutableVector2D

(v5.DX, v5.DY);;
//val it: float * float = (3.0, 4.0)

(v5.Length, v5.Angle);;
//val it: float * float = (5.0, 0.927295218)

v5.Angle <- System.Math.PI / 6.0
(v5.DX, v5.DY);;
//val it: float * float = (4.330127019, 2.5)

(v5.Length, v5.Angle);;
//al it: float * float = (5.0, 0.5235987756)


// If the type has an indexer (Item) property, then you write an indexed setter as follows:

open System.Collections.Generic

type IntegerMatrix(rows : int, cols : int) =
    let elems = Array2D.zeroCreate<int> rows cols

    /// This defines an indexer property with getter and setter
    member t.Item
        with get (idx1, idx2) = elems.[idx1, idx2]
        and set (idx1, idx2) v = elems.[idx1, idx2] <- v

//type IntegerMatrix =
//  new: rows: int * cols: int -> IntegerMatrix
//  member Item: idx1: int * idx2: int -> int with get


// Using Optional Property Settings

open System.Windows.Forms

let form' = 
    new Form(Visible = true, TopMost = true, Text = "Welcome to F#")

let form =
    let tmp = new Form()
    tmp.Visible <- true
    tmp.TopMost <- true
    tmp.Text <- "Welcome to F#"
    tmp


open System.Drawing

type LabelInfoWithPropertySetting() =
     let mutable text = "" // the default
     let mutable font = new Font(FontFamily.GenericSansSerif, 12.0f)
     member x.Text with get() = text and set v = text <- v
     member x.Font with get() = font and set v = font <- v

//type LabelInfoWithPropertySetting =
//  new: unit -> LabelInfoWithPropertySetting
//  member Font: Font with get, set
//  member Text: string with get, set

let labelInfo = LabelInfoWithPropertySetting(Text = "Hello, World")
// val labelInfo: LabelInfoWithPropertySetting

let form2 = new Form(Visible = true, TopMost = true, Text = "Welcome to F#")


// Declaring Auto-Properties
// auto-property declaration has the form member val id = expr followed by an optional with get,set

type LabelInfoWithPropertySetting2() =
    member val Name = "Label"
    member val Text = "" with get, set
    member val Font = new Font(FontFamily.GenericSansSerif, 12.0f) with get, set

//type LabelInfoWithPropertySetting2 =
//    new: unit -> LabelInfoWithPropertySetting2
//    member Font: Font with get, set
//    member Name: string
//    member Text: string with get, set


// Getting Started with Object Interface Types

// Listing 6-5. An object interface type IShape and some implementations

open System.Drawing

type IShape =
    abstract Contains : Point -> bool
    abstract BoundingBox : Rectangle

// Defining New Object Interface Types

let circle (center: Point, radius : int) =
    { new IShape with
        
        member x.Contains(p: Point) = 
            let dx = float32 (p.X - center.X)
            let dy = float32 (p.Y - center.Y)
            sqrt(dx * dx + dy * dy) <= float32 radius

        member x.BoundingBox: Rectangle = 
            Rectangle(
                center.X - radius,
                center.Y - radius,
                2 * radius + 1,
                2 * radius + 1)
        }


let square (center : Point, side : int) =
    { new IShape with

        member x.Contains(p: Point) =
            let dx = p.X - center.X
            let dy = p.Y - center.Y
            abs(dx) < side / 2 && abs(dy) < side / 2

        member x.BoundingBox =
            Rectangle(center.X - side, center.Y - side, side * 2, side * 2)
    }


// Defining New Object Interface Types

type MutableCircle() =
     member val Center = Point(x = 0, y = 0) with get, set
     member val Radius = 10 with get, set
     member c.Perimeter = 2.0 * System.Math.PI * float c.Radius
 
     interface IShape with

        member c.Contains(p : Point) =
            let dx = float32 (p.X - c.Center.X)
            let dy = float32 (p.Y - c.Center.Y)
            sqrt(dx * dx + dy * dy) <= float32 c.Radius

        member c.BoundingBox =
            Rectangle(
            c.Center.X - c.Radius, c.Center.Y - c.Radius,
            2 * c.Radius + 1, 2 * c.Radius + 1)


let bigCircle = circle(Point(0, 0), 100);;
//val bigCircle: IShape

bigCircle.BoundingBox;;
//val it: Rectangle =
//  {X=-100,Y=-100,Width=201,Height=201}

 bigCircle.Contains(Point(70, 70));;
 //val it: bool = true

bigCircle.Contains(Point(71, 71));;
//val it: bool = false


let smallSquare = square(Point(1, 1), 1);;
//val smallSquare: IShape

smallSquare.BoundingBox;;
//val it: Rectangle = {X=0,Y=0,Width=2,Height=2}

smallSquare.Contains(Point(0, 0));;
//val it: bool = false


// You can now reveal the interface (through a type cast) and use its members:

let circle2 = MutableCircle();;
//val circle2: MutableCircle

circle2.Radius;;
//val it: int = 10

(circle2 :> IShape).BoundingBox;;
//val it: Rectangle =
//  {X=-10,Y=-10,Width=21,Height=21}


// Using Common Object Interface Types from the .NET Libraries

//type IEnumerator<'T> =
//    abstract Current : 'T
//    abstract MoveNext : unit -> bool

//type IEnumerable<'T> =
//     abstract GetEnumerator : unit -> IEnumerator<'T>


// More Techniques for Implementing Objects

// Combining Object Expressions and Function Parameters

/// An object interface type that consume characters and string
type ITextOutputSink =
    
    /// When implemented, writes one Unicode character to the sink
    abstract WriteChar : char -> unit

    /// When immplemented, writes one Unicode string to the sink
    abstract WriteString : string -> unit


/// Return an object that implements ITextOutputSink using writeCharFunction
let simpleOutputSink writeCharFunction=
    { new ITextOutputSink with
        member x.WriteChar(c) = writeCharFunction c
        member x.WriteString(s) = s |> String.iter x.WriteChar }

let stringBuilderOutputSink (buf : System.Text.StringBuilder ) =
    simpleOutputSink (fun c -> buf.Append(c) |> ignore)


open System.Text

let buf1 = new StringBuilder();;
//al buf1: StringBuilder = 

let c = stringBuilderOutputSink(buf1);;
//val c: ITextOutputSink

["Incy"; " "; "Wincy"; " "; "Spider"] |> List.iter c.WriteString
//val it: unit = ()

buf1.ToString();;
//val it: string = "Incy Wincy Spider"
