Imports System.Data
Imports System.Data.SqlClient

Public Class FormUsuarios

    Private dtUsuarios As DataTable
    Private dtRoles As DataTable
    Private usuarioSeleccionadoOriginal As String = Nothing

    Private Sub FormUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Gestión de Usuarios - BushmillDrinks"
        Me.WindowState = FormWindowState.Normal

        CargarRolesCombo()
        CargarUsuarios()

        DGVUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVUsuarios.MultiSelect = False
    End Sub

    Private Sub CargarRolesCombo()
        dtRoles = DataAccess.GetRoles()

        ' Filtrar para ocultar "gerente" si ya existe uno activo
        Dim rolesDisponibles As New List(Of String)
        For Each row As DataRow In dtRoles.Rows
            Dim tipoRol As String = row("tipo_rol").ToString()
            If tipoRol = "gerente" Then
                If Not DataAccess.ExisteGerenteActivo() Then
                    rolesDisponibles.Add(CapitalizarPrimeraLetra(tipoRol))
                End If
            Else
                rolesDisponibles.Add(CapitalizarPrimeraLetra(tipoRol))
            End If
        Next

        CBRol.DataSource = rolesDisponibles
        CBRol.SelectedIndex = -1
    End Sub

    Private Function CapitalizarPrimeraLetra(texto As String) As String
        If String.IsNullOrEmpty(texto) Then Return texto
        Return Char.ToUpper(texto(0)) & texto.Substring(1).ToLower()
    End Function

    Private Sub CargarUsuarios()
        dtUsuarios = DataAccess.GetUsuarios()
        DGVUsuarios.DataSource = dtUsuarios
        usuarioSeleccionadoOriginal = Nothing

        ' Ocultar columnas sensibles y ajustar visualización
        If DGVUsuarios.Columns.Contains("contrasena") Then
            DGVUsuarios.Columns("contrasena").Visible = False
        End If
        If DGVUsuarios.Columns.Contains("id_usuario") Then
            DGVUsuarios.Columns("id_usuario").Visible = False
        End If
        If DGVUsuarios.Columns.Contains("id_rol") Then
            DGVUsuarios.Columns("id_rol").Visible = False
        End If

        ' Asegurar que el rol se muestre capitalizado
        If DGVUsuarios.Columns.Contains("rol") Then
            For Each row As DataGridViewRow In DGVUsuarios.Rows
                If Not row.IsNewRow AndAlso row.Cells("rol").Value IsNot Nothing Then
                    row.Cells("rol").Value = CapitalizarPrimeraLetra(row.Cells("rol").Value.ToString())
                End If
            Next
        End If
    End Sub

    Private Function ValidarCampos(Optional editar As Boolean = False) As Boolean
        ' Usuario obligatorio, sin espacios
        If String.IsNullOrWhiteSpace(TBUsuario.Text) OrElse TBUsuario.Text.Contains(" ") Then
            MessageBox.Show("Debe ingresar un nombre de usuario sin espacios.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBUsuario.Focus()
            Return False
        End If

        ' Nombre obligatorio, solo letras
        If String.IsNullOrWhiteSpace(TBNombre.Text) OrElse Not TBNombre.Text.All(Function(c) Char.IsLetter(c) Or Char.IsWhiteSpace(c)) Then
            MessageBox.Show("El campo Nombre es obligatorio y solo puede contener letras.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBNombre.Focus()
            Return False
        End If

        ' Apellido obligatorio, solo letras
        If String.IsNullOrWhiteSpace(TBApellido.Text) OrElse Not TBApellido.Text.All(Function(c) Char.IsLetter(c) Or Char.IsWhiteSpace(c)) Then
            MessageBox.Show("El campo Apellido es obligatorio y solo puede contener letras.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBApellido.Focus()
            Return False
        End If

        ' Contraseña obligatoria al agregar
        If Not editar AndAlso (String.IsNullOrWhiteSpace(TBContrasena.Text) OrElse TBContrasena.Text.Length < 6) Then
            MessageBox.Show("Debe ingresar una contraseña de al menos 6 caracteres.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBContrasena.Focus()
            Return False
        End If

        ' Rol obligatorio
        If CBRol.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un Rol.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CBRol.Focus()
            Return False
        End If

        Return True
    End Function

    '----------- AGREGAR -----------
    Private Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click
        If Not ValidarCampos() Then Exit Sub

        If DataAccess.ExisteUsuario(TBUsuario.Text.Trim()) Then
            MessageBox.Show("El nombre de usuario ya existe.", "Duplicado",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBUsuario.Focus() : TBUsuario.SelectAll() : Exit Sub
        End If

        Try
            Dim idRol As Integer = DataAccess.GetIdRolPorTipo(CBRol.Text.ToLower())

            DataAccess.InsertUsuario(
                usuario:=TBUsuario.Text.Trim(),
                nombre:=TBNombre.Text.Trim(),
                apellido:=TBApellido.Text.Trim(),
                passwordPlano:=TBContrasena.Text,
                idRol:=idRol
            )
            MessageBox.Show("Usuario creado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LimpiarCampos()
            CargarUsuarios()
            CargarRolesCombo() ' refresca por si se creó el Gerente activo
        Catch ex As SqlException When ex.Number = 50002
            MessageBox.Show(ex.Message, "Regla de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show($"Error al crear usuario: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '----------- EDITAR -----------
    Private Sub BEditar_Click(sender As Object, e As EventArgs) Handles BEditar.Click
        If DGVUsuarios.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un usuario para editar.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Not ValidarCampos(editar:=True) Then Exit Sub

        Dim row As DataGridViewRow = DGVUsuarios.SelectedRows(0)
        If String.IsNullOrEmpty(usuarioSeleccionadoOriginal) Then
            usuarioSeleccionadoOriginal = row.Cells("nombre_usuario").Value.ToString()
        End If

        Dim rolActualFila As String = row.Cells("rol").Value.ToString().ToLower()
        Dim esMismoUsuario As Boolean =
        String.Equals(usuarioSeleccionadoOriginal, SessionUser.Usuario, StringComparison.OrdinalIgnoreCase)
        Dim esGerenteFila As Boolean = (rolActualFila = "gerente")

        ' 1) No permitir cambiarte tu propio rol
        Dim nuevoIdRol As Integer? = Nothing
        If Not esMismoUsuario Then
            nuevoIdRol = DataAccess.GetIdRolPorTipo(CBRol.Text.ToLower())
        End If

        ' 2) Blindar al Gerente: no permitir cambiar el rol del usuario Gerente
        If esGerenteFila AndAlso Not String.Equals(CBRol.Text, "Gerente", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("El rol del Gerente no puede modificarse desde este formulario.",
                        "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If esMismoUsuario AndAlso Not String.Equals(TBUsuario.Text.Trim(), usuarioSeleccionadoOriginal, StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("No podés cambiar tu propio nombre de usuario.", "Acción no permitida",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            DataAccess.UpdateUsuario(
                usuarioOriginal:=usuarioSeleccionadoOriginal,
                usuarioNuevo:=TBUsuario.Text.Trim(),
                nombre:=TBNombre.Text.Trim(),
                apellido:=TBApellido.Text.Trim(),
                nuevaPassword:=If(String.IsNullOrWhiteSpace(TBContrasena.Text), Nothing, TBContrasena.Text),
                nuevoIdRol:=nuevoIdRol
            )

            MessageBox.Show("Usuario actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LimpiarCampos()
            CargarUsuarios()
            CargarRolesCombo() ' refresca el combo por si cambió disponibilidad de Gerente

        Catch ex As SqlClient.SqlException When ex.Number = 50002
            ' Regla BD: solo 1 Gerente ACTIVO
            MessageBox.Show(ex.Message, "Regla de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show($"Error al actualizar usuario: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '----------- SUSPENDER / REACTIVAR -----------
    Private Sub BSuspender_Click(sender As Object, e As EventArgs) Handles BSuspender.Click
        If DGVUsuarios.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim row = DGVUsuarios.SelectedRows(0)
        Dim u = row.Cells("nombre_usuario").Value.ToString()
        Dim rol = row.Cells("rol").Value.ToString().ToLower()
        Dim estado = row.Cells("estado").Value.ToString()
        Dim activar = (estado = "I") ' Ahora estado es 'A' o 'I'

        ' Bloqueos:
        If String.Equals(u, SessionUser.Usuario, StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("No podés desactivar tu propio usuario.", "Acción no permitida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If rol = "gerente" Then
            MessageBox.Show("El usuario Gerente no puede desactivarse desde aquí.", "Acción no permitida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            DataAccess.CambiarEstadoUsuario(u, activar)
            CargarUsuarios()
            CargarRolesCombo()
            MessageBox.Show(If(activar, "Usuario reactivado.", "Usuario suspendido."),
                        "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As SqlClient.SqlException When ex.Number = 50002
            MessageBox.Show(ex.Message, "Regla de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            MessageBox.Show($"Error al cambiar estado: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '----------- Selección de la grilla -----------
    Private Sub DGVUsuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUsuarios.CellClick
        If e.RowIndex < 0 Then Return

        Dim row = DGVUsuarios.Rows(e.RowIndex)
        TBUsuario.Text = row.Cells("nombre_usuario").Value.ToString()
        TBNombre.Text = row.Cells("nombre").Value.ToString()
        TBApellido.Text = row.Cells("apellido").Value.ToString()
        CBRol.Text = CapitalizarPrimeraLetra(row.Cells("rol").Value.ToString())
        usuarioSeleccionadoOriginal = row.Cells("nombre_usuario").Value.ToString()

        Dim esGerente As Boolean = (row.Cells("rol").Value.ToString().ToLower() = "gerente")
        Dim esMismoUsuario As Boolean = String.Equals(usuarioSeleccionadoOriginal, SessionUser.Usuario,
                                                  StringComparison.OrdinalIgnoreCase)

        ' No permitir suspender: gerente o uno mismo
        Dim puedeSuspender = Not esGerente AndAlso Not esMismoUsuario
        BSuspender.Enabled = puedeSuspender
        If puedeSuspender Then
            Dim estado As String = If(row.Cells("estado").Value.ToString() = "A", "Activo", "Inactivo")
            ActualizarBotonSuspender(estado)
        Else
            ' Mostrar como deshabilitado
            BSuspender.Text = "Suspender"
            BSuspender.BackColor = Color.LightGray
            BSuspender.ForeColor = Color.Black
        End If
    End Sub

    Private Sub ActualizarBotonSuspender(estado As String)
        If estado = "Activo" Then
            BSuspender.Text = "Suspender"
            BSuspender.BackColor = Color.Red
            BSuspender.ForeColor = Color.White
        Else
            BSuspender.Text = "Reactivar"
            BSuspender.BackColor = Color.Green
            BSuspender.ForeColor = Color.White
        End If
    End Sub

    Private Sub LimpiarCampos()
        TBUsuario.Clear()
        TBNombre.Clear()
        TBApellido.Clear()
        TBContrasena.Clear()
        CBRol.SelectedIndex = -1
        usuarioSeleccionadoOriginal = Nothing
        BSuspender.Enabled = True
        BSuspender.Text = "Suspender"
        BSuspender.BackColor = Color.Red
        BSuspender.ForeColor = Color.White
    End Sub
End Class

