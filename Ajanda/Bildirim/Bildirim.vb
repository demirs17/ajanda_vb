Imports System.Data.SqlClient
Public Class Bildirim
    Private Sub Bildirim_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim hatirlaticilar As List(Of Reminder) = New List(Of Reminder)()

        Dim now As DateTime = DateTime.Now
        Dim g As Integer = now.Day
        Dim a As Integer = now.Month
        Dim y As Integer = now.Year

        Dim connStr As String = "Server=localhost;Database=Ajanda;Trusted_Connection=True"
        Dim conn As SqlConnection = New SqlConnection(connStr)
        conn.Open()

        Dim command As SqlCommand
        Dim reader As SqlDataReader
        Dim sql As String

        'sql = "select * from gorevler where tarih = '"+ tarih +"' and kullanici_id = " + KullaniciId;
        sql = "select * from gorevler where tarih = '" & g & "." & a & "." & y & "'"
        command = New SqlCommand(sql, conn)
        reader = command.ExecuteReader()

        Dim i As Integer = 0

        MessageBox.Show(reader.ToString)

        While reader.Read()
            i += 1
            hatirlaticilar.Add(New Reminder(id:=Integer.Parse(reader.GetValue(0).ToString()), baslik:=reader.GetValue(1).ToString(), saat:=reader.GetValue(4).ToString(), tamamlandi:=reader.GetValue(7).ToString()))
            ' id baslik kullanici_id tarih saat kategori aciklama tamamlandi
        End While

        conn.Close()

        SetInterval(hatirlaticilar)

        For j As Integer = 0 To hatirlaticilar.Count - 1
            flowLayoutPanel1.Controls.Add(New Label With {.Text = hatirlaticilar(j).ToString(), .Width = 350})
        Next j
    End Sub

    Private Async Sub SetInterval(ByVal array As List(Of Reminder))
        For i As Integer = 0 To array.Count - 1
            If array(i).Saat = DateTime.Now.ToString("HH:mm") AndAlso array(i).Tamamlandi = False Then
                Call (New ToastContentBuilder()).AddArgument("action", "viewConversation").AddArgument("conversationId", 9813).AddText(array(i).Baslik).AddText(array(i).Saat & " - " & DateTime.Now.Day & "." & DateTime.Now.Month & "." & DateTime.Now.Year).Show()
                array(i).Tamamlandi = True
                SetInterval(array)
            End If
        Next i
        Await Task.Delay(1000)
        SetInterval(array)
    End Sub
End Class
