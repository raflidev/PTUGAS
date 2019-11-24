Imports System.Data.SqlClient

Public Class users
    Sub kosongkan()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
        ComboBox1.Text = ""

    End Sub
    Sub tampillevel()
        ComboBox1.Items.Add("0")
        ComboBox1.Items.Add("1")
    End Sub
    Sub ketemu()
        TextBox1.Text = dr(0)
        TextBox2.Text = dr(1)
        TextBox3.Text = dr(2)
        ComboBox1.Text = dr(3)
    End Sub

    Sub carikode()
        Call koneksi()
        cmd = New SqlCommand("select * from TBL_User where id_user = '" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub

    Sub tampilgrid()
        Call koneksi()
        da = New SqlDataAdapter("select * from TBL_USER", conn)
        ds = New DataSet
        da.Fill(ds)
        DGV.DataSource = ds.Tables(0)
        DGV.ReadOnly = True
    End Sub
    Private Sub users_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call tampilgrid()
        Me.CenterToScreen()
        Call tampillevel()

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call kosongkan()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("data belum lengkap")
        Else
            Call carikode()
            If dr.HasRows Then
                Call koneksi()

                'update
                Dim update As String = "update tbl_user set username='" & TextBox2.Text & "', password='" & TextBox3.Text & "', user_level='" & ComboBox1.Text & "' where id_user='" & TextBox1.Text & "'"
                cmd = New SqlCommand(update, conn)
                dr = cmd.ExecuteReader
                Call tampilgrid()
                MsgBox("data berhasil diupdate")
            Else
                Call koneksi()

                'insert
                Dim simpan As String = "insert into tbl_user values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & ComboBox1.Text & "')"
                cmd = New SqlCommand(simpan, conn)
                dr = cmd.ExecuteReader
                Call tampilgrid()
                Call kosongkan()

                MsgBox("data berhasil dimasukan")

            End If

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call carikode()
        If dr.HasRows Then
            'delete
            cmd = New SqlCommand("delete tbl_user where id_user='" & TextBox1.Text & "'", conn)
            dr = cmd.ExecuteReader
            Call tampilgrid()
            MsgBox("data berhasil di hapus")
        Else
            MsgBox("tolong klik dahulu data yg ingin dihapus")
        End If

    End Sub


    Private Sub DGV_CellMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGV.CellMouseClick
        On Error Resume Next
        TextBox1.Text = DGV.Rows(e.RowIndex).Cells(0).Value
        Call carikode()
        If dr.HasRows Then
            Call ketemu()
        End If
    End Sub
End Class