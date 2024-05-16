// Exceptions - When Things Go Wrong

// Handling Exceptions

// try ... with Expressions

open System.IO

let current = Directory.GetCurrentDirectory()
//val current: string = "C:\Users\emaph\AppData\Local\Temp\"

try
    use file = File.OpenText "input.txt"
    let strings = file.ReadToEnd() |> printfn "%s"
    printfn "%A" strings
with
    | :? FileNotFoundException -> printfn "File not found"
    | _ -> printfn "Error loading file"

// Examining the Exception

try
    use file = File.OpenText "input.txt"
    let strings = file.ReadToEnd() |> printfn "%s"
    printfn "%A" strings
with
    | :? FileNotFoundException as ex ->
        printfn "%s file was not found" ex.FileName
    | _ -> printfn "Error loading file"


// Combining cases

open System

try
    use file = File.OpenText "input.txt"
    let strings = file.ReadToEnd() |> printfn "%s"
    printfn "%A" strings
with
    | :? FileNotFoundException as ex ->
        printfn "%s file was not found" ex.FileName
    | :? PathTooLongException
    | :? ArgumentNullException
    | :? ArgumentException ->
        printfn "Invalid filename"
    | _ -> printfn "Error loading file"


// Reraising an Exception after partialy handling it.

try
    use file = File.OpenText "input.txt"
    let strings = file.ReadToEnd() |> printfn "%s"
    printfn "%A" strings
with
    | :? FileNotFoundException as ex ->
        printfn "%s file was not found" ex.FileName
    | _ ->
        printfn "Error loading file"
        reraise()


// try ... with  is an expression so returns a value:

open System
open System.Diagnostics
open System.IO

let fileContents =
    try
        use file = File.OpenText "input.txt"
        Some <| file.ReadToEnd()
    with
        | :? FileNotFoundException as ex ->
            printfn "%s file was not found" ex.FileName
            None
        | _ ->
            printfn "Error loading file"
            reraise()


// try. . .finally Expressions

let fileValue =
    try
        use file = File.OpenText "input.txt"
        Some <| file.ReadToEnd()
    finally
        printfn "cleaning up"


// Raising Exceptions

let filename = "x"

if not (File.Exists filename) then
    raise <| FileNotFoundException("filename was null or empty")

// failwith
if not (File.Exists filename) then
    failwith "File not found"

// failwithf
if not (String.IsNullOrEmpty filename) then
    failwithf "%s could not be found" filename


// Custom Exceptions

type MyException(message, category) =
    inherit exn(message)
    member x.Category = category
    override x.ToString() = sprintf "[%s] %s" category message

try
    raise <| MyException("blah blah", "debug")
with
    | :? MyException as ex -> printfn "My Exception: %s" <| ex.ToString()
    | _ as ex -> printfn "General Exception: %s" <| ex.ToString()


// Lightweight exception creation
exception RetryAttemptFailed of string * int
exception RetryCountExceeded of string

let retry maxTries action =
    let rec retryInternal attempt =
        try
            if not (action()) then
                raise <| if attempt > maxTries then
                            RetryCountExceeded("Maximum attempts exeeded.")
                         else
                            RetryAttemptFailed(sprintf "Attempt %i failed" attempt, attempt)
        with
            | RetryAttemptFailed(msg, count) as ex ->
                        Console.WriteLine(msg)
                        retryInternal (count + 1)
            | RetryCountExceeded(msg) ->
                        Console.WriteLine(msg)
                        reraise()
    retryInternal 1

try
    retry 5 (fun() -> false)
with
    | :? RetryCountExceeded as ex -> printfn "Exception: %s" (ex.Message)
