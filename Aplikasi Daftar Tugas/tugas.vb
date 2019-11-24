Imports System.Data.SqlClient

Public Class tugas
    Sub kosongkan()
        TextBox1.Clear()
        ComboBox1.Text = ""

        TextBox2.Clear()
        CheckBox1.Checked = False
        Call idtugas()
    End Sub
    Sub carikode()
        Call koneksi()

        cmd = New SqlCommand("select * from tbl_tugas where id_tugas='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()


    End Sub
    Sub ketemu()
        TextBox1.Text = dr(0) 'id tugas
        CheckBox1.Checked = dr(1) 'tugas aktif
        ComboBox1.Text = dr(2) 'id mapel
        DTP.Value = dr(4) 'deadline
        TextBox2.Text = dr(5) 'tugas /penjelasan

    End Sub
    Sub idtugas()
        Call koneksi()

        cmd = New SqlCommand("select count(id_tugas) from tbl_tugas", conn)
        Using dr = cmd.ExecuteReader
            dr.Read()
            Dim nomor As Integer = dr(0) + 1
            TextBox1.Text = "TGS" & nomor & ""
        End Using
    End Sub


    Sub idmapel()
        cmd = New SqlCommand("select id_mapel from tbl_mapel", conn)
        Using dr = cmd.ExecuteReader
            Do While dr.Read
                ComboBox1.Items.Add(dr(0))
            Loop
        End Using



    End Sub

    Sub tampilgrid()
        Call koneksi()

        da = New SqlDataAdapter("select * from tbl_tugas", conn)
        ds = New DataSet
        da.Fill(ds)
        DGV.DataSource = ds.Tables(0)
        DGV.ReadOnly = True
    End Sub



    Private Sub tugas_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call tampilgrid()
        Call idtugas()

        Call idmapel()

        Me.CenterToScreen()

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or ComboBox1.Text = "" Then
            MsgBox("data belum lengkap")
        Else
            Call carikode()
            If dr.HasRows Then
                Call koneksi()

                'update
                Dim update As String = "update tbl_tugas set  tugas_aktif='" & CheckBox1.Checked & "',id_mapel='" & ComboBox1.Text & "',id_user='" & MenuUtama.kodeuser.Text & "',deadline='" & Format(DTP.Value, "yyyy-MM-dd") & "',tugas='" & TextBox2.Text & "' where id_tugas='" & TextBox1.Text & "'"
                cmd = New SqlCommand(update, conn)
                Using dr = cmd.ExecuteReader


                    Call tampilgrid()
                    MsgBox("data berhasil diupdate")
                    Call idtugas()

                End Using
            Else
                Call koneksi()

                'insert
                Dim simpan As String = "insert into tbl_tugas values('" & TextBox1.Text & "','" & CheckBox1.Checked & "','" & ComboBox1.Text & "','" & MenuUtama.kodeuser.Text & "','" & Format(DTP.Value, "yyyy-MM-dd") & "','" & TextBox2.Text & "')"
                cmd = New SqlCommand(simpan, conn)
                Using dr = cmd.ExecuteReader
                    Call tampilgrid()
                    Call kosongkan()

                    MsgBox("data berhasil dimasukan")
                    Call idtugas()

                End Using

            End If

        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Call koneksi()
        If TextBox1.Text = "" Then
            MsgBox("tolong klik dahulu data yg ingin dihapus")
        Else
            'delete
            cmd = New SqlCommand("delete from tbl_tugas where id_tugas='" & TextBox1.Text & "'", conn)
            dr = cmd.ExecuteReader



            Call tampilgrid()
            MsgBox("data berhasil di hapus")
            Call idtugas()

        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Call kosongkan()
        ComboBox1.Focus()

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
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