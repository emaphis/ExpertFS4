// Listing 2-3. Using the .NET networking libraries from F#

open System.IO
open System.Net

/// Get the contents of the URL via a web request
let http (url: string) =
    let req = WebRequest.Create(url)
    let resp = req.GetResponse()
    let stream = resp.GetResponseStream()
    let reader = new StreamReader(stream)
    let html = reader.ReadToEnd()
    resp.Close()
    html

    // Antonio Cisternino, Adam Granicz, Don Syme