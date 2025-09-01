Public Class FormUsuarios

    Private Sub FormUsuarios_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        TableLayoutPanel1.Invalidate() ' fuerza redibujo
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub
End Class