Imports System.Data.SqlClient

Public Class FormAnaSayfa
    Public Property Kullanici As Kullanici
    Public Property SeciliTarih As String

    Public Sub New(ByVal kullaniciId As Integer)
        InitializeComponent()
        Me.Kullanici = DB.KullaniciBilgileriGetir(kullaniciId)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "AJANDA - (" + Kullanici.Adi + ")"
        Label1.Text = BugunTarih(-1)
        Label2.Text = BugunTarih()
        Label3.Text = BugunTarih(1)

        SeciliTarih = BugunTarih()

        Dim lists As List(Of List(Of Reminder)) = DB.GunuYukle(BugunTarih(), Kullanici.Id)
        'MessageBox.Show(lists(1).Count.ToString())
        Listele(lists)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim FormEkle As FormEkle = New FormEkle(Kullanici, Me, Kullanici.Id, seciliTarih, False)
        FormEkle.Show()
    End Sub

    Private Function BugunTarih(Optional ByVal gunEkle As Integer = 0) As String
        If gunEkle = 0 Then
            Dim now As DateTime = DateTime.Now
            Dim g As Integer = now.Day
            Dim a As Integer = now.Month
            Dim y As Integer = now.Year
            Return g & "." & a & "." & y
        Else
            Dim gun As DateTime = DateTime.Now.Date.AddDays(gunEkle)
            Return gun.Day & "." & gun.Month & "." & gun.Year
        End If
    End Function

    Dim tbls As List(Of TableLayoutPanel) = New List(Of TableLayoutPanel)()

    Friend Sub Listele(ByVal list As List(Of List(Of Reminder)))
        Dim assignedtome As List(Of Reminder) = list(0)
        Dim assignedbyme As List(Of Reminder) = list(1)
        FlowLayoutPanel1.Controls.Clear()

        ' Gorevlerim Baslik
        If assignedtome.Count <> 0 Then
            FlowLayoutPanel1.Controls.Add(New Label With {
                .Name = "label2",
                .Text = "Görevlerim",
                .Width = FlowLayoutPanel1.Width - 15,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleCenter
            })

            Dim tblBaslik As TableLayoutPanel = New TableLayoutPanel()
            tblBaslik.Width = FlowLayoutPanel1.Width - 30
            tblBaslik.Height = 25
            tblBaslik.BackColor = Color.SlateBlue
            tblBaslik.ForeColor = Color.White
            tblBaslik.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            tblBaslik.RowCount = 1
            tbls.Add(tblBaslik)

            'tblBaslik.ColumnStyles.Add(New ColumnStyle() With {.SizeType = SizeType.AutoSize})
            'tblBaslik.ColumnStyles.Add(New ColumnStyle() With {.SizeType = SizeType.Percent, .Width = 50%})

            tblBaslik.Controls.Add(New Label With {
                .Text = "Saat",
                .Width = 60,
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            }, 0, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = "Baslik",
                .Width = 340,
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            }, 1, 0)

            Dim resim As Image = Image.FromFile("kebab-menu.ico")
            tblBaslik.Controls.Add(New Label With {
                .Text = "",
                .Image = resim.GetThumbnailImage(15, 15, Nothing, IntPtr.Zero),
                .Anchor = AnchorStyles.None,
                .Width = 25,
                .Margin = New Padding(0),
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            }, 2, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = "Kategori",
                .Width = 85,
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            }, 3, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = "Atayan",
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            }, 4, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = " ",
                .Width = 20,
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
            }, 5, 0)


            tblBaslik.Controls.Add(New Label With {
                .Text = "Düzenle",
                .Width = 60,
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 7, FontStyle.Regular)
            }, 6, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = "Sil",
                .Width = 50,
                .Anchor = AnchorStyles.Left,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 7, FontStyle.Regular)
            }, 7, 0)

            FlowLayoutPanel1.Controls.Add(tblBaslik)
        End If

        ' Gorevlerim
        For Each i As Integer In Enumerable.Range(0, assignedtome.Count)
            Dim tbl As New TableLayoutPanel()
            tbl.Width = FlowLayoutPanel1.Width - 25
            tbl.Height = 50
            tbl.BackColor = Color.White
            tbl.ForeColor = Color.Black
            tbl.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            tbl.RowCount = 1
            tbl.Tag = assignedtome(i).Aciklama
            tbls.Add(tbl)
            Dim chckd As Boolean = False
            If assignedtome(i).Tamamlandi Then chckd = True


            tbl.Controls.Add(New Label With {.Text = assignedtome(i).Saat, .Width = 60, .Anchor = AnchorStyles.None, .TextAlign = ContentAlignment.MiddleCenter, .Font = New Font("Microsoft Sans Serif", 13, FontStyle.Bold)}, 0, 0)

            tbl.Controls.Add(New Label With {.Text = assignedtome(i).Baslik, .Width = 340, .Padding = New Padding(20, 0, 0, 0), .Anchor = AnchorStyles.Left, .TextAlign = ContentAlignment.MiddleLeft, .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)}, 1, 0)

            Dim aciklamaBtn As Button = New Button()
            aciklamaBtn.Width = 23
            aciklamaBtn.Height = 23
            aciklamaBtn.Tag = assignedtome(i).Aciklama
            aciklamaBtn.Anchor = AnchorStyles.None
            Dim resim3 As Image = Image.FromFile("kebab-menu.ico")
            If resim3.Width > aciklamaBtn.Width Or resim3.Height > aciklamaBtn.Height Then
                aciklamaBtn.Image = resim3.GetThumbnailImage(aciklamaBtn.Height - 10, aciklamaBtn.Height - 10, Nothing, IntPtr.Zero)
            Else
                aciklamaBtn.Image = resim3
            End If
            '.Tag = assignedtome(i).Aciklama
            AddHandler aciklamaBtn.Click, AddressOf aciklamaClick
            tbl.Controls.Add(aciklamaBtn, 2, 0)
            'kebab-menu.ico

            tbl.Controls.Add(New Label With {.Text = assignedtome(i).Kategori, .Width = 85, .Anchor = AnchorStyles.None, .TextAlign = ContentAlignment.MiddleCenter, .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)}, 3, 0)
            tbl.Controls.Add(New Label With {.Text = assignedtome(i).KullaniciAdi, .Height = 40, .Anchor = AnchorStyles.None, .TextAlign = ContentAlignment.MiddleCenter, .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)}, 4, 0)

            Dim tamam As New CheckBox()
            tamam.Text = ""
            tamam.Anchor = AnchorStyles.None
            tamam.Checked = chckd
            tamam.Width = 25
            tamam.CheckAlign = ContentAlignment.MiddleCenter
            tamam.TabStop = False
            tamam.Tag = assignedtome(i).Id
            AddHandler tamam.Click, AddressOf tamamButtonClick
            tbl.Controls.Add(tamam, 5, 0)

            If assignedtome(i).AtayanYetki >= Kullanici.Yetki Then
                Dim duzenleBtn As New Button()
                'duzenleBtn.Text = "Dznlrsmkoy"
                duzenleBtn.Width = 70
                duzenleBtn.Height = 25
                duzenleBtn.Anchor = AnchorStyles.None
                duzenleBtn.TabStop = False
                duzenleBtn.BackColor = Color.White
                duzenleBtn.Tag = assignedtome(i).Id
                duzenleBtn.Width = 55
                duzenleBtn.Height = 23
                Dim resim As Image = Image.FromFile("edit.png")
                If resim.Width > duzenleBtn.Width Or resim.Height > duzenleBtn.Height Then
                    duzenleBtn.Image = resim.GetThumbnailImage(duzenleBtn.Height - 10, duzenleBtn.Height - 10, Nothing, IntPtr.Zero)
                Else
                    duzenleBtn.Image = resim
                End If
                AddHandler duzenleBtn.Click, AddressOf duzenleButtonClick
                tbl.Controls.Add(duzenleBtn, 6, 0)

                Dim silBtn As New Button()
                silBtn.Name = "silbtn" & i
                silBtn.Text = "Sil"
                silBtn.Width = 70
                silBtn.Height = 25
                silBtn.Anchor = AnchorStyles.Left
                silBtn.TabStop = False
                silBtn.BackColor = Color.White
                silBtn.Tag = assignedtome(i).Id
                silBtn.Margin = New Padding(6.5, 0, 0, 0)
                silBtn.Width = 55
                silBtn.Height = 23
                Dim resim2 As Image = Image.FromFile("delete.png")
                If resim2.Width > silBtn.Width Or resim2.Height > silBtn.Height Then
                    silBtn.Image = resim2.GetThumbnailImage(silBtn.Height - 10, silBtn.Height - 10, Nothing, IntPtr.Zero)
                Else
                    silBtn.Image = resim2
                End If
                AddHandler silBtn.Click, AddressOf silButtonClick
                tbl.Controls.Add(silBtn, 7, 0)
                'tbl.Controls.Add(New Label() With {.Text = ""}, 8, 0)
            Else
                tbl.Controls.Add(New Label, 5, 0)
                tbl.Controls.Add(New Label, 6, 0)
            End If


            'tbl.ColumnStyles.Add(New ColumnStyle() With {.SizeType = SizeType.AutoSize})
            'tbl.ColumnStyles.Add(New ColumnStyle() With {.SizeType = SizeType.Percent, .Width = 50%})

            FlowLayoutPanel1.Controls.Add(tbl)
        Next

        ' Atadiklarim Baslik
        If assignedbyme.Count <> 0 Then
            FlowLayoutPanel1.Controls.Add(New Label With {
                .Name = "label3",
                .Text = "Atadıklarım",
                .Width = FlowLayoutPanel1.Width - 15,
                .AutoSize = False,
                .TextAlign = ContentAlignment.MiddleCenter
            })

            Dim tblBaslik As TableLayoutPanel = New TableLayoutPanel()
            tblBaslik.Width = FlowLayoutPanel1.Width - 30
            tblBaslik.Height = 25
            tblBaslik.BackColor = Color.DarkSlateBlue
            tblBaslik.ForeColor = Color.White
            tblBaslik.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single
            tblBaslik.RowCount = 1
            tbls.Add(tblBaslik)

            tblBaslik.Controls.Add(New Label With {
                .Text = "Saat",
                .Width = 60,
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            }, 0, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = "Baslik",
                .Width = 340,
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            }, 1, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = "...",
                .Anchor = AnchorStyles.None,
                .Width = 25,
                .Margin = New Padding(0),
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Bold)
            }, 2, 0)

            tblBaslik.Controls.Add(New Label With {
            .Text = "Kategori",
            .Width = 85,
            .Anchor = AnchorStyles.None,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
        }, 3, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = "Atanan",
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 8, FontStyle.Regular)
            }, 4, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = " ",
                .Width = 20,
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)
            }, 5, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = "Düzenle",
                .Width = 60,
                .Anchor = AnchorStyles.None,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 7, FontStyle.Regular)
            }, 6, 0)

            tblBaslik.Controls.Add(New Label With {
                .Text = "Sil",
                .Width = 50,
                .Anchor = AnchorStyles.Left,
                .TextAlign = ContentAlignment.MiddleCenter,
                .Font = New Font("Microsoft Sans Serif", 7, FontStyle.Regular)
            }, 7, 0)

            FlowLayoutPanel1.Controls.Add(tblBaslik)
        End If

        ' Atadiklarim
        For Each i As Integer In Enumerable.Range(0, assignedbyme.Count)
            Dim tbl As New TableLayoutPanel()
            tbl.Width = FlowLayoutPanel1.Width - 25
            tbl.Height = 50
            tbl.BackColor = Color.White
            tbl.ForeColor = Color.Black
            tbl.CellBorderStyle = TableLayoutPanelCellBorderStyle.None
            tbl.RowCount = 1
            tbl.Tag = assignedbyme(i).Aciklama
            tbls.Add(tbl)
            Dim chckd As Boolean = False
            If assignedbyme(i).Tamamlandi Then chckd = True

            tbl.Controls.Add(New Label With {.Text = assignedbyme(i).Saat, .Width = 60, .Anchor = AnchorStyles.None, .TextAlign = ContentAlignment.MiddleCenter, .Font = New Font("Microsoft Sans Serif", 13, FontStyle.Bold)}, 0, 0)
            tbl.Controls.Add(New Label With {.Text = assignedbyme(i).Baslik, .Width = 340, .Padding = New Padding(20, 0, 0, 0), .Anchor = AnchorStyles.Left, .TextAlign = ContentAlignment.MiddleLeft, .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)}, 1, 0)

            Dim aciklamaBtn As Button = New Button()
            aciklamaBtn.Width = 23
            aciklamaBtn.Height = 23
            aciklamaBtn.Tag = assignedtome(i).Aciklama
            aciklamaBtn.Anchor = AnchorStyles.None
            Dim resim3 As Image = Image.FromFile("kebab-menu.ico")
            If resim3.Width > aciklamaBtn.Width Or resim3.Height > aciklamaBtn.Height Then
                aciklamaBtn.Image = resim3.GetThumbnailImage(aciklamaBtn.Height - 10, aciklamaBtn.Height - 10, Nothing, IntPtr.Zero)
            Else
                aciklamaBtn.Image = resim3
            End If
            AddHandler aciklamaBtn.Click, AddressOf aciklamaClick
            tbl.Controls.Add(aciklamaBtn, 2, 0)

            tbl.Controls.Add(New Label With {.Text = assignedbyme(i).Kategori, .Width = 85, .Anchor = AnchorStyles.None, .TextAlign = ContentAlignment.MiddleCenter, .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)}, 3, 0)
            tbl.Controls.Add(New Label With {.Text = assignedbyme(i).KullaniciAdi, .Height = 35, .Anchor = AnchorStyles.None, .TextAlign = ContentAlignment.MiddleCenter, .Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)}, 4, 0)

            Dim tamam As New CheckBox()
            tamam.Text = ""
            tamam.Anchor = AnchorStyles.None
            tamam.Checked = chckd
            tamam.Width = 25
            tamam.CheckAlign = ContentAlignment.MiddleCenter
            tamam.TabStop = False
            tamam.Tag = assignedbyme(i).Id
            AddHandler tamam.Click, AddressOf tamamButtonClick
            tbl.Controls.Add(tamam, 5, 0)

            'If assignedbyme(i).AtayanYetki >= Kullanici.Yetki Then
            If True Then
                Dim duzenleBtn As New Button()
                duzenleBtn.Text = ""
                duzenleBtn.Width = 70
                duzenleBtn.Height = 25
                duzenleBtn.Anchor = AnchorStyles.None
                duzenleBtn.TabStop = False
                duzenleBtn.BackColor = Color.White
                duzenleBtn.Tag = assignedbyme(i).Id
                duzenleBtn.Width = 55
                duzenleBtn.Height = 23
                Dim resim As Image = Image.FromFile("edit.png")
                If resim.Width > duzenleBtn.Width Or resim.Height > duzenleBtn.Height Then
                    duzenleBtn.Image = resim.GetThumbnailImage(duzenleBtn.Height - 10, duzenleBtn.Height - 10, Nothing, IntPtr.Zero)
                Else
                    duzenleBtn.Image = resim
                End If
                AddHandler duzenleBtn.Click, AddressOf duzenleButtonClick
                tbl.Controls.Add(duzenleBtn, 6, 0)

                Dim silBtn As New Button()
                silBtn.Name = "silbtn" & i
                silBtn.Text = "Sil"
                silBtn.Width = 70
                silBtn.Height = 25
                silBtn.Anchor = AnchorStyles.Left
                silBtn.TabStop = False
                silBtn.BackColor = Color.White
                silBtn.Tag = assignedbyme(i).Id
                silBtn.Margin = New Padding(6.5, 0, 0, 0)
                silBtn.Width = 60
                silBtn.Height = 23
                Dim resim2 As Image = Image.FromFile("delete.png")
                If resim2.Width > silBtn.Width Or resim2.Height > silBtn.Height Then
                    silBtn.Image = resim2.GetThumbnailImage(silBtn.Height - 10, silBtn.Height - 10, Nothing, IntPtr.Zero)
                Else
                    silBtn.Image = resim2
                End If
                AddHandler silBtn.Click, AddressOf silButtonClick
                tbl.Controls.Add(silBtn, 7, 0)
            Else
                tbl.Controls.Add(New Label, 5, 0)
                tbl.Controls.Add(New Label, 6, 0)
            End If

            FlowLayoutPanel1.Controls.Add(tbl)
        Next
    End Sub

    Private Sub tamamButtonClick(sender As Object, e As EventArgs)
        Dim tamamButton As CheckBox = TryCast(sender, CheckBox)
        Dim sql As String = ""


        If tamamButton.Checked Then
            sql = "update gorevler set tamamlandi = 'e' where id = " + tamamButton.Tag.ToString()
        Else
            sql = "update gorevler set tamamlandi = 'h' where id = " + tamamButton.Tag.ToString()
        End If


        Dim command As SqlCommand = DB.query(sql)

        If command.ExecuteNonQuery() = 1 Then
            'Logger.logTxt("Hatırlatıcı Tamamlandı, id: " & tamamButton.Tag, BugunTarih(), DateTime.Now.ToString("HH:mm"))
        Else
            MessageBox.Show("kaydedilemedi")
        End If

        DB.conn.Close()
    End Sub

    Private Sub duzenleButtonClick(sender As Object, e As EventArgs)
        Dim dznlbtn As Button = TryCast(sender, Button)
        'Dim FormEkle As FormEkle = New FormEkle(Kullanici, Me, Kullanici.Id, SeciliTarih, False)
        'Dim duzenleForm As New FormEkle(Kullanici, Me, Kullanici.Id, SeciliTarih, True, Integer.Parse(dznlbtn.Tag.ToString()))
        Dim duzenleForm As New FormDuzenle(Integer.Parse(dznlbtn.Tag.ToString()), Kullanici, SeciliTarih, Me)
        duzenleForm.Show()
    End Sub

    Private Sub silButtonClick(sender As Object, e As EventArgs)
        Dim btn As Button = TryCast(sender, Button)

        Dim sql As String = "delete from gorevler where id = " & btn.Tag.ToString()
        Dim command As SqlCommand = DB.query(sql)

        If command.ExecuteNonQuery() = 1 Then
            'Logger.logTxt("Hatırlatıcı silindi, Görev ID: " & btn.Tag, BugunTarih(), DateTime.Now.ToString("HH:mm"))
        Else
            MessageBox.Show("Silinemedi" + btn.Tag.ToString())
        End If

        DB.conn.Close()

        Dim list As List(Of List(Of Reminder)) = DB.GunuYukle(SeciliTarih, Kullanici.Id)
        Listele(list)
    End Sub


    Private Sub aciklamaClick(sender As Object, e As EventArgs)
        Dim btn As Button = TryCast(sender, Button)

        MessageBox.Show(btn.Tag.ToString())
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        SeciliTarih = BugunTarih(-1)
        TextBox1.Text = SeciliTarih
        Dim lists As List(Of List(Of Reminder)) = DB.GunuYukle(SeciliTarih, Kullanici.Id)
        Listele(lists)
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs) Handles Label3.Click
        SeciliTarih = BugunTarih(1)
        TextBox1.Text = SeciliTarih
        Dim lists As List(Of List(Of Reminder)) = DB.GunuYukle(SeciliTarih, Kullanici.Id)
        Listele(lists)
    End Sub

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        SeciliTarih = BugunTarih()
        TextBox1.Text = SeciliTarih
        Dim lists As List(Of List(Of Reminder)) = DB.GunuYukle(SeciliTarih, Kullanici.Id)
        Listele(lists)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SeciliTarih = TextBox1.Text
        Dim lists As List(Of List(Of Reminder)) = DB.GunuYukle(SeciliTarih, Kullanici.Id)
        Listele(lists)
    End Sub

    Private Sub FormAnaSayfa_SizeChanged(sender As Object, e As EventArgs) Handles MyBase.SizeChanged
        For Each table In tbls
            table.Width = FlowLayoutPanel1.Width - 25
        Next

        For Each ctrl As Control In FlowLayoutPanel1.Controls
            If ctrl.Name = "label2" OrElse ctrl.Name = "label3" Then
                ctrl.Width = FlowLayoutPanel1.Width - 25
            End If
        Next
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim form As FormYetkiler = New FormYetkiler()
        form.Show()
    End Sub
End Class