Imports System.Data
Imports System.Data.SqlClient

Public Class FormUsuarios

    Private dtUsuarios As DataTable
    Private dtRoles As DataTable
    Private usuarioSeleccionadoOriginal As String = Nothing
    Private bloqueandoEvento As Boolean = False ' ← evita ejecuciones indeseadas de CellClick

    '==============================
    ' FORM LOAD
    '==============================
    Private Sub FormUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Gestión de Usuarios - BushmillDrinks"
        Me.WindowState = FormWindowState.Normal

        TBUsuario.ReadOnly = False
        TBUsuario.Enabled = True
        TBUsuario.BackColor = SystemColors.Window

        CargarRolesCombo()
        CargarUsuarios()

        DGVUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVUsuarios.MultiSelect = False
        DGVUsuarios.ClearSelection()

        BEditar.Enabled = False
        BSuspender.Enabled = False
        BAgregar.Text = "Nuevo"

    End Sub

    '==============================
    ' CARGAR ROLES
    '==============================
    Private Sub CargarRolesCombo()
        dtRoles = DataAccess.GetRoles()

        Dim rolesDisponibles As New List(Of String)
        For Each row As DataRow In dtRoles.Rows
            Dim tipoRol As String = row("tipo_rol").ToString()
            If tipoRol.ToLower() = "gerente" Then
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

    '==============================
    ' CARGAR USUARIOS EN EL DGV
    '==============================
    Private Sub CargarUsuarios()
        Try
            bloqueandoEvento = True ' ← evita que se dispare CellClick
            dtUsuarios = DataAccess.GetUsuarios()
            DGVUsuarios.DataSource = dtUsuarios
            usuarioSeleccionadoOriginal = Nothing

            ' Ocultar columnas no necesarias
            If DGVUsuarios.Columns.Contains("contrasena") Then DGVUsuarios.Columns("contrasena").Visible = False
            If DGVUsuarios.Columns.Contains("id_usuario") Then DGVUsuarios.Columns("id_usuario").Visible = False
            If DGVUsuarios.Columns.Contains("id_rol") Then DGVUsuarios.Columns("id_rol").Visible = False

            DGVUsuarios.ClearSelection()
        Finally
            bloqueandoEvento = False
        End Try
    End Sub

    '==============================
    ' VALIDACIONES
    '==============================
    Private Function ValidarCampos(Optional editar As Boolean = False) As Boolean
        If String.IsNullOrWhiteSpace(TBNombre.Text) OrElse Not TBNombre.Text.All(Function(c) Char.IsLetter(c) Or Char.IsWhiteSpace(c)) Then
            MessageBox.Show("El campo Nombre es obligatorio y solo puede contener letras.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBNombre.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(TBApellido.Text) OrElse Not TBApellido.Text.All(Function(c) Char.IsLetter(c) Or Char.IsWhiteSpace(c)) Then
            MessageBox.Show("El campo Apellido es obligatorio y solo puede contener letras.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBApellido.Focus()
            Return False
        End If

        If Not editar AndAlso (String.IsNullOrWhiteSpace(TBUsuario.Text) OrElse TBUsuario.Text.Contains(" ")) Then
            MessageBox.Show("Debe ingresar un nombre de usuario sin espacios.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBUsuario.Focus()
            Return False
        End If

        If Not editar AndAlso (String.IsNullOrWhiteSpace(TBContrasena.Text) OrElse TBContrasena.Text.Length < 6) Then
            MessageBox.Show("Debe ingresar una contraseña de al menos 6 caracteres.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBContrasena.Focus()
            Return False
        End If

        If CBRol.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar un Rol.", "Validación",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CBRol.Focus()
            Return False
        End If

        Return True
    End Function

    '==============================
    ' AGREGAR NUEVO USUARIO
    '==============================
    Private Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click
        ' Si el botón está en modo "Nuevo", limpiamos y preparamos el formulario
        If BAgregar.Text = "Nuevo" Then
            LimpiarCampos()
            TBUsuario.Focus()
            BAgregar.Text = "Guardar"
            BEditar.Enabled = False
            BSuspender.Enabled = False
            Return
        End If

        ' Si está en modo "Guardar", intentamos crear el usuario
        If Not ValidarCampos() Then Exit Sub

        If DataAccess.ExisteUsuario(TBUsuario.Text.Trim()) Then
            MessageBox.Show("El nombre de usuario ya existe.", "Duplicado",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBUsuario.Focus()
            TBUsuario.SelectAll()
            Exit Sub
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

            MessageBox.Show("Usuario creado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CargarUsuarios()
            CargarRolesCombo()
            LimpiarCampos()

            ' Volvemos al modo normal
            BAgregar.Text = "Nuevo"
            DGVUsuarios.ClearSelection()

        Catch ex As Exception
            MessageBox.Show($"Error al crear usuario: {ex.Message}", "Error",
                      MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==============================
    ' EDITAR USUARIO (sin cambiar usuario)
    '==============================
    Private Sub BEditar_Click(sender As Object, e As EventArgs) Handles BEditar.Click
        If DGVUsuarios.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un usuario para editar.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If Not ValidarCampos(editar:=True) Then Exit Sub

        Dim row As DataGridViewRow = DGVUsuarios.SelectedRows(0)
        usuarioSeleccionadoOriginal = row.Cells("nombre_usuario").Value.ToString()

        Dim nuevoIdRol As Integer = DataAccess.GetIdRolPorTipo(CBRol.Text.ToLower())

        Try
            DataAccess.UpdateUsuario(
                usuarioOriginal:=usuarioSeleccionadoOriginal,
                nombre:=TBNombre.Text.Trim(),
                apellido:=TBApellido.Text.Trim(),
                nuevaPassword:=If(String.IsNullOrWhiteSpace(TBContrasena.Text), Nothing, TBContrasena.Text),
                nuevoIdRol:=nuevoIdRol
            )

            MessageBox.Show("Usuario actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LimpiarCampos()
            CargarUsuarios()
            CargarRolesCombo()

        Catch ex As Exception
            MessageBox.Show($"Error al actualizar usuario: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==============================
    ' SUSPENDER / REACTIVAR USUARIO
    '==============================
    Private Sub BSuspender_Click(sender As Object, e As EventArgs) Handles BSuspender.Click
        If DGVUsuarios.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim row = DGVUsuarios.SelectedRows(0)
        Dim u = row.Cells("nombre_usuario").Value.ToString()
        Dim rol = row.Cells("rol").Value.ToString().ToLower()
        Dim estado = row.Cells("estado").Value.ToString()
        Dim activar = (estado.ToLower() = "inactivo")

        If rol = "gerente" Then
            MessageBox.Show("El usuario Gerente no puede desactivarse.", "Acción no permitida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            DataAccess.CambiarEstadoUsuario(u, activar)
            CargarUsuarios()
            MessageBox.Show(If(activar, "Usuario reactivado.", "Usuario suspendido."),
                        "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show($"Error al cambiar estado: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    '==============================
    ' SELECCIÓN EN EL DGV
    '==============================
    Private Sub DGVUsuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUsuarios.CellClick
        If bloqueandoEvento OrElse e.RowIndex < 0 Then Return

        Dim row = DGVUsuarios.Rows(e.RowIndex)
        TBUsuario.Text = row.Cells("nombre_usuario").Value.ToString()
        TBNombre.Text = row.Cells("nombre").Value.ToString()
        TBApellido.Text = row.Cells("apellido").Value.ToString()
        CBRol.Text = CapitalizarPrimeraLetra(row.Cells("rol").Value.ToString())

        usuarioSeleccionadoOriginal = row.Cells("nombre_usuario").Value.ToString()

        TBUsuario.ReadOnly = True
        TBUsuario.Enabled = False
        TBUsuario.BackColor = Color.LightGray

        BEditar.Enabled = True
        BSuspender.Enabled = True
    End Sub

    '==============================
    ' LIMPIAR CAMPOS
    '==============================
    Private Sub LimpiarCampos()
        TBUsuario.Clear()
        TBNombre.Clear()
        TBApellido.Clear()
        TBContrasena.Clear()
        CBRol.SelectedIndex = -1
        usuarioSeleccionadoOriginal = Nothing

        TBUsuario.ReadOnly = False
        TBUsuario.Enabled = True
        TBUsuario.BackColor = SystemColors.Window

        DGVUsuarios.ClearSelection()
        BEditar.Enabled = False
        BSuspender.Enabled = False
    End Sub

End Class



