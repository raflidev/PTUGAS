
Public Class MenuUtama

    Private Sub QuitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles QuitToolStripMenuItem.Click
        Me.Close()
    End Sub


    Private Sub MapelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MapelToolStripMenuItem.Click
        mapel.Show()
    End Sub

    Private Sub TugasToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TugasToolStripMenuItem.Click
        tugas.Show()

    End Sub

    Private Sub UserToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UserToolStripMenuItem.Click
        users.Show()
    End Sub

    Private Sub ToolStripStatusLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles kodeuser.Click

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        CRV.SelectionFormula = "{TBL_USER.id_user} = '" & kodeuser.Text & "'"
        CRV.ReportSource = "LaporanAllTime.rpt"
        CRV.RefreshReport()
    End Sub

    Private Sub MenuUtama_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

        
    End Sub



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        CRV.SelectionFormula = "{TBL_TUGAS.tugas_aktif} = 'True'"
        CRV.ReportSource = "LaporanAllTime.rpt"
        CRV.RefreshReport()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        CRV.ReportSource = "LaporanAllMapel.rpt"
        CRV.RefreshReport()
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        CRV.SelectionFormula = "month({TBL_TUGAS.deadline}) = (" & Month(DTP.Text) & ")"
        CRV.ReportSource = "LaporanAllTime.rpt"
        CRV.RefreshReport()
    End Sub
End Class