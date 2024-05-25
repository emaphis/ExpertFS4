// Quotations

// Composing Quoted Expressions

open Microsoft.FSharp.Quotations

let x, y = 10, 10
let expr = <@ x * y @>

let expr2 = <@ fun a b -> a * b @>

let expr3 =
    <@ let mult x y = x * y
    mult 10 20 @>


// .NET Reflection
// Another way to create a quoted expression is through standard .NET
// reflection.

type Calc =
    [<ReflectedDefinition>]
    static member Multiply x y = x * y

let expr4 =
    typeof<Calc>
      .GetMethod("Multiply")
    |> Expr.TryGetReflectedDefinition


// Manual Composition

let operators =
    System.Type.GetType("Microsoft.FSharp.Core.Operators, FSharp.Core")


let multiplyOperator = operators.GetMethod("op_Multiply")

let varX, varY =
    multiplyOperator.GetParameters()
    |> Array.map (fun p -> Var(p.Name, p.ParameterType))
    |> (function
        | [| x; y |] -> x, y
        | _ -> failwith "not supported")

let call = Expr.Call(multiplyOperator, [ Expr.Var(varX); Expr.Var(varY) ])

let innerLambda = Expr.Lambda(varY, call)
let outerLambda = Expr.Lambda(varX, innerLambda)

let what1 = <@@ fun x y -> x * y @@>

// See: AllTheParts.fsx


// Splicing Quoted Expressions

let numbers = seq { 1..10 }
let sum = <@ Seq.sum numbers @>
let count = <@ Seq.length numbers @>
let avgExpr = <@ %sum / %count @>

;;
