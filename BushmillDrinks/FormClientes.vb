Public Class FormClientes

    Private Shared clientes As DataTable

    Public Property ClienteAgregado As DataRow
    Public Property CerrarAlAgregar As Boolean = False

    Public Sub Prefill(dni As String, Optional nombre As String = "", Optional telefono As String = "")
        If Not String.IsNullOrWhiteSpace(dni) Then TBDNI.Text = dni
        If Not String.IsNullOrWhiteSpace(nombre) Then TBNombre.Text = nombre
        If Not String.IsNullOrWhiteSpace(telefono) Then TBTelefono.Text = telefono
    End Sub

    Public Shared Function ExisteCliente(dni As String) As Boolean
        If clientes Is Nothing OrElse String.IsNullOrWhiteSpace(dni) Then Return False
        Return clientes.AsEnumerable().Any(Function(r) r.Field(Of String)("DNI") = dni _
                                     AndAlso r.Field(Of String)("Estado") = "Activo")
    End Function

    Public Shared Function ObtenerCliente(dni As String) As DataRow
        If clientes Is Nothing Then Return Nothing
        Return clientes.AsEnumerable().FirstOrDefault(Function(r) r.Field(Of String)("DNI") = dni)
    End Function

    Private Sub FormClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Gestión de Clientes"
        Me.WindowState = FormWindowState.Normal

        ' Configurar orden de tabulación
        TBDNI.TabIndex = 0
        TBNombre.TabIndex = 1
        TBTelefono.TabIndex = 2
        BAgregar.TabIndex = 3
        BEditar.TabIndex = 4
        BSuspender.TabIndex = 5
        DGVClientes.TabIndex = 6

        ' Crear la tabla solo si aún no existe
        If clientes Is Nothing Then
            PrepararTabla()
            DGVClientes.DataSource = clientes
        End If

        DGVClientes.DataSource = clientes

        ' Ajustes DataGridView
        DGVClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect

        ' Configurar validación de entrada
        ConfigureInputValidation()

        Me.AcceptButton = BAgregar
    End Sub

    Private Sub ConfigureInputValidation()
        ' Permitir solo números en DNI
        AddHandler TBDNI.KeyPress, AddressOf TBDNI_KeyPress

        ' Permitir solo letras y espacios en Nombre
        AddHandler TBNombre.KeyPress, AddressOf TBNombre_KeyPress

        ' Permitir solo números en Teléfono
        AddHandler TBTelefono.KeyPress, AddressOf TBTelefono_KeyPress
    End Sub

    ' --- VALIDACIÓN DE ENTRADA PARA DNI (SOLO NÚMEROS) ---
    Private Sub TBDNI_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Solo se permiten números en el campo DNI.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- VALIDACIÓN PARA NOMBRE (SOLO LETRAS Y ESPACIOS) ---
    Private Sub TBNombre_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsLetter(e.KeyChar) AndAlso e.KeyChar <> " "c Then
            e.Handled = True
            MessageBox.Show("Solo se permiten letras y espacios en el campo Nombre.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- VALIDACIÓN PARA TELÉFONO (SOLO NÚMEROS) ---
    Private Sub TBTelefono_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Solo se permiten números en el campo Teléfono.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub PrepararTabla()
        clientes = New DataTable()
        clientes.Columns.Add("DNI", GetType(String))
        clientes.Columns.Add("Nombre", GetType(String))
        clientes.Columns.Add("Telefono", GetType(String))
        clientes.Columns.Add("Estado", GetType(String))
    End Sub

    ' --- VALIDACIÓN GENERAL DE CAMPOS ---
    Private Function ValidarCampos() As Boolean
        ' Validar campos vacíos
        If String.IsNullOrWhiteSpace(TBDNI.Text) Then
            MessageBox.Show("El campo DNI es obligatorio.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBDNI.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(TBNombre.Text) Then
            MessageBox.Show("El campo Nombre es obligatorio.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBNombre.Focus()
            Return False
        End If

        If String.IsNullOrWhiteSpace(TBTelefono.Text) Then
            MessageBox.Show("El campo Teléfono es obligatorio.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBTelefono.Focus()
            Return False
        End If

        ' Validar longitud del DNI (7-8 dígitos)
        If TBDNI.Text.Length < 7 OrElse TBDNI.Text.Length > 8 Then
            MessageBox.Show("El DNI debe tener entre 7 y 8 dígitos.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBDNI.Focus()
            TBDNI.SelectAll()
            Return False
        End If

        ' Validar que el DNI contenga solo números
        If Not TBDNI.Text.All(Function(c) Char.IsDigit(c)) Then
            MessageBox.Show("El DNI solo puede contener números.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBDNI.Focus()
            TBDNI.SelectAll()
            Return False
        End If

        ' Validar formato del nombre
        If TBNombre.Text.Trim().Length < 5 Then
            MessageBox.Show("El nombre debe tener al menos 5 caracteres.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBNombre.Focus()
            TBNombre.SelectAll()
            Return False
        End If

        If Not TBNombre.Text.Trim().Contains(" ") Then
            MessageBox.Show("Por favor ingrese nombre y apellido.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBNombre.Focus()
            TBNombre.SelectAll()
            Return False
        End If

        If TBNombre.Text.Contains("  ") Then
            MessageBox.Show("El nombre no puede tener espacios consecutivos.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBNombre.Focus()
            TBNombre.SelectAll()
            Return False
        End If

        ' Validar formato del teléfono
        If TBTelefono.Text.Length < 8 OrElse TBTelefono.Text.Length > 15 Then
            MessageBox.Show("El teléfono debe tener entre 8 y 15 dígitos.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBTelefono.Focus()
            TBTelefono.SelectAll()
            Return False
        End If

        ' Validar que el teléfono contenga solo números
        If Not TBTelefono.Text.All(Function(c) Char.IsDigit(c)) Then
            MessageBox.Show("El teléfono solo puede contener números.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBTelefono.Focus()
            TBTelefono.SelectAll()
            Return False
        End If

        Return True
    End Function

    ' --- BOTÓN AGREGAR ---
    Private Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click
        ' Validar todos los campos antes de proceder
        If Not ValidarCampos() Then Exit Sub

        ' Verificar si el DNI ya existe
        Dim dniExistente = clientes.AsEnumerable().
        Where(Function(row) row.Field(Of String)("DNI") = TBDNI.Text).
        FirstOrDefault()

        If dniExistente IsNot Nothing Then
            MessageBox.Show("El DNI ya existe en la base de datos.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error)
            TBDNI.Focus()
            TBDNI.SelectAll()
            Exit Sub
        End If

        Dim nuevaFila = clientes.NewRow()
        nuevaFila("DNI") = TBDNI.Text
        nuevaFila("Nombre") = TBNombre.Text.Trim()
        nuevaFila("Telefono") = TBTelefono.Text
        nuevaFila("Estado") = "Activo"
        clientes.Rows.Add(nuevaFila)

        ClienteAgregado = nuevaFila

        If CerrarAlAgregar Then
            ' Abierto desde Ventas -> cerrar y devolver
            MessageBox.Show("Cliente agregado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
            Return
        Else
            ' Gestor de clientes -> permanecer abierto
            MessageBox.Show("Cliente agregado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
            DGVClientes.Refresh()
            LimpiarCampos()
            TBDNI.Focus()
            Return
        End If
    End Sub

    ' --- BOTÓN EDITAR ---
    Private Sub BEditar_Click(sender As Object, e As EventArgs) Handles BEditar.Click
        If DGVClientes.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un cliente para editar.", "Atención",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        ' Validar todos los campos antes de proceder
        If Not ValidarCampos() Then
            Exit Sub
        End If

        Dim row As DataGridViewRow = DGVClientes.SelectedRows(0)

        ' Verificar si el nuevo DNI ya existe (excepto en la fila actual)
        Dim dniNuevo = TBDNI.Text
        Dim dniActual = row.Cells("DNI").Value.ToString()

        If dniNuevo <> dniActual Then
            Dim dniExistente = clientes.AsEnumerable().
                Where(Function(r) r.Field(Of String)("DNI") = dniNuevo AndAlso
                                 r.Field(Of String)("DNI") <> dniActual).
                FirstOrDefault()

            If dniExistente IsNot Nothing Then
                MessageBox.Show("El DNI ya existe en otro cliente.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                TBDNI.Focus()
                TBDNI.SelectAll()
                Exit Sub
            End If
        End If

        row.Cells("DNI").Value = TBDNI.Text
        row.Cells("Nombre").Value = TBNombre.Text.Trim()
        row.Cells("Telefono").Value = TBTelefono.Text

        MessageBox.Show("Cliente actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)
        LimpiarCampos()
    End Sub

    ' --- BOTÓN SUSPENDER / REACTIVAR ---
    Private Sub BSuspender_Click(sender As Object, e As EventArgs) Handles BSuspender.Click
        If DGVClientes.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DGVClientes.SelectedRows(0)
            Dim estadoActual As String = row.Cells("Estado").Value.ToString()

            If estadoActual = "Activo" Then
                row.Cells("Estado").Value = "Inactivo"
                ActualizarBotonSuspender("Inactivo")
                MessageBox.Show("El cliente fue suspendido.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                row.Cells("Estado").Value = "Activo"
                ActualizarBotonSuspender("Activo")
                MessageBox.Show("El cliente fue reactivado.", "Información",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Seleccione un cliente primero.", "Atención",
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
    Private Sub DGVClientes_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVClientes.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVClientes.Rows(e.RowIndex)
            TBDNI.Text = row.Cells("DNI").Value.ToString()
            TBNombre.Text = row.Cells("Nombre").Value.ToString()
            TBTelefono.Text = row.Cells("Telefono").Value.ToString()

            ActualizarBotonSuspender(row.Cells("Estado").Value.ToString())
        End If
    End Sub

    ' --- FORMATEAR FILAS SEGÚN ESTADO ---
    Private Sub DGVClientes_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGVClientes.CellFormatting
        If DGVClientes.Columns(e.ColumnIndex).Name = "Estado" AndAlso e.Value IsNot Nothing Then
            If e.Value.ToString() = "Inactivo" Then
                DGVClientes.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Gray
            Else
                DGVClientes.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
            End If
        End If
    End Sub

    ' --- LIMPIAR CAMPOS ---
    Private Sub LimpiarCampos()
        TBDNI.Clear()
        TBNombre.Clear()
        TBTelefono.Clear()
    End Sub

    ' --- VALIDACIÓN EN TIEMPO REAL PARA DNI ---
    Private Sub TBDNI_TextChanged(sender As Object, e As EventArgs) Handles TBDNI.TextChanged
        If TBDNI.Text.Length > 8 Then
            TBDNI.Text = TBDNI.Text.Substring(0, 8)
            TBDNI.SelectionStart = TBDNI.Text.Length
            MessageBox.Show("El DNI no puede tener más de 8 dígitos.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- VALIDACIÓN EN TIEMPO REAL PARA NOMBRE ---
    Private Sub TBNombre_TextChanged(sender As Object, e As EventArgs) Handles TBNombre.TextChanged
        ' Limitar a 50 caracteres máximo para nombre
        If TBNombre.Text.Length > 50 Then
            TBNombre.Text = TBNombre.Text.Substring(0, 50)
            TBNombre.SelectionStart = TBNombre.Text.Length
            MessageBox.Show("El nombre no puede tener más de 50 caracteres.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- VALIDACIÓN EN TIEMPO REAL PARA TELÉFONO ---
    Private Sub TBTelefono_TextChanged(sender As Object, e As EventArgs) Handles TBTelefono.TextChanged
        ' Limitar a 15 caracteres máximo para teléfono
        If TBTelefono.Text.Length > 15 Then
            TBTelefono.Text = TBTelefono.Text.Substring(0, 15)
            TBTelefono.SelectionStart = TBTelefono.Text.Length
        End If
    End Sub

    ' --- VALIDACIÓN AL PERDER EL FOCO PARA DNI ---
    Private Sub TBDNI_Leave(sender As Object, e As EventArgs) Handles TBDNI.Leave
        If Not String.IsNullOrWhiteSpace(TBDNI.Text) Then
            If TBDNI.Text.Length < 7 Then
                MessageBox.Show("El DNI debe tener al menos 7 dígitos.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBDNI.Focus()
            End If
        End If
    End Sub

    ' --- VALIDACIÓN AL PERDER EL FOCO PARA TELÉFONO ---
    Private Sub TBTelefono_Leave(sender As Object, e As EventArgs) Handles TBTelefono.Leave
        If Not String.IsNullOrWhiteSpace(TBTelefono.Text) Then
            If TBTelefono.Text.Length < 8 Then
                MessageBox.Show("El teléfono debe tener al menos 8 dígitos.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBTelefono.Focus()
            End If
        End If
    End Sub

    ' --- VALIDACIÓN AL PERDER EL FOCO PARA NOMBRE ---
    Private Sub TBNombre_Leave(sender As Object, e As EventArgs) Handles TBNombre.Leave
        If Not String.IsNullOrWhiteSpace(TBNombre.Text) Then
            ' Normalizar espacios múltiples
            If TBNombre.Text.Contains("  ") Then
                TBNombre.Text = System.Text.RegularExpressions.Regex.Replace(TBNombre.Text.Trim(), "\s+", " ")
                TBNombre.SelectionStart = TBNombre.Text.Length
            End If

            ' Validar longitud mínima
            If TBNombre.Text.Trim().Length < 5 Then
                MessageBox.Show("El nombre debe tener al menos 5 caracteres.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBNombre.Focus()
            End If

            ' Validar que tenga nombre y apellido
            If Not TBNombre.Text.Trim().Contains(" ") Then
                MessageBox.Show("Por favor ingrese nombre y apellido.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBNombre.Focus()
            End If
        End If
    End Sub

End Class