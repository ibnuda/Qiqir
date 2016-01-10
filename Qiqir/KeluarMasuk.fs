namespace Qiqir

open System
open System.Linq
open SQLite

type KeluarMasuk() =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdTrx: int = 0 with get, set

    member val Jumlah: double = 0.0 with get, set
    member val DateTrx: DateTime = DateTime.Now with get, set
    member val NoCek: string = "" with get, set
    member val Pajak: double = 0.0 with get, set


type Metode() =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdMetode: int = 0 with get, set

    member val NamaMetode: string = "" with get, set


type Penerima() =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdPenerima: int = 0 with get, set
    member val NamaPenerima: string = "" with get, set