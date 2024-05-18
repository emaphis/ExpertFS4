// Functional Programming  - Discriminated Unions

// Discriminated Unions

//type Option<'T> =
//| None
//| Some of 'T

let showValue (v: _ option) =
    printfn "%s" (match v with
                  | Some x -> x.ToString()
                  | None -> "None")

Some 123 |> showValue
Some "abc" |> showValue
None |> showValue

// showValue "xyz"


// Defining Discriminated Unions

// Simple Object Hierarchies

type Shape =
    /// Describes a circle by its radius
    | Circle of float
    /// Describes a rectangle by its width and height
    | Rectangle of float * float
    /// Describes a triangle by its three sides
    | Triangle of float * float * float

let c1 = Circle(3.0)
let r1 = Rectangle(10.0, 12.0)
let t1 = Triangle(25.0, 20.0, 7.0)


type ShapeB =
    | CircleB of Radius : float
    | RectangleB of Width : float * Height : float
    | TriangleB of Leg1 : float * Leg2 : float * Leg3 : float


// Tree Structures

type Markup =
    | ContentElement of string * Markup list
    | EmptyElement of string
    | Content of string


let movieList =
    ContentElement("html",
        [ ContentElement("head", [ ContentElement("title", [ Content "Guilty Pleasures" ])])
          ContentElement("body",
            [ ContentElement("article",
                [ ContentElement("h1", [ Content "Some Guilty Pleasures" ])
                  ContentElement("p",
                    [ Content "These are "
                      ContentElement("strong", [ Content "a few" ])
                      Content " of my guilty pleasures" ])
                  ContentElement("ul",
                    [ ContentElement("li", [ Content "Crank (2006)" ])
                      ContentElement("li", [ Content "Starship Troopers (1997)" ])
                      ContentElement("li", [ Content "RoboCop (1987)" ])])])])])

let rec toHtml markup =
      match markup with
      | ContentElement (tag, children) ->
          use w = new System.IO.StringWriter()
          children
            |> Seq.map toHtml
            |> Seq.iter (fun (s : string) -> w.Write(s))
          sprintf "<%s>%s</%s>" tag (w.ToString()) tag
      | EmptyElement (tag) -> sprintf "<%s />" tag
      | Content (c) -> sprintf "%s" c

movieList |> toHtml

// See Movies.html


// Replacing Type Abbreviations

open System.IO

// Type alias
type HtmlString1 = string

let displayHtml1 (html: HtmlString1) =
     let fn = Path.Combine(Path.GetTempPath(), "HtmlDemo.htm")
     let bytes = System.Text.UTF8Encoding.UTF8.GetBytes html
     using
          (new FileStream(fn, FileMode.Create, FileAccess.Write))
          (fun fs -> fs.Write(bytes, 0, bytes.Length))
     System.Diagnostics.Process.Start(fn).WaitForExit()
     File.Delete fn

HtmlString1(movieList |> toHtml) |> displayHtml1


// Single-Case Discriminated Union
type HtmlString = | HtmlString of string

let displayHtml2 (HtmlString(html)) =
     let fn = Path.Combine(Path.GetTempPath(), "HtmlDemo.htm")
     let bytes = System.Text.UTF8Encoding.UTF8.GetBytes html
     using
          (new FileStream(fn, FileMode.Create, FileAccess.Write))
          (fun fs -> fs.Write(bytes, 0, bytes.Length))
     System.Diagnostics.Process.Start(fn).WaitForExit()
     File.Delete fn

HtmlString(movieList |> toHtml) |> displayHtml2


// Additional Members
// See Markup.fsx
