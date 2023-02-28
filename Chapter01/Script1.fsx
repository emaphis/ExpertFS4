

#I @"C:\users\emaph\.nuget\packages\"
#r @"fsharp.data\4.2.8\lib\netstandard2.0\FSharp.Data.dll"

open FSharp.Data

[<Literal>]
let F1_2017_URL =
    "https://en.wikipedia.org/wiki/2017_FIA_Formula_One_World_Championship"

type F1_2017 = HtmlProvider<F1_2017_URL>

// Download the table for the 2017 F1 calendar from Wikipedia
let f1Calendar = F1_2017.Load(F1_2017_URL).Tables.``Season calendarEdit``

// Look at the top row, being the first race of the calendar
let firstRow = f1Calendar.Rows |> Seq.head
let round = firstRow.Round
let grandPrix = firstRow.``Grand Prix``
let date = firstRow.Date


// Print the round, location and date for each race, corresponding to a row
for row in f1Calendar.Rows do
    printfn "Race, round %A is hosted at %A on %A" row.Round row.``Grand Prix`` row.Date



[<Literal>]
let Species_url = "https://en.wikipedia.org/wiki/The_world's_100_most_threatened_species"

type SPECIES = HtmlProvider<Species_url>

let table = SPECIES.Load(Species_url).Tables
