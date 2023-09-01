namespace Chapter05

module Say =
    let hello name =
        printfn "Hello %s" name

// Useful functions from other chapters
module Useful =

    open System
    open System.IO
    open System.Net

    //Using the time, http and getWords functions from Chapter 3.
    let time f =
        let start = DateTime.Now
        let res = f()
        let finish = DateTime.Now
        (res, finish - start)

    let http (url : string) =
        let req = System.Net.WebRequest.Create(url)
        let resp = req.GetResponse()
        let stream = resp.GetResponseStream()
        let reader = new StreamReader(stream)
        let html = reader.ReadToEnd()
        resp.Close()
        html

    let delimiters = [|' '; '\n'; '\t'; '<'; '>'; '='|]
    let getWords (s:string) = s.Split delimiters
    //val time : f:(unit -> 'a) -> 'a * TimeSpan

