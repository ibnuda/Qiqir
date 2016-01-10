namespace Qiqir

open System
open System.Linq
open SQLite
open SQLiteNetExtensions.Attributes
open SQLiteNetExtensions.Extensions

(*
    Manager TO DO:
    Add CreateTable.
*)

type Manager () =
    member this.GetConnection (): SQLiteConnection =
        let folder: string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        let fullPath: string = IO.Path.Combine(folder, "qiqir.db3")
        let conn: SQLiteConnection = new SQLiteConnection(fullPath, true)
        conn

    member this.connection = this.GetConnection ()
    member this.Add (thing) = this.connection.Insert(thing)
    member this.Update (thing) = this.connection.Update(thing)
    member this.Delete (thing) = this.connection.Delete(thing)

[<Table("metode")>]
type Metode () =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdMetode: int = 0 with get, set

    member val NamaMetode: string = "" with get, set

type ManagerMetode () =
    inherit Manager ()
    member this.GetMetodes () = base.connection.Table<Metode>().ToList
    member this.GetMetode (idMetode: int) = this.connection.Find<Metode>(idMetode)


[<Table("penerima")>]
type Penerima () =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdPenerima: int = 0 with get, set
    member val NamaPenerima: string = "" with get, set

type MetodePenerima () =
    inherit Metode () 

[<Table("kategori")>]
type Kategori () =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdKat: int = 0 with get, set

    member val NamaKat: string = "" with get, set

[<Table("subkategori")>]
type SubKategori () =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdSubKat: int = 0 with get, set

    // [<ForeignKey(typeof(Kategori))>]
    member val IdKat: int = 0 with get, set

    member val NamaSubKat: string = "" with get, set

[<Table("keluarmasuk")>]
type KeluarMasuk () =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdKM: int = 0 with get, set

    member val DateKM: DateTime = DateTime.Now with get, set
    member val Jumlah: double = 0.0 with get, set
    // don't have the time to fix foreignkeys up.
    //[<ForeignKey(typeof(Metode))>]
    member val IdMetode: int = 0 with get, set
    //[<ForeignKey(typeof(Penerima))>]
    member val IdPenerima: int = 0 with get, set
    //[<ForeignKey(typeof(Status))>]
    member val IdStatus: int = 0 with get, set
    //[<ForeignKey(typeof(SubKategori))>]
    member val IdSubkategori: int = 0 with get, set
    member val NoCek: string = "" with get, set
    member val Pajak: double = 0.0 with get, set

type ManagerKeluarMasuk () =
    member this.GetConnection (): SQLiteConnection =
        let folder: string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        let fullPath: string = IO.Path.Combine(folder, "qiqir.db3")
        let conn: SQLiteConnection = new SQLiteConnection(fullPath, true)
        let tableKM: int = conn.CreateTable<KeluarMasuk>()
        conn

    member this.connection: SQLiteConnection = this.GetConnection ()

    member this.GetKeluarMasuks () = this.connection.Table<KeluarMasuk>().ToList
    member this.GetKeluarMasuk (idKM: int) = this.connection.Find<KeluarMasuk>(idKM)

    member this.AddKeluarMasuk (trx) =
        (*  Something like this,
            let keluarMasuk = new KeluarMasuk(trx)
            let recordsAffected = this.connection.Insert(keluarMasuk)
            keluarMasuk *)
        NotImplementedException

    member this.UpdateKeluarMasuk (keluarMasuk) = this.connection.Update(keluarMasuk)

    member this.DeleteKeluarMasuk (keluarMasuk) = this.connection.Delete(keluarMasuk)
