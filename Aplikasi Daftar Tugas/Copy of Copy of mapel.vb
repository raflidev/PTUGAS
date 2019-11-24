Imports System.Data.SqlClient

Public Class mapel

    Sub kosongkan()
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox3.Clear()
    End Sub

    Sub ketemu()
        TextBox1.Text = dr(0)
        TextBox2.Text = dr(1)
        TextBox3.Text = dr(2)
    End Sub

    Sub carikode()
        Call koneksi()
        cmd = New SqlCommand("select * from TBL_MAPEL where id_mapel = '" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()

    End Sub

    Sub tampilgrid()
        Call koneksi()
        da = New SqlDataAdapter("select * from TBL_MAPEL", conn)
        ds = New DataSet
        da.Fill(ds)
        DGV.DataSource = ds.Tables(0)
        DGV.ReadOnly = True
    End Sub

    Private Sub mapel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call koneksi()

        Me.CenterToScreen()
        Call tampilgrid()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Then
            MsgBox("data belum lengkap")
        Else
            Call carikode()
            If dr.HasRows Then
                Call koneksi()

                'update
                Dim update As String = "update TBL_MAPEL set nama_mapel='" & TextBox2.Text & "', nama_guru='" & TextBox3.Text & "' where id_mapel='" & TextBox1.Text & "'"
                cmd = New SqlCommand(update, conn)
                dr = cmd.ExecuteReader
                Call tampilgrid()
                MsgBox("data berhasil diupdate")
                TextBox1.Focus()

            Else
                Call koneksi()

                'insert
                Dim simpan As String = "insert into TBL_MAPEL values('" & TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "')"
                cmd = New SqlCommand(simpan, conn)
                dr = cmd.ExecuteReader
                Call tampilgrid()
                Call kosongkan()

                MsgBox("data berhasil dimasukan")
                TextBox1.Focus()
            End If

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call kosongkan()
        TextBox1.Focus()


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Call koneksi()

        If TextBox1.Text = "" Then
            MsgBox("tolong klik dahulu data yg ingin dihapus")
        Else
            'delete
            cmd = New SqlCommand("delete TBL_MAPEL where id_mapel='" & TextBox1.Text & "'", conn)
            dr = cmd.ExecuteReader
            Call tampilgrid()
            MsgBox("data berhasil di hapus")

        End If



    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()

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
