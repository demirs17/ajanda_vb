Public Class Kullanici
    Public Id As Integer
    Public Adi As String
    Public Email As String
    Public Yetki As Integer

    Public Sub New(id As Integer, adi As String, email As String, yetki As Integer)
        Me.Id = id
        Me.Adi = adi
        Me.Email = email
        Me.Yetki = yetki
    End Sub

End Class
