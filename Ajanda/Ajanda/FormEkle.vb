Imports System.Data.SqlClient
Public Class FormEkle
    Dim Kullanici As Kullanici



    Dim Duzenle As Boolean
    'Dim KullaniciId As Integer
    Dim SeciliTarih As String
    Dim GorevId As Integer
    Private MainForm As FormAnaSayfa = Nothing

    Public Sub New(kullanici_bilgiler As Kullanici, ByVal frm1 As Form, ByVal kullaniciId As Integer, ByVal seciliTarih As String, Optional ByVal duzenle As Boolean = False, Optional ByVal gorevid As Integer = 0)
        MainForm = TryCast(frm1, FormAnaSayfa)
        Me.Kullanici = kullanici_bilgiler
        Me.Duzenle = duzenle
        'Me.KullaniciId = kullaniciId
        Me.SeciliTarih = seciliTarih
        Me.GorevId = gorevid
        InitializeComponent()
    End Sub


    Private Sub FormEkle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox2.Text = SeciliTarih
        TextBox3.Text = "12:00"
        ComboBox1.Text = Kullanici.Adi

        Dim kullanicilar As List(Of String) = DB.KullanicilariCek()
        For index = 0 To kullanicilar.Count - 1
            ComboBox1.Items.Add(kullanicilar(index))
        Next

        'MessageBox.Show("SEciliTarih : " + SeciliTarih + " - Duzenle? : " + Duzenle.ToString())
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Duzenle Then ' güncelleme




            'Duzenle BOZUK





            Dim kime_id As Integer = Kullanici.Id
            Dim reader As SqlDataReader = DB.selectFrom("select id from kullanicilar where kullanici_adi = '" & ComboBox1.Text & "'")
            While reader.Read()
                kime_id = Integer.Parse(reader.GetValue(0).ToString())
            End While
            'MessageBox.Show(kime_id.ToString())

            Dim sql As String = "update gorevler set baslik = '" & TextBox1.Text & "',kategori = '" & TextBox3.Text & "' ,tarih = '" & ComboBox2.Text & "',aciklama = '" & TextBox2.Text & "',saat = '" & ComboBox1.Text & "', kime = " & kime_id & " where id = " & GorevId
            Dim command As SqlCommand = DB.query(sql)

            If command.ExecuteNonQuery() = 1 Then
                Dim now As DateTime = DateTime.Now
                Dim g As String = If(now.Day.ToString().Length = 1, "0" & now.Day, now.Day.ToString())
                Dim a As String = If(now.Month.ToString().Length = 1, "0" & now.Month, now.Month.ToString())
                Dim y As Integer = now.Year
                ''Dim logger As New Logger("Düzenlendi, Görev ID : " & GorevId, g & "." & a & "." & y, DateTime.Now.ToString("HH:mm"))
                ''logger.txtYaz()
            Else
                MessageBox.Show("Kaydedilemedi")
            End If
            DB.conn.Close()

            MainForm.Listele(DB.GunuYukle(SeciliTarih, Kullanici.Id))
            'MainForm.BildirimYenile()

            Me.Close()
        Else
            Dim reader As SqlDataReader = DB.selectFrom("select id from kullanicilar where kullanici_adi = '" + ComboBox1.Text + "'")
            Dim kime_id As Integer = Kullanici.Id
            While reader.Read()
                kime_id = Integer.Parse(reader.GetValue(0).ToString())
            End While

            Dim sql As String = $"insert into gorevler(baslik, kullanici_id, tarih, saat, kategori, aciklama, tamamlandi, kime) values('{TextBox1.Text}',{Kullanici.Id.ToString()} ,'{TextBox2.Text}','{TextBox3.Text}','{ComboBox2.Text}','{TextBox4.Text}','h', {kime_id.ToString()})"
            'MessageBox.Show(sql)
            Dim command As SqlCommand = DB.query(sql)

            If command.ExecuteNonQuery() = 1 Then
                Dim now As DateTime = DateTime.Now
                Dim g As String = If(now.Day.ToString().Length = 1, "0" + now.Day.ToString(), now.Day.ToString())
                Dim a As String = If(now.Month.ToString().Length = 1, "0" + now.Month.ToString(), now.Month.ToString())
                Dim y As Integer = now.Year
                'Dim logger As New Logger("Yeni Hatırlatıcı oluşturuldu, Başlık: " + TextBox1.Text, g + "." + a + "." + y, DateTime.Now.ToString("HH:mm"))
                'logger.txtYaz()
            Else
                MessageBox.Show("Hata Oluştu")
            End If

            DB.conn.Close()

            MainForm.Listele(DB.GunuYukle(SeciliTarih, Kullanici.Id))
            'MainForm.BildirimYenile()

            Me.Close()
        End If
    End Sub
End Class