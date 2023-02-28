
#I @"C:\users\emaph\.nuget\packages\"
#r @"suave\2.6.2\lib\netstandard2.1\Suave.dll"

open Suave
open Suave.Http.Successful
open Suave.Web

let html =
    [ yield "<html><body><u1>"
      for (category,count) in speciesSorted do
        yield sprintf "<li>Category <b>%s</b: <b>%d</b><li>" category count
      yield "</u1></body></html>" ]
    |> String.concat "\n"

