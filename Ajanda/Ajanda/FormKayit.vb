Imports System.Data.SqlClient
Public Class FormKayit
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim kullanici_adi As String = TextBox1.Text
        Dim sifre As String = TextBox2.Text

        Dim command As SqlCommand = DB.query("insert into kullanicilar(kullanici_adi, sifre, yetki) values('" + kullanici_adi.ToString() + "','" + sifre.ToString() + "', '" + 11000.ToString() + "d')")
        If command.ExecuteNonQuery = 1 Then
            MessageBox.Show("Kayıt Yapıldı")
        Else
            MessageBox.Show("Kayıt Yapılamadı")
        End If
    End Sub
End Class