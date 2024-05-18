// Programming With Functions

// Functions as Data

// Interoperability Considerations

// If you need to pass Func and Action delegates into an F# assembly,
// you can use the following class to simplify the conversion.

open System.Runtime.CompilerServices

[<Extension>]
type public FSharpFunUtil =
    [<Extension>]
    static member ToFSharpFunc<'a, 'b> (func : System.Func<'a, 'b>) =
       fun x -> func.Invoke(x)
    [<Extension>]
    static member ToFSharpFunce<'a, 'b> (act: System.Action<'a>) =
        fun x -> act.Invoke(x)
