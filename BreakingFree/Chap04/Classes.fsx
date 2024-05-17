// Classes

open System

// A General Read-only Class

type Person (id:Guid, name:string, age:int) =
    member x.Id = id
    member x.Name = name
    member x.Age = age

let person1 = Person(Guid.NewGuid(), "Charley", 49)

printfn "Id = %A" person1.Id
printfn "Name = %s" person1.Name
printfn "Age = %i" person1.Age
