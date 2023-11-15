Public Class FormYetkiler
    Private Sub FormYetkiler_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim kullanicilar As List(Of Kullanici) = DB.KullaniciYetki()
        For index = 0 To kullanicilar.Count - 1
            Dim tbl As TableLayoutPanel = New TableLayoutPanel()

            tbl.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset
            tbl.Width = FlowLayoutPanel1.Width - 25
            tbl.Height = 50

            tbl.Controls.Add(New Label() With {.Text = kullanicilar(index).Id, .Width = 40, .Dock = DockStyle.Fill, .TextAlign = ContentAlignment.MiddleCenter}, 0, 0)
            tbl.Controls.Add(New TextBox() With {.Text = kullanicilar(index).Adi, .Anchor = AnchorStyles.None}, 1, 0)
            tbl.Controls.Add(New TextBox() With {.Text = kullanicilar(index).Yetki, .Anchor = AnchorStyles.None}, 2, 0)

            Dim kaydetBtn As Button = New Button()
            kaydetBtn.Text = "Kaydet"
            kaydetBtn.Anchor = AnchorStyles.None
            AddHandler kaydetBtn.Click, AddressOf kaydetClick
            tbl.Controls.Add(kaydetBtn, 3, 0)

            FlowLayoutPanel1.Controls.Add(tbl)
        Next
    End Sub

    Private Sub kaydetClick(sender As Object, e As EventArgs)
        Dim kaydetButton As Button = TryCast(sender, Button)
        DB.KullaniciYetkiKaydet("", "", 0)
    End Sub
End Class