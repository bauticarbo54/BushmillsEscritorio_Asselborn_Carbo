Public Class FormProductos

    Private productos As DataTable

    Private Sub FormProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Gestión de Productos"
        Me.WindowState = FormWindowState.Normal ' Importante si es hijo del menú

        ' Crear la tabla solo si aún no existe
        If productos Is Nothing Then
            PrepararTabla()
            DGVProductos.DataSource = productos
        End If

        ' Formato del precio como moneda
        If DGVProductos.Columns.Contains("Precio") Then
            DGVProductos.Columns("Precio").DefaultCellStyle.Format = "C2"
            DGVProductos.Columns("Precio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        ' Datos de prueba para combos (solo una vez)
        If CBCategoria.Items.Count = 0 Then
            CBCategoria.Items.AddRange({"Vino", "Cerveza", "Whisky", "Vodka"})
        End If
        If CBMarca.Items.Count = 0 Then
            CBMarca.Items.AddRange({"Luigi Bosca", "Quilmes", "Johnnie Walker", "Absolut"})
        End If
    End Sub

    Private Sub PrepararTabla()
        productos = New DataTable()
        productos.Columns.Add("CodigoBarra", GetType(String))
        productos.Columns.Add("Categoria", GetType(String))
        productos.Columns.Add("Marca", GetType(String))
        productos.Columns.Add("Precio", GetType(Decimal))
        productos.Columns.Add("Volumen", GetType(String))
        productos.Columns.Add("Graduacion", GetType(String))
        productos.Columns.Add("Proveedor", GetType(String))
        productos.Columns.Add("Estado", GetType(String))
    End Sub

    ' --- ESCANEO O INGRESO MANUAL DEL CÓDIGO ---
    Private Sub TBCodBarra_KeyDown(sender As Object, e As KeyEventArgs) Handles TBCodBarra.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ProcesarCodigoBarra(TBCodBarra.Text.Trim())
        End If
    End Sub

    Private Sub ProcesarCodigoBarra(codigo As String)
        If String.IsNullOrWhiteSpace(codigo) Then Exit Sub

        ' Verificar si ya existe
        Dim encontrado = productos.Select($"CodigoBarra = '{codigo}'")
        If encontrado.Length > 0 Then
            MessageBox.Show("Este producto ya está cargado.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            TBCodBarra.Text = codigo
            CBCategoria.Focus()
        End If
    End Sub

    ' --- BOTÓN AGREGAR ---
    Private Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click
        If String.IsNullOrWhiteSpace(TBCodBarra.Text) OrElse
           CBCategoria.SelectedIndex = -1 OrElse
           String.IsNullOrWhiteSpace(TBProveedor.Text) OrElse
           NUDPrecio.Value <= 0 OrElse
           String.IsNullOrWhiteSpace(TBVolumen.Text) OrElse
           String.IsNullOrWhiteSpace(TBGraduacion.Text) OrElse
           CBMarca.SelectedIndex = -1 Then

            MessageBox.Show("Todos los campos son obligatorios.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        productos.Rows.Add(TBCodBarra.Text, CBCategoria.Text, CBMarca.Text,
                           NUDPrecio.Value, TBVolumen.Text, TBGraduacion.Text,
                           TBProveedor.Text, "Activo")

        LimpiarCampos()
    End Sub

    ' --- BOTÓN SUSPENDER/REACTIVAR ---
    Private Sub BSuspender_Click(sender As Object, e As EventArgs) Handles BSuspender.Click
        If DGVProductos.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DGVProductos.SelectedRows(0)
            Dim estadoActual As String = row.Cells("Estado").Value.ToString()

            If estadoActual = "Activo" Then
                row.Cells("Estado").Value = "Inactivo"
                ActualizarBotonSuspender("Inactivo")
                MessageBox.Show("El producto fue suspendido.", "Información",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                row.Cells("Estado").Value = "Activo"
                ActualizarBotonSuspender("Activo")
                MessageBox.Show("El producto fue reactivado.", "Información",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            MessageBox.Show("Seleccione un producto primero.", "Atención",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub


    ' --- BOTÓN EDITAR ---
    Private Sub BEditar_Click(sender As Object, e As EventArgs) Handles BEditar.Click
        If DGVProductos.SelectedRows.Count > 0 Then
            Dim row As DataGridViewRow = DGVProductos.SelectedRows(0)

            row.Cells("CodigoBarra").Value = TBCodBarra.Text
            row.Cells("Categoria").Value = CBCategoria.Text
            row.Cells("Marca").Value = CBMarca.Text
            row.Cells("Precio").Value = NUDPrecio.Value
            row.Cells("Volumen").Value = TBVolumen.Text
            row.Cells("Graduacion").Value = TBGraduacion.Text
            row.Cells("Proveedor").Value = TBProveedor.Text

            MessageBox.Show("Producto actualizado correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            LimpiarCampos()
        Else
            MessageBox.Show("Seleccione un producto para editar.", "Atención",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- CARGAR DATOS AL SELECCIONAR FILA ---
    Private Sub DGVProductos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVProductos.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DGVProductos.Rows(e.RowIndex)
            TBCodBarra.Text = row.Cells("CodigoBarra").Value.ToString()
            CBCategoria.Text = row.Cells("Categoria").Value.ToString()
            CBMarca.Text = row.Cells("Marca").Value.ToString()
            NUDPrecio.Value = Convert.ToDecimal(row.Cells("Precio").Value)
            TBVolumen.Text = row.Cells("Volumen").Value.ToString()
            TBGraduacion.Text = row.Cells("Graduacion").Value.ToString()
            TBProveedor.Text = row.Cells("Proveedor").Value.ToString()

            ' 🔹 Actualizar texto del botón según estado
            Dim estadoActual As String = row.Cells("Estado").Value.ToString()
            If estadoActual = "Activo" Then
                ActualizarBotonSuspender(estadoActual)
                BSuspender.Text = "Suspender"
            Else
                ActualizarBotonSuspender(estadoActual)
                BSuspender.Text = "Reactivar"
            End If


        End If
    End Sub

    ' --- FORMATEAR FILAS SEGÚN ESTADO ---
    Private Sub DGVProductos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGVProductos.CellFormatting
        If DGVProductos.Columns(e.ColumnIndex).Name = "Estado" AndAlso e.Value IsNot Nothing Then
            If e.Value.ToString() = "Inactivo" Then
                DGVProductos.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Gray
            Else
                DGVProductos.Rows(e.RowIndex).DefaultCellStyle.ForeColor = Color.Black
            End If
        End If
    End Sub

    ' --- LIMPIAR CAMPOS ---
    Private Sub LimpiarCampos()
        TBCodBarra.Clear()
        CBCategoria.SelectedIndex = -1
        CBMarca.SelectedIndex = -1
        NUDPrecio.Value = 0
        TBVolumen.Clear()
        TBGraduacion.Clear()
        TBProveedor.Clear()
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
End Class
