namespace Qiqir

open System
open System.Linq
open SQLite
open SQLiteNetExtensions.Attributes
open SQLiteNetExtensions.Extensions

(*
    Create a more modular Manager_ data type.
    So I don't have to write many things out.
*)
[<Table("metode")>]
type Metode () =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdMetode: int = 0 with get, set

    member val NamaMetode: string = "" with get, set

type ManagerMetode () =
    member this.GetConnection (): SQLiteConnection =
        let folder: string = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        let fullPath: string = IO.Path.Combine(folder, "qiqir.db3")
        let conn: SQLiteConnection = new SQLiteConnection(fullPath, true)
        let tableKM: int = conn.CreateTable<Metode>()
        conn
    
    member this.connection: SQLiteConnection = this.GetConnection ()

    member this.GetMetodes () = this.connection.Table<Metode>().ToList
    member this.GetMetode (idMetode: int) = this.connection.Find<Metode>(idMetode)

    member this.AddMetode (met) = NotImplementedException
    member this.UpdateMetode (metode) = this.connection.Update(metode)
    member this.DeleteMetode (metode) = this.connection.Delete(metode)

[<Table("penerima")>]
type Penerima () =
    [<PrimaryKey>]
    [<AutoIncrement>]
    member val IdPenerima: int = 0 with get, set
    member val NamaPenerima: string = "" with get, set

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
