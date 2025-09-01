Public Class FormMenu
    Private Sub AbrirFormularioEnPanel(formHijo As Form)
        PanelGeneral.Controls.Clear() ' Limpia el panel
        formHijo.TopLevel = False
        formHijo.FormBorderStyle = FormBorderStyle.None
        formHijo.Dock = DockStyle.Fill
        PanelGeneral.Controls.Add(formHijo)
        formHijo.Show()
    End Sub

    Private Sub BUsuarios_Click(sender As Object, e As EventArgs) Handles BUsuarios.Click
        Dim f As New FormUsuarios()
        AbrirFormularioEnPanel(f)
    End Sub

    Private Sub BBuckup_Click(sender As Object, e As EventArgs) Handles BBuckup.Click
        Dim f As New FormBackup()
        AbrirFormularioEnPanel(f)
    End Sub

    Private Sub BVentas_Click(sender As Object, e As EventArgs) Handles BVentas.Click
        Dim f As New FormVentas()
        AbrirFormularioEnPanel(f)
    End Sub

    Private Sub BClientes_Click(sender As Object, e As EventArgs) Handles BClientes.Click
        Dim f As New FormClientes()
        AbrirFormularioEnPanel(f)
    End Sub

    Private Sub BProductos_Click(sender As Object, e As EventArgs) Handles BProductos.Click
        Dim f As New FormProductos()
        AbrirFormularioEnPanel(f)
    End Sub

    Private Sub BReportes_Click(sender As Object, e As EventArgs) Handles BReportes.Click
        Dim f As New FormReportes()
        AbrirFormularioEnPanel(f)
    End Sub

    Private Sub BSalir_Click(sender As Object, e As EventArgs) Handles BSalir.Click
        Application.Exit()
    End Sub
End Class