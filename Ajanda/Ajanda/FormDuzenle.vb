Imports System.Data.SqlClient
Public Class FormDuzenle
    Public Id As Integer
    Public Kullanici As Kullanici
    Dim SeciliTarih As String
    Private MainForm As FormAnaSayfa = Nothing

    Public Sub New(id As Integer, kullanici As Kullanici, secili_tarih As String, frm1 As Form)
        MainForm = TryCast(frm1, FormAnaSayfa)
        Me.Id = id
        Me.Kullanici = kullanici
        Me.SeciliTarih = secili_tarih
        InitializeComponent()
    End Sub

    Private Sub FormDuzenle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim reminder As Reminder = DB.getReminder(Id)
        TextBox1.Text = reminder.Baslik
        TextBox2.Text = reminder.Tarih
        TextBox3.Text = reminder.Saat
        TextBox4.Text = reminder.Aciklama
        ComboBox2.Text = reminder.Kategori
        ComboBox1.Text = DB.KullaniciAdi(reminder.Kime)

        Dim kullanicilar As List(Of String) = DB.KullanicilariCek()
        For index = 1 To kullanicilar.Count - 1
            ComboBox1.Items.Add(kullanicilar(index))
        Next
    End Sub


    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        Dim reader As SqlDataReader = DB.selectFrom("select id from kullanicilar where kullanici_adi = '" + ComboBox1.Text + "'")
        Dim kime_id As Integer = Kullanici.Id
        While reader.Read()
            kime_id = Integer.Parse(reader.GetValue(0).ToString())
        End While
        Dim sql As String = $"update gorevler set baslik = '{TextBox1.Text}', tarih = '{TextBox2.Text}', saat = '{TextBox3.Text}', kategori = '{ComboBox2.Text}', aciklama = '{TextBox4.Text}', kime = {kime_id} where id = {Id}"
        'Dim sql As String = $"insert into gorevler(baslik, kullanici_id, tarih, saat, kategori, aciklama, tamamlandi, kime) values('{TextBox1.Text}',{Kullanici.Id.ToString()} ,'{TextBox2.Text}','{TextBox3.Text}','{ComboBox2.Text}',' ','h', {kime_id.ToString()})"
        Dim command As SqlCommand = DB.query(sql)

        If command.ExecuteNonQuery() = 1 Then
            'Dim now As DateTime = DateTime.Now
            'Dim g As String = If(now.Day.ToString().Length = 1, "0" + now.Day.ToString(), now.Day.ToString())
            'Dim a As String = If(now.Month.ToString().Length = 1, "0" + now.Month.ToString(), now.Month.ToString())
            'Dim y As Integer = now.Year
            'Dim logger As New Logger("Yeni Hatırlatıcı oluşturuldu, Başlık: " + TextBox1.Text, g + "." + a + "." + y, DateTime.Now.ToString("HH:mm"))
            'logger.txtYaz()
        Else
            MessageBox.Show("Hata Oluştu")
        End If

        DB.conn.Close()

        MainForm.Listele(DB.GunuYukle(SeciliTarih, Kullanici.Id))
        'MainForm.BildirimYenile()

        Me.Close()
    End Sub
End Class