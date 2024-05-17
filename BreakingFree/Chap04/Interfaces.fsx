// Classes - Interfaces

// Implementing Interfaces

open System

type MyDisposable() =
    interface IDisposable with
        member x.Dispose() = printfn "Disposing"

let dis1 = new MyDisposable()

(dis1 :> IDisposable).Dispose()


// Defining Interfaces TODO:

type ILifeForm =
    abstract Name : string
    abstract Speak : unit -> unit
    abstract Eat : unit -> unit

type Dog(name: string, age: int) =
    member x.Age = age

    interface ILifeForm with
        member x.Name = name
        member x.Speak() = printfn "Woof!"
        member x.Eat() = printfn "Yum.doggy biscuits!"

type Monkey(weight: float) =
    let mutable _weight = weight

    member x.Weight
        with get() = _weight
        and set(v) = _weight <- v

    interface ILifeForm with
        member this.Name = "Monkey!!!"
        member this.Speak() = printfn "Ook ook"
        member this.Eat() = printfn "Bananas!"

type Ninja() =
    interface ILifeForm with
        member this.Name = "Ninjas have no name"
        member this.Speak() = printfn "Ninjas are silent, deadly killers"
        member this.Eat() =
            printfn "Ninjas don't eat, they wail on guitars because they're totally sweet"


let letsEat (lifeForm: ILifeForm) = lifeForm.Eat()
let myDog = Dog("Buffy", 9)
do letsEat (myDog)
