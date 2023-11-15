Imports System.Data.SqlClient

Public Class FormGiris
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim kullanici_adi As String = TextBox1.Text
        Dim sifre As String = TextBox2.Text
        Dim id As Integer

        Dim reader As SqlDataReader = DB.selectFrom($"select * from kullanicilar where kullanici_adi = '{kullanici_adi}' and sifre = '{sifre}'")
        Dim i As Integer = 0
        While reader.Read()
            i = i + 1
            'Dim k As String = reader("kullanici_adi").ToString()
            'Dim s As String = reader("sifre").ToString()
            id = Integer.Parse(reader("id").ToString())
            'MessageBox.Show(i.ToString() + " - " + k + " - " + s)
        End While

        If i = 1 Then
            Dim form1 As FormAnaSayfa = New FormAnaSayfa(id)
            form1.Show()
        Else
            MessageBox.Show("Yanlış kullanıcı adı veya parola")
        End If

        reader.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Dim kayitform As FormKayit = New FormKayit()
        kayitform.Show()
    End Sub
End Class