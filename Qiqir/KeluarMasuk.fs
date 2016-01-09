namespace Qiqir

open System
open System.Linq
open SQLite

type KeluarMasuk() =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val Id: int = 0 with get, set

    member val Amount: double = 0.0 with get, set

