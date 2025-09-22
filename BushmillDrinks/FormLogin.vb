Public Class FormLogin

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.AcceptButton = BIngresar
        If TypeOf TBContrasenia Is TextBox Then
            DirectCast(TBContrasenia, TextBox).UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub BIngresar_Click(sender As Object, e As EventArgs) Handles BIngresar.Click
        Dim user As String = TBUsuario.Text.Trim()
        Dim pass As String = TBContrasenia.Text

        If String.IsNullOrWhiteSpace(user) OrElse String.IsNullOrWhiteSpace(pass) Then
            MessageBox.Show("Ingrese usuario y contraseña.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBUsuario.Focus() : Exit Sub
        End If

        Try
            Dim r = DataAccess.TryLogin(user, pass)

            If Not r.Ok Then
                ' Si existe pero está inactivo, mensaje específico
                Dim existePeroInactivo = (r.Estado = "I")
                If existePeroInactivo Then
                    MessageBox.Show("El usuario está inactivo. Contacte al gerente.", "Acceso denegado",
                                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Else
                    MessageBox.Show("Usuario o contraseña incorrectos.", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                TBContrasenia.Clear()
                TBContrasenia.Focus()
                Exit Sub
            End If

            ' Guardar sesión
            SessionUser.Usuario = user
            SessionUser.Nombre = r.Nombre
            SessionUser.Rol = r.Rol

            MessageBox.Show($"Bienvenido {r.Nombre} ({r.Rol})", "Login correcto",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Abrir menú principal
            Dim f As New FormMenu()
            Me.Hide()
            f.Show()

        Catch ex As Exception
            MessageBox.Show("Error al autenticar: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BSalir_Click(sender As Object, e As EventArgs) Handles BSalir.Click
        Application.Exit()
    End Sub

End Class

