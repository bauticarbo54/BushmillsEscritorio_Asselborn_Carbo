Imports System.Data
Imports System.Data.SqlClient

Public Class FormUsuarios

    Private dtUsuarios As DataTable
    Private usuarioSeleccionadoOriginal As String = Nothing

    Private Sub FormUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Gestión de Usuarios - BushmillDrinks"
        Me.WindowState = FormWindowState.Normal

        CargarRoles()
        CargarUsuarios()

        DGVUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVUsuarios.MultiSelect = False
    End Sub

    Private Sub CargarRoles()
        Dim roles = New List(Of String) From {"Gerente", "Administrador", "Vendedor"}

        ' Ocultar "Gerente" si ya hay uno ACTIVO
        If DataAccess.ExisteGerenteActivo() Then
            roles.Remove("Gerente")
        End If

        CBRol.DataSource = roles
        CBRol.SelectedIndex = -1
    End Sub

    Private Sub CargarUsuarios()
        dtUsuarios = DataAccess.GetUsuarios()
        DGVUsuarios.DataSource = dtUsuarios
        usuarioSeleccionadoOriginal = Nothing
    End Sub

    Private Function ValidarCampos(Optional editar As Boolean = False) As Boolean
        If String.IsNullOrWhiteSpace(TBUsuario.Text) OrElse
           String.IsNullOrWhiteSpace(TBNombre.Text) OrElse
           (Not editar AndAlso String.IsNullOrWhiteSpace(TBContrasena.Text)) OrElse
           CBRol.SelectedIndex = -1 Then
            MessageBox.Show("Todos los campos son obligatorios.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            DataAccess.InsertUsuario(
                usuario:=TBUsuario.Text.Trim(),
                nombre:=TBNombre.Text.Trim(),
                passwordPlano:=TBContrasena.Text,
                rol:=CBRol.Text
            )
            MessageBox.Show("Usuario creado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LimpiarCampos()
            CargarUsuarios()
            CargarRoles() ' refresca por si se creó el Gerente activo
        Catch ex As SqlException When ex.Number = 50002
            MessageBox.Show(ex.Message, "Regla de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
            usuarioSeleccionadoOriginal = row.Cells("usuario").Value.ToString()
        End If

        Dim rolActualFila As String = row.Cells("rol").Value.ToString()
        Dim esMismoUsuario As Boolean =
        String.Equals(usuarioSeleccionadoOriginal, SessionUser.Usuario, StringComparison.OrdinalIgnoreCase)
        Dim esGerenteFila As Boolean =
        String.Equals(rolActualFila, "Gerente", StringComparison.OrdinalIgnoreCase)

        ' 1) No permitir cambiarte tu propio rol
        Dim nuevoRol As String = CBRol.Text
        If esMismoUsuario Then
            nuevoRol = Nothing   ' => DataAccess.UpdateUsuario NO cambia el rol
        End If

        ' 2) Blindar al Gerente: no permitir cambiar el rol del usuario Gerente
        '    (si querés permitirlo, sacá este bloque y dejará la validación al trigger de SQL)
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
            nuevaPassword:=If(String.IsNullOrWhiteSpace(TBContrasena.Text), Nothing, TBContrasena.Text),
            nuevoRol:=nuevoRol                    ' <- puede venir Nothing si es el mismo usuario
        )

            MessageBox.Show("Usuario actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            LimpiarCampos()
            CargarUsuarios()
            CargarRoles() ' refresca el combo por si cambió disponibilidad de Gerente

        Catch ex As SqlClient.SqlException When ex.Number = 50002
            ' Regla BD: solo 1 Gerente ACTIVO
            MessageBox.Show(ex.Message, "Regla de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub


    '----------- SUSPENDER / REACTIVAR -----------
    Private Sub BSuspender_Click(sender As Object, e As EventArgs) Handles BSuspender.Click
        If DGVUsuarios.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un usuario.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Dim row = DGVUsuarios.SelectedRows(0)
        Dim u = row.Cells("usuario").Value.ToString()
        Dim rol = row.Cells("rol").Value.ToString()
        Dim estado = row.Cells("estado").Value.ToString()
        Dim activar = (estado = "Inactivo")

        ' Bloqueos:
        If String.Equals(u, SessionUser.Usuario, StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("No podés desactivar tu propio usuario.", "Acción no permitida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If
        If String.Equals(rol, "Gerente", StringComparison.OrdinalIgnoreCase) Then
            MessageBox.Show("El usuario Gerente no puede desactivarse desde aquí.", "Acción no permitida",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            DataAccess.CambiarEstadoUsuario(u, activar)
            CargarUsuarios()
            CargarRoles()
            MessageBox.Show(If(activar, "Usuario reactivado.", "Usuario suspendido."),
                        "OK", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As SqlClient.SqlException When ex.Number = 50002
            MessageBox.Show(ex.Message, "Regla de negocio", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    '----------- Selección de la grilla -----------
    Private Sub DGVUsuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUsuarios.CellClick
        If e.RowIndex < 0 Then Return

        Dim row = DGVUsuarios.Rows(e.RowIndex)
        TBUsuario.Text = row.Cells("usuario").Value.ToString()
        TBNombre.Text = row.Cells("nombre").Value.ToString()
        TBContrasena.Clear()
        CBRol.Text = row.Cells("rol").Value.ToString()
        usuarioSeleccionadoOriginal = row.Cells("usuario").Value.ToString()

        Dim esGerente As Boolean = String.Equals(row.Cells("rol").Value.ToString(), "Gerente",
                                             StringComparison.OrdinalIgnoreCase)
        Dim esMismoUsuario As Boolean = String.Equals(usuarioSeleccionadoOriginal, SessionUser.Usuario,
                                                  StringComparison.OrdinalIgnoreCase)

        ' No permitir suspender: gerente o uno mismo
        Dim puedeSuspender = Not esGerente AndAlso Not esMismoUsuario
        BSuspender.Enabled = puedeSuspender
        If puedeSuspender Then
            ActualizarBotonSuspender(If(row.Cells("estado").Value.ToString() = "Activo", "Activo", "Inactivo"))
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
        TBContrasena.Clear()
        CBRol.SelectedIndex = -1
        usuarioSeleccionadoOriginal = Nothing
    End Sub

End Class

