Public Class FormUsuarios

    Private usuarios As DataTable

    Private Sub FormUsuarios_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Gestión de Usuarios"
        Me.WindowState = FormWindowState.Normal

        ' Crear la tabla solo si aún no existe
        If usuarios Is Nothing Then
            PrepararTabla()
            DGVUsuarios.DataSource = usuarios
        End If

        ' Ajustes DataGridView
        If DGVUsuarios.Columns.Contains("Rol") Then
            DGVUsuarios.Columns("Rol").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        End If

        ' Roles de prueba para combo
        If CBRol.Items.Count = 0 Then
            CBRol.Items.AddRange({"Administrador", "Vendedor"})
        End If

        ' Estilo de grilla
        DGVUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect
    End Sub

    Private Sub PrepararTabla()
        usuarios = New DataTable()
        usuarios.Columns.Add("Usuario", GetType(String))
        usuarios.Columns.Add("Nombre", GetType(String))
        usuarios.Columns.Add("Contraseña", GetType(String))
        usuarios.Columns.Add("Rol", GetType(String))
        usuarios.Columns.Add("Estado", GetType(String))
    End Sub

    ' --- BOTÓN AGREGAR ---
    Private Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click
        If String.IsNullOrWhiteSpace(TBUsuario.Text) OrElse
           String.IsNullOrWhiteSpace(TBNombre.Text) OrElse
           String.IsNullOrWhiteSpace(TBContrasena.Text) OrElse
           CBRol.SelectedIndex = -1 Then

            MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim nuevoId As Integer = usuarios.Rows.Count + 1

        usuarios.Rows.Add(TBUsuario.Text, TBNombre.Text, TBContrasena.Text, CBRol.Text, "Activo")

        LimpiarCampos()
    End Sub

    ' --- BOTÓN EDITAR ---
    Private Sub BEditar_Click(sender As Object, e As EventArgs) Handles BEditar.Click
        If DGVUsuarios.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DGVUsuarios.SelectedRows(0)
            row.Cells("Usuario").Value = TBUsuario.Text
            row.Cells("Nombre").Value = TBNombre.Text
            row.Cells("Contraseña").Value = TBContrasena.Text
            row.Cells("Rol").Value = CBRol.Text

            MessageBox.Show("Usuario actualizado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            LimpiarCampos()
        Else
            MessageBox.Show("Seleccione un usuario para editar.", "Atención",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- BOTÓN SUSPENDER / REACTIVAR ---
    Private Sub BSuspender_Click(sender As Object, e As EventArgs) Handles BSuspender.Click
        If DGVUsuarios.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DGVUsuarios.SelectedRows(0)
            Dim estadoActual As String = row.Cells("Estado").Value.ToString()

            If estadoActual = "Activo" Then
                row.Cells("Estado").Value = "Inactivo"
                ActualizarBotonSuspender("Inactivo")
                MessageBox.Show("El usuario fue suspendido.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                row.Cells("Estado").Value = "Activo"
                ActualizarBotonSuspender("Activo")
                MessageBox.Show("El usuario fue reactivado.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Seleccione un usuario primero.", "Atención",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
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

    ' --- SELECCIONAR FILA ---
    Private Sub DGVUsuarios_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUsuarios.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVUsuarios.Rows(e.RowIndex)
            TBUsuario.Text = row.Cells("Usuario").Value.ToString()
            TBNombre.Text = row.Cells("Nombre").Value.ToString()
            TBContrasena.Text = row.Cells("Contraseña").Value.ToString()
            CBRol.Text = row.Cells("Rol").Value.ToString()

            ActualizarBotonSuspender(row.Cells("Estado").Value.ToString())
        End If
    End Sub

    ' --- FORMATEAR FILAS SEGÚN ESTADO ---
    Private Sub DGVUsuarios_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGVUsuarios.CellFormatting
        If DGVUsuarios.Columns(e.ColumnIndex).Name = "Estado" AndAlso e.Value IsNot Nothing Then
            If e.Value.ToString() = "Inactivo" Then
                DGVUsuarios.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Gray
            Else
                DGVUsuarios.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
            End If
        End If
    End Sub

    ' --- LIMPIAR CAMPOS ---
    Private Sub LimpiarCampos()
        TBUsuario.Clear()
        TBNombre.Clear()
        TBContrasena.Clear()
        CBRol.SelectedIndex = -1
    End Sub

    Private Sub CBRol_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBRol.SelectedIndexChanged

    End Sub

    Private Sub TableLayoutPanel1_Paint(sender As Object, e As PaintEventArgs) Handles TableLayoutPanel1.Paint

    End Sub

    Private Sub DGVUsuarios_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVUsuarios.CellContentClick

    End Sub
End Class
