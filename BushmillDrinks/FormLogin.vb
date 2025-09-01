Public Class FormLogin
    Private usuarios As New Dictionary(Of String, String) From {
        {"admin", "admin"},
        {"vendedor", "1234"},
        {"gerente", "abcd"}
    }

    Private Sub BIngresar_Click(sender As Object, e As EventArgs) Handles BIngresar.Click
        Dim user As String = TBUsuario.Text.Trim()
        Dim pass As String = TBContrasenia.Text.Trim()

        If usuarios.ContainsKey(user) AndAlso usuarios(user) = pass Then
            MessageBox.Show("Bienvenido " & user, "Login Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Abrir menú principal
            Dim f As New FormMenu()
            Me.Hide()
            f.Show()
        Else
            MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub BSalir_Click(sender As Object, e As EventArgs) Handles BSalir.Click
        Application.Exit()
    End Sub

    Private Sub TBContrasenia_TextChanged(sender As Object, e As EventArgs) Handles TBContrasenia.TextChanged

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub FormLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PLogin_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub LContrasenia_Click(sender As Object, e As EventArgs) Handles LContrasenia.Click

    End Sub


End Class
