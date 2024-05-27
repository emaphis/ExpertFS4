module BusinessLogicTests

open BusinessLogic
open Xunit
open FsUnit.Xunit
open Swensen.Unquote


[<Fact>]
let ``Large, young teams are correctly identified``() =
    let department =
        { Name = "Super Team"
          Team = [ for i in 1..15 -> { Name = sprintf "Person %d" i;
                Age = 19 } ]}
    Assert.True(department |> isLargeAndYoungTeam)

let isTrue (b:bool) = Assert.True b

[<Fact>]
let ``Large, young teams are correctly identified 2``() =
    let department =
        { Name = "Super Team"
          Team = [ for i in 1..15 -> { Name = sprintf "Person %d" i;
                Age = 19 } ]}
    department |> isLargeAndYoungTeam |> isTrue

[<Fact>]
let ``FSUnit makes nice DSLs!``() =
    let department =
        { Name = "Super Team"
          Team = [ for i in 1..15 -> { Name = sprintf "Person %d" i;
                Age = 19 } ]}
    department
    |> isLargeAndYoungTeam
    |> should equal true
    department.Team.Length
    |> should be (greaterThan 10)

[<Fact>]
let ``Unquote has a simple custom operator for equality``() =
    let department =
        { Name = "Super Team"
          Team = [ for i in 1..15 -> { Name = sprintf "Person %d" i;
                Age = 19 } ]}

    department |> isLargeAndYoungTeam =! true

[<Fact>]
let ``Unquote can parse quotations for excellent diagnostics``() =
    let emptyTeam = { Name = "Empty Team"; Team = [] }
    test <@ emptyTeam.Name.StartsWith "E" @>
