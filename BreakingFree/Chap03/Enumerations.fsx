// Enumerations

type DayOfWeek =
    | Sunday = 0
    | Monday = 1
    | Tuesday = 2
    | Wednesday = 3
    | Thursday = 4
    | Friday = 5
    | Saturday = 6

// Enumerations as bytes
type DayOfWeekByte =
    | Sunday = 0y
    | Monday = 1y
    | Tuesday = 2y
    | Wednesday = 3y
    | Thursday = 4y
    | Friday = 5y
    | Saturday = 6y


// Flags Enumerations  - bit set

open System

[<Flags>]
type DayOfWeekFlag =
    | None = 0
    | Sunday = 1
    | Monday = 2
    | Tuesday = 4
    | Wednesday = 8
    | Thursday = 16
    | Friday = 32
    | Saturday = 64

let weekend = DayOfWeek.Saturday ||| DayOfWeek.Sunday

// Reconstructing Enumeration Values

let day1 = enum<DayOfWeek> 16

open Microsoft.FSharp.Core.LanguagePrimitives

let day2 = EnumOfValue<sbyte, DayOfWeekByte> 16y;
