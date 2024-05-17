// Class Constructors
// Constructors

// Constructors (Primary Constructor)

type ConstructorlessClass = class end

open System

type PersonA (id:Guid, name:string, age:int) =
    member x.Id = id
    member x.Name = name
    member x.Age = age

let me = PersonA(Guid.NewGuid(), "Edward", 55)
printfn "my name is %A" me.Id
printfn "my name is %s" me.Name
printfn "my name is %d" me.Age


// Primary Constructors

type PersonB (name:string, dob :DateTime) =
    // body of primary constructor
    let age = (DateTime.Now - dob).TotalDays / 365.25
    do printfn "Creating person: %s (Age: %f)" name age

    member x.Name = name
    member x.DateOfBirth = dob
    member x.Age = age

let meB = PersonB ("Edward", DateTime(1975, 8, 17))

printfn "My name is %s" meB.Name
printfn "My DOB is %A" meB.DateOfBirth
printfn "My age is %f" meB.Age

// Private Primary Constructor

type Greeter private () =
    static let _instance = lazy (Greeter())
    static member Instance with get() = _instance.Force()
    member x.SayHello = printfn "hello"

let greeter = Greeter.Instance
greeter.SayHello

// Additional Constructors

type PersonC (name:string, age:int) =
    do printfn "Creating person: %s (Age: %i)" name age
    new (name) = PersonC(name, 0)
    new () = PersonC ("")

    member x.Name = name
    member x.Age = age

let meC1 = PersonC("Charley", 49)
let meC2 = PersonC("Fred")
let meC3 = PersonC()

printfn "Person 1 = %s" meC1.Name
printfn "Person 2 = %s" meC2.Name
printfn "Person 3 = %i" meC3.Age


// Additional constructors can invoke additional code

type PersonD (name, age) =
    do printfn "Creating person: %s (Age: %i)" name age
    new (name) = PersonD(name, 0) then
        printfn "Creating person with default age"
    new () = PersonD ("") then
        printfn  "Creating person with default name and age"

    member x.Name = name
    member x.Age = age

let meD1 = PersonD("Charley", 49)
let meD2 = PersonD("Fred")
let meD3 = PersonD()

printfn "Person 1 = %s" meD1.Name
printfn "Person 2 = %s" meD2.Name
printfn "Person 3 = %i" meD3.Age


// Classes without a primary constructor behave a bit differently at initialization.

type PersonE =
    val _name : string
    val _age : int
    new (name, age) = { _name = name; _age = age}
    new (name) = PersonE(name, 0)
    new () = PersonE("")

    member x.Name = x._name
    member x.Age = x._age

let meE1 = PersonE("Charley", 49)
let meE2 = PersonE("Fred")
let meE3 = PersonE()

printfn "Person 1 = %s" meE1.Name
printfn "Person 2 = %s" meE2.Name
printfn "Person 3 = %i" meE3.Age


// Self-Identifiers 'as this'

type PersonF (name, age) as this =
    do printfn "Creating person: %s (Age: %i)" this.Name this.Age

    member x.Name = name
    member x.Age = age

let meF = PersonF("Charley", 33)
printfn "Person 2 = %s" meF.Name
printfn "Person 3 = %i" meF.Age


// You can use any identifier other than `this`

type PersonG (name, age) as ``This is a bad identifier`` =
    do printfn "Creating person: %s (Age: %i)"
        ``This is a bad identifier``.Name  // KeK
        ``This is a bad identifier``.Age

    member x.Name = name
    member x.Age = age

let meG = PersonG("Charley", 33)
printfn "Person 2 = %s" meG.Name
printfn "Person 3 = %i" meG.Age
