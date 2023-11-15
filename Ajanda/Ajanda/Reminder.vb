Public Class Reminder
    Public Id As Integer
    Public Baslik As String
    Public KullaniciId As Integer
    Public Tarih As String
    Public Saat As String
    Public Kategori As String
    Public Aciklama As String
    Public Tamamlandi As Boolean
    Public Kime As Integer
    Public KullaniciAdi As String
    Public AtayanYetki As Integer

    Public Sub New(id As Integer, baslik As String, kullanici_id As Integer, tarih As String, saat As String, kategori As String, aciklama As String, tamamlandi As String, kime As Integer, kullanici_adi As String, atayan_yetki As Integer)
        Me.Id = id
        Me.Baslik = baslik
        Me.KullaniciId = kullanici_id
        Me.Tarih = tarih
        Me.Saat = saat
        Me.Kategori = kategori
        Me.Aciklama = aciklama
        Me.Tamamlandi = If(tamamlandi.ToLower() = "e", True, False)
        Me.Kime = kime
        Me.KullaniciAdi = kullanici_adi
        Me.AtayanYetki = atayan_yetki
    End Sub

End Class
