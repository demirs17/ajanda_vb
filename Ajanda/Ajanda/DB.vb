Imports System.Data.SqlClient

Public Class DB

    Public Shared conn As SqlConnection
    Public Shared Function query(ByVal sql As String) As SqlCommand
        Dim connStr As String = "Server=localhost;Database=Ajanda;Trusted_Connection=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim command As SqlCommand
        command = New SqlCommand(sql, conn)

        Return command
    End Function

    Public Shared Function selectFrom(ByVal sql As String) As SqlDataReader
        Dim connStr As String = "Server=localhost;Database=Ajanda;Trusted_Connection=True"
        conn = New SqlConnection(connStr)
        conn.Open()

        Dim command As SqlCommand
        Dim reader As SqlDataReader

        command = New SqlCommand(sql, conn)
        reader = command.ExecuteReader()

        Return reader
    End Function

    Public Shared Function KullaniciBilgileriGetir(kullanici_id As Integer) As Kullanici
        Dim KullaniciBilgileri As Kullanici = New Kullanici(0, "Kullanıcı Bulunamadı", "", 0)
        Dim reader As SqlDataReader = DB.selectFrom($"select * from kullanicilar where id = '{kullanici_id}'")
        Dim i As Integer = 0
        While reader.Read()
            i = i + 1
            Dim id As Integer = Integer.Parse(reader("id").ToString())
            Dim adi As String = reader("kullanici_adi").ToString()
            Dim email As String = "" 'reader("email").ToString()
            Dim yetki As String = reader("yetki").ToString()

            KullaniciBilgileri = New Kullanici(id, adi, email, yetki)
        End While
        reader.Close()

        Return KullaniciBilgileri
    End Function

    Public Shared Function KullaniciYetkiKaydet(kullanici_adi As String, yetki As String, kullanici_id As Integer) As Boolean
        Dim sql As String = $"update kullanicilar set kullanici_adi = '{kullanici_adi}', yetki = '{yetki}' where kullanici_id = {kullanici_id.ToString()}"
        Dim cmd As SqlCommand = DB.query(sql)

        If cmd.ExecuteNonQuery = 1 Then
            'MessageBox.Show("Kaydedildi")
            Return True
        Else
            MessageBox.Show("Kaydedilemedi")
            Return False
        End If
    End Function

    Public Shared Function KullaniciAdi(id As Integer) As String
        Dim rdr As SqlDataReader = DB.selectFrom("select kullanici_adi from kullanicilar where id = " & id)
        While rdr.Read()
            Return rdr("kullanici_adi")
        End While
        Return ""
    End Function

    Public Shared Function KullanicilariCek() As List(Of String)
        'Dim list As List(Of String) = New List(Of String) From {"1,", "2."}
        Dim list As List(Of String) = New List(Of String) From {}

        Dim reader As SqlDataReader = DB.selectFrom("select kullanici_adi from kullanicilar")
        While reader.Read()
            'MessageBox.Show("readerda")
            list.Add(reader("kullanici_adi").ToString())
            'MessageBox.Show("kadi:" + reader("kullanici_adi").ToString())
        End While
        reader.Close()
        'MessageBox.Show("readerdan çıktı")

        Return list
    End Function

    Public Shared Function KullaniciYetki() As List(Of Kullanici)
        Dim list As List(Of Kullanici) = New List(Of Kullanici) From {}
        Dim reader As SqlDataReader = DB.selectFrom("select * from kullanicilar")
        While reader.Read()
            list.Add(New Kullanici(reader("id"), reader("kullanici_adi"), "", reader("yetki")))
        End While
        Return list
    End Function

    Public Shared Function GunuYukle(tarih As String, kullanici_id As Integer) As List(Of List(Of Reminder))
        Dim assignedtome As New List(Of Reminder)()
        Dim assignedbyme As New List(Of Reminder)()

        ' DB.selectFrom("select * from gorevler, kullanicilar where tarih = '" + tarih + "' and kime = " + kullanici_id + "and gorevler.kullanici_id = kullanicilar.id")


        Dim reader As SqlDataReader = DB.selectFrom("select * from gorevler, kullanicilar where tarih = '" + tarih + "' and kime = " + kullanici_id.ToString() + " and gorevler.kullanici_id = kullanicilar.id")

        Dim i As Integer = 0
        While reader.Read()
            i += 1
            assignedtome.Add(New Reminder(Integer.Parse(reader.GetValue(0).ToString()), reader.GetValue(1).ToString(), Integer.Parse(reader.GetValue(2).ToString()), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString(), reader.GetValue(6).ToString(), reader.GetValue(7).ToString(), Integer.Parse(reader.GetValue(8).ToString()), reader.GetValue(10).ToString(), Integer.Parse(reader.GetValue(12).ToString())))
            ' id baslik kullanici_id(kimden) tarih saat kategori aciklama tamamlnadi kime
        End While

        reader = DB.selectFrom("select * from gorevler, kullanicilar where tarih = '" + tarih + "' and kullanici_id = " + kullanici_id.ToString() + " and kime != kullanici_id and gorevler.kime = kullanicilar.id")

        Dim j As Integer = 0
        While reader.Read()
            j += 1
            assignedbyme.Add(New Reminder(Integer.Parse(reader.GetValue(0).ToString()), reader.GetValue(1).ToString(), Integer.Parse(reader.GetValue(2).ToString()), reader.GetValue(3).ToString(), reader.GetValue(4).ToString(), reader.GetValue(5).ToString(), reader.GetValue(6).ToString(), reader.GetValue(7).ToString(), Integer.Parse(reader.GetValue(8).ToString()), reader.GetValue(10).ToString(), Integer.Parse(reader.GetValue(12).ToString())))
            ' id baslik kullanici_id(kimden) tarih saat kategori aciklama tamamlnadi kime
        End While
        DB.conn.Close()

        Return New List(Of List(Of Reminder)) From {assignedtome, assignedbyme}
    End Function

    Public Shared Function getReminder(id As Integer) As Reminder
        Dim reader As SqlDataReader = DB.selectFrom("select * from gorevler, kullanicilar where gorevler.kullanici_id = kullanicilar.id and gorevler.id = " & id)
        While reader.Read()
            Return New Reminder(Integer.Parse(reader("id")), reader("baslik"), Integer.Parse(reader("kullanici_id")), reader("tarih"), reader("saat"), reader("kategori"), reader("aciklama"), reader("tamamlandi"), Integer.Parse(reader("kime")), reader("kullanici_adi"), Integer.Parse(reader("yetki")))
        End While
        Return New Reminder(0, "", 0, "", "", "", "", "", 0, "", 0)
    End Function

End Class
