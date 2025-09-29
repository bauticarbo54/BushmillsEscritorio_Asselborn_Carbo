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
        ApplyButtonState(BBuckup, False)
        ApplyButtonState(BReportes, False)
        ApplyButtonState(BProductos, False)
        ApplyButtonState(BClientes, False)
        ApplyButtonState(BVentas, False)
    End Sub

    Private Sub HabilitarPorRol(rol As String)
        SetAllDisabled()

        Select Case rol
            Case "gerente"
                ApplyButtonState(BUsuarios, True)
                ApplyButtonState(BBuckup, True)
                ApplyButtonState(BReportes, True)

            Case "administrador"
                ApplyButtonState(BProductos, True)
                ApplyButtonState(BReportes, True)

            Case "vendedor"
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

    ' ========= BOTÓN SALIR ACTUALIZADO =========
    Private Sub BSalir_Click(sender As Object, e As EventArgs) Handles BSalir.Click
        ' Preguntar confirmación antes de salir
        Dim resultado As DialogResult = MessageBox.Show(
            "¿Está seguro que desea cerrar sesión?",
            "Confirmar cierre de sesión",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If resultado = DialogResult.Yes Then
            CerrarSesion()
        End If
    End Sub

    Private Sub CerrarSesion()
        ' Limpiar datos de sesión
        SessionUser.Usuario = Nothing
        SessionUser.Nombre = Nothing
        SessionUser.Rol = Nothing
        SessionUser.Estado = Nothing

        ' Cerrar este formulario
        Me.Hide()

        ' Mostrar formulario de login
        Dim formLogin As New FormLogin()
        formLogin.Show()

        ' Opcional: limpiar el panel si hay algún formulario abierto
        PanelGeneral.Controls.Clear()
    End Sub

    ' Opcional: Manejar el cierre del formulario con la X
    Private Sub FormMenu_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ' Si el usuario cierra con la X, preguntar si quiere cerrar sesión
        If e.CloseReason = CloseReason.UserClosing Then
            Dim resultado As DialogResult = MessageBox.Show(
                "¿Desea cerrar sesión y salir de la aplicación?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question)

            If resultado = DialogResult.Yes Then
                CerrarSesion()
                Application.Exit() ' Cerrar completamente la aplicación
            Else
                e.Cancel = True ' Cancelar el cierre
            End If
        End If
    End Sub
End Class
