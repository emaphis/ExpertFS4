// Collections - Sets

// Sets

// Creating Sets

let alphabet = [ 'A'..'Z' ] |> Set.ofList

let vowels = Set.empty.Add('A').Add('E').Add('I').Add('O').Add('U')


// Working with Sets

// Unions

let set1 = [ 1..5 ] |> Set.ofList
let set2 = [ 3..7 ] |> Set.ofList

let setU1 = Set.union set1 set2
let setU2 = set1 + set2

// Intersections

let setI = Set.intersect set1 set2


// Differences

let setD1 = Set.difference set1 set2
let setD2 = Set.difference set2 set1

let setD3 = set1 - set2
let setD4 = set2 - set1


// Subsets and Supersets

let set3 = [ 1..5 ] |> Set.ofList
let set4 = [ 1..5 ] |> Set.ofList

let bool1 = Set.isSuperset set3 set4
let bool2 = Set.isProperSuperset set3 set4
let bool3 = Set.isSubset set4 set3
let bool4 = Set.isProperSubset set4 set3

let set5 = [ 0..5 ] |> Set.ofList

let bool5 = Set.isSuperset set5 set4
let bool6 = Set.isProperSuperset set5 set4
let bool7 = Set.isSubset set4 set5
let bool8 = Set.isProperSubset set4 set5
