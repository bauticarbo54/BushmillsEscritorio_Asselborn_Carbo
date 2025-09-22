Imports System.Drawing.Imaging

Public Class FormMenu

    ' ========= utilidades =========
    Private Function ToGray(img As Image) As Image
        If img Is Nothing Then Return Nothing
        Dim gray As New Bitmap(img.Width, img.Height)
        Using g = Graphics.FromImage(gray)
            Dim m As New ColorMatrix(New Single()() {
                New Single() {0.299F, 0.299F, 0.299F, 0, 0},
                New Single() {0.587F, 0.587F, 0.587F, 0, 0},
                New Single() {0.114F, 0.114F, 0.114F, 0, 0},
                New Single() {0, 0, 0, 1, 0},
                New Single() {0, 0, 0, 0, 1}
            })
            Using ia As New ImageAttributes()
                ia.SetColorMatrix(m)
                g.DrawImage(img, New Rectangle(0, 0, img.Width, img.Height),
                            0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia)
            End Using
        End Using
        Return gray
    End Function

    Private Sub ApplyButtonState(btn As Button, enabledState As Boolean)
        ' Guardar imagen original una sola vez en Tag
        If btn.Tag Is Nothing AndAlso btn.Image IsNot Nothing Then
            btn.Tag = btn.Image
        End If

        btn.Enabled = enabledState
        btn.Cursor = If(enabledState, Cursors.Hand, Cursors.No)

        If btn.Image IsNot Nothing OrElse btn.Tag IsNot Nothing Then
            If enabledState Then
                ' restaurar imagen original
                btn.Image = TryCast(btn.Tag, Image)
            Else
                ' aplicar escala de grises (sin pisar original)
                Dim original = TryCast(btn.Tag, Image)
                btn.Image = If(original IsNot Nothing, ToGray(original), Nothing)
            End If
        End If
    End Sub

    Private Sub SetAllDisabled()
        ApplyButtonState(BUsuarios, False)
        ApplyButtonState(BBuckup, False) ' ojo: tu botón se llama BBuckup
        ApplyButtonState(BReportes, False)
        ApplyButtonState(BProductos, False)
        ApplyButtonState(BClientes, False)
        ApplyButtonState(BVentas, False)
    End Sub

    Private Sub HabilitarPorRol(rol As String)
        SetAllDisabled()

        Select Case rol
            Case "Gerente"
                ApplyButtonState(BUsuarios, True)
                ApplyButtonState(BBuckup, True)
                ApplyButtonState(BReportes, True)

            Case "Administrador"
                ApplyButtonState(BProductos, True)
                ApplyButtonState(BReportes, True)

            Case "Vendedor"
                ApplyButtonState(BClientes, True)
                ApplyButtonState(BVentas, True)
                ApplyButtonState(BReportes, True)

            Case Else
                ' sin sesión válida: todo deshabilitado
        End Select
    End Sub

    ' ========= carga del menú =========
    Private Sub FormMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' mensaje opcional
        ' LBienvenida.Text = $"Hola {SessionUser.Nombre} — Rol: {SessionUser.Rol}"

        HabilitarPorRol(SessionUser.Rol)
    End Sub

    ' ========= navegación =========
    Private Sub AbrirFormularioEnPanel(formHijo As Form)
        PanelGeneral.Controls.Clear()
        formHijo.TopLevel = False
        formHijo.FormBorderStyle = FormBorderStyle.None
        formHijo.Dock = DockStyle.Fill
        PanelGeneral.Controls.Add(formHijo)
        formHijo.Show()
    End Sub

    Private Sub BUsuarios_Click(sender As Object, e As EventArgs) Handles BUsuarios.Click
        AbrirFormularioEnPanel(New FormUsuarios())
    End Sub

    Private Sub BBuckup_Click(sender As Object, e As EventArgs) Handles BBuckup.Click
        AbrirFormularioEnPanel(New FormBackUp())
    End Sub

    Private Sub BVentas_Click(sender As Object, e As EventArgs) Handles BVentas.Click
        AbrirFormularioEnPanel(New FormVentas())
    End Sub

    Private Sub BClientes_Click(sender As Object, e As EventArgs) Handles BClientes.Click
        AbrirFormularioEnPanel(New FormClientes())
    End Sub

    Private Sub BProductos_Click(sender As Object, e As EventArgs) Handles BProductos.Click
        AbrirFormularioEnPanel(New FormProductos())
    End Sub

    Private Sub BReportes_Click(sender As Object, e As EventArgs) Handles BReportes.Click
        AbrirFormularioEnPanel(New FormReportes())
    End Sub

    Private Sub BSalir_Click(sender As Object, e As EventArgs) Handles BSalir.Click
        Application.Exit()
    End Sub

End Class
