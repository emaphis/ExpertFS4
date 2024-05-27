module Instrument

type Instrument =
    | Guitar
    | Drums
    | Bass


/// Play the instruement
 let play instument =
    match instument with
    | Guitar -> "Wah wah"
    | Drums -> "Ba dam tss"
    | Bass _> "Ba ba bom"
