// Classes - Object Expressions

// Object Expressions

open System

type IWeapon =
    abstract Description : string with get
    abstract Power : int with get

type Character(name: string, maxHP: int) =
    member x.Name = name
    member val HP = maxHP with get, set
    member val Weapon : IWeapon option = None with get, set
    member x.Attack(o: Character) =
        let power =
            match x.Weapon with
            | Some(w) -> w.Power
            | None  -> 1  // Fists
        o.HP <- Math.Max(0, o.HP - power)
    override x.ToString() =
        sprintf "%s: %i%i" name x.HP maxHP

// Defined with an Object-Expression
let forgeWeapon desc power =
    { new IWeapon with
         member x.Description with get() = desc
         member x.Power with get() = power }

// Implement two interfaces
let forgeWeapon2 desc power =
    { new IWeapon with
         member x.Description with get() = desc
         member x.Power with get() = power

      interface IDisposable with
         member x.Dispose() = printfn "Disposing Weapon"
    }


let witchKing = Character("Witch-king", 100)
let frodo = Character("Frodo", 0)

let morgulBlade = forgeWeapon2 "Morgul-blade" 25
let sting = forgeWeapon2 "Sting" 10
let narsil = forgeWeapon2 "Narsil" 25

witchKing.Weapon <- Some(morgulBlade)
frodo.Weapon  <- Some(sting)

witchKing.Attack frodo
printfn "Frodo %A" frodo

(morgulBlade :?> IDisposable).Dispose()
(sting :?> IDisposable).Dispose()
(narsil :?> IDisposable).Dispose()
