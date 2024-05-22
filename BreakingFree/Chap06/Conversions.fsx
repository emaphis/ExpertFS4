// Collections Converting Between Collection Types


let array1 = seq { 1..10 } |> Seq.toArray
printfn "%A" array1

let array2 = seq { 1..10 } |> Array.ofSeq
printfn "%A" array2

// Some conversions are casting

let lst1 = [ 1..10 ]
obj.ReferenceEquals(lst1, Seq.ofList lst1)

let array3 = [| 1; 2; 3; 4; 5; 6; 7; 8; 9; 10 |]
obj.ReferenceEquals(array3, List.ofArray array3)
