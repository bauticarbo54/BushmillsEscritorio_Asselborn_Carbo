Imports System.Data.SqlClient

Public Class FormVentas

    Private detalle As DataTable
    Private totalVenta As Decimal = 0D
    Private usuarioActual As String = ""

    ' Índice de fila seleccionada en el DGV (-1 = ninguna)
    Private selectedRowIndex As Integer = -1

    ' Constructor que recibe el usuario logueado
    Public Sub New(usuario As String)
        InitializeComponent()
        usuarioActual = usuario
    End Sub

    ' Constructor por defecto
    Public Sub New()
        InitializeComponent()
        usuarioActual = "vendedor" ' Por defecto
    End Sub

    ' =============================
    ' ============ LOAD ===========
    ' =============================
    Private Sub FormVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Registro de Ventas"
        Me.WindowState = FormWindowState.Normal

        ' Mostrar información del vendedor
        LVendedorID.Text = usuarioActual
        LNombreVend.Text = ObtenerNombreVendedor(usuarioActual)

        ' Tabla del detalle
        PrepararTabla()
        DGVDetalleVenta.DataSource = detalle
        DGVDetalleVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVDetalleVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVDetalleVenta.MultiSelect = False

        ' Configurar validación de entrada
        ConfigureInputValidation()

        ' Total inicial
        LTotal.Text = totalVenta.ToString("C2")

        ' Foco
        TBDNICliente.Focus()
    End Sub

    Private Function ObtenerNombreVendedor(usuario As String) As String
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim cmd As New SqlCommand("SELECT nombre FROM Usuario WHERE nombre_usuario = @usuario", cn)
                cmd.Parameters.AddWithValue("@usuario", usuario)
                cn.Open()
                Dim resultado = cmd.ExecuteScalar()
                Return If(resultado IsNot Nothing, resultado.ToString(), usuario)
            End Using
        Catch ex As Exception
            Return usuario
        End Try
    End Function

    Private Sub ConfigureInputValidation()
        ' Validación para código de barras
        AddHandler TBCodBarra.KeyPress, AddressOf TBCodBarra_KeyPress
        AddHandler TBCodBarra.TextChanged, AddressOf TBCodBarra_TextChanged

        ' Validación para cantidad
        AddHandler NUDCantidad.ValueChanged, AddressOf NUDCantidad_ValueChanged
    End Sub

    Private Sub PrepararTabla()
        detalle = New DataTable()
        detalle.Columns.Add("CodigoBarra", GetType(String))
        detalle.Columns.Add("Producto", GetType(String))
        detalle.Columns.Add("Marca", GetType(String))
        detalle.Columns.Add("Categoria", GetType(String))
        detalle.Columns.Add("Cantidad", GetType(Integer))
        detalle.Columns.Add("PrecioUnitario", GetType(Decimal))
        detalle.Columns.Add("Subtotal", GetType(Decimal))
    End Sub

    ' ===========================================
    ' =========== UTILIDADES / TOTAL ============
    ' ===========================================
    Private Sub RecalcularTotal()
        Dim suma As Decimal = 0D
        For Each r As DataRow In detalle.Rows
            If r.RowState <> DataRowState.Deleted Then
                suma += Convert.ToDecimal(r("Subtotal"))
            End If
        Next
        totalVenta = suma
        LTotal.Text = totalVenta.ToString("C2")
    End Sub

    Private Sub LimpiarCamposProducto(Optional limpiarSeleccion As Boolean = True)
        TBCodBarra.Clear()
        NUDCantidad.Value = 1
        If limpiarSeleccion Then
            selectedRowIndex = -1
            BAgregarProducto.Text = "Agregar Producto"
            DGVDetalleVenta.ClearSelection()
        End If
        TBCodBarra.Focus()
    End Sub

    Private Function BuscarFilaPorCodigo(cod As String) As Integer
        For i As Integer = 0 To detalle.Rows.Count - 1
            If detalle.Rows(i).RowState <> DataRowState.Deleted AndAlso
               String.Equals(detalle.Rows(i)("CodigoBarra").ToString(), cod, StringComparison.OrdinalIgnoreCase) Then
                Return i
            End If
        Next
        Return -1
    End Function

    ' ===========================================
    ' ======== VALIDACIONES DE CLIENTE ==========
    ' ===========================================

    ' Solo dígitos en DNI
    Private Sub TBDNICliente_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TBDNICliente.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    ' Limitar DNI a 8 caracteres
    Private Sub TBDNICliente_TextChanged(sender As Object, e As EventArgs) Handles TBDNICliente.TextChanged
        If TBDNICliente.Text.Length > 8 Then
            TBDNICliente.Text = TBDNICliente.Text.Substring(0, 8)
            TBDNICliente.SelectionStart = TBDNICliente.Text.Length
        End If
    End Sub

    ' Enter en DNI -> validar/buscar
    Private Sub TBDNICliente_KeyDown(sender As Object, e As KeyEventArgs) Handles TBDNICliente.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            ValidarYResolverCliente()
        End If
    End Sub

    ' Valida formato y existencia; si no existe, ofrece alta
    Private Function ValidarYResolverCliente() As Boolean
        Dim dni = TBDNICliente.Text.Trim()

        If String.IsNullOrWhiteSpace(dni) Then
            MessageBox.Show("Ingrese el DNI del cliente.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBDNICliente.Focus()
            Return False
        End If

        If dni.Length < 7 OrElse dni.Length > 8 Then
            MessageBox.Show("El DNI debe tener entre 7 y 8 dígitos.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBDNICliente.Focus()
            TBDNICliente.SelectAll()
            Return False
        End If

        Dim dr = FormClientes.ObtenerCliente(dni)
        If dr IsNot Nothing Then
            LNombreClienteConf.Text = dr("nombre").ToString()
            Return True
        End If

        ' No existe -> proponer alta
        Dim resp = MessageBox.Show("El cliente no existe. ¿Desea registrarlo ahora?",
                                   "Cliente no encontrado",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If resp = DialogResult.Yes Then
            Using frm As New FormClientes()
                frm.CerrarAlAgregar = True
                frm.Prefill(dni)
                frm.StartPosition = FormStartPosition.CenterParent
                If frm.ShowDialog(Me) = DialogResult.OK AndAlso frm.ClienteAgregado IsNot Nothing Then
                    TBDNICliente.Text = frm.ClienteAgregado("DNI").ToString()
                    LNombreClienteConf.Text = frm.ClienteAgregado("Nombre").ToString()
                    Return True
                End If
            End Using
        End If

        LNombreClienteConf.Text = ""
        Return False
    End Function

    ' ===========================================
    ' ======== VALIDACIONES DE PRODUCTOS =========
    ' ===========================================

    ' --- VALIDACIÓN EN TIEMPO REAL PARA CÓDIGO DE BARRAS ---
    Private Sub TBCodBarra_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Solo se permiten números en el código de barras.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub TBCodBarra_TextChanged(sender As Object, e As EventArgs)
        ' Limitar longitud del código de barras
        If TBCodBarra.Text.Length > 50 Then
            TBCodBarra.Text = TBCodBarra.Text.Substring(0, 50)
            TBCodBarra.SelectionStart = TBCodBarra.Text.Length
            MessageBox.Show("El código de barras no puede tener más de 50 dígitos.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- VALIDACIÓN DE CANTIDAD MÁXIMA ---
    Private Sub NUDCantidad_ValueChanged(sender As Object, e As EventArgs)
        If NUDCantidad.Value > 1000 Then
            NUDCantidad.Value = 1000
            MessageBox.Show("La cantidad no puede ser mayor a 1000 unidades.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- VALIDACIÓN COMPLETA DE PRODUCTO ---
    Private Function ValidarProducto() As Boolean
        ' Validar código de barras
        If String.IsNullOrWhiteSpace(TBCodBarra.Text) Then
            MessageBox.Show("Ingrese el código de barra.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            Return False
        End If

        ' Validar longitud mínima del código de barras
        If TBCodBarra.Text.Trim().Length < 3 Then
            MessageBox.Show("El código de barras debe tener al menos 3 caracteres.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            TBCodBarra.SelectAll()
            Return False
        End If

        ' Validar que el código de barras contenga solo números
        If Not TBCodBarra.Text.Trim().All(Function(c) Char.IsDigit(c)) Then
            MessageBox.Show("El código de barras solo puede contener números.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            TBCodBarra.SelectAll()
            Return False
        End If

        ' Validar cantidad
        If NUDCantidad.Value <= 0 Then
            MessageBox.Show("La cantidad debe ser mayor a cero.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NUDCantidad.Focus()
            NUDCantidad.Select(0, NUDCantidad.Value.ToString().Length)
            Return False
        End If

        ' Validar producto existente
        If Not ValidarProductoExistente(TBCodBarra.Text.Trim()) Then
            TBCodBarra.Focus()
            TBCodBarra.SelectAll()
            Return False
        End If

        ' Validar stock disponible
        If Not ValidarStockDisponible(TBCodBarra.Text.Trim(), Convert.ToInt32(NUDCantidad.Value)) Then
            MessageBox.Show("Stock insuficiente para este producto.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NUDCantidad.Focus()
            NUDCantidad.Select(0, NUDCantidad.Value.ToString().Length)
            Return False
        End If

        Return True
    End Function

    ' --- VALIDACIÓN DE PRODUCTO EXISTENTE ---
    Private Function ValidarProductoExistente(codigoBarra As String) As Boolean
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim cmd As New SqlCommand("SELECT 1 FROM Producto WHERE codigo_barras = @codigo AND estado = 'A'", cn)
                cmd.Parameters.AddWithValue("@codigo", codigoBarra)
                cn.Open()
                Return cmd.ExecuteScalar() IsNot Nothing
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al verificar producto: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ' --- VALIDACIÓN DE STOCK DISPONIBLE ---
    Private Function ValidarStockDisponible(codigoBarra As String, cantidadSolicitada As Integer) As Boolean
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim cmd As New SqlCommand("SELECT stock FROM Producto WHERE codigo_barras = @codigo AND estado = 'A'", cn)
                cmd.Parameters.AddWithValue("@codigo", codigoBarra)
                cn.Open()
                Dim stockDisponible = cmd.ExecuteScalar()

                If stockDisponible IsNot Nothing Then
                    Dim stock As Integer = Convert.ToInt32(stockDisponible)
                    If cantidadSolicitada > stock Then
                        MessageBox.Show($"Stock insuficiente. Disponible: {stock} unidades", "Validación",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False
                    End If
                    Return True
                Else
                    MessageBox.Show("Producto no encontrado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return False
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al verificar stock: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ' --- OBTENER DATOS DEL PRODUCTO DESDE BD ---
    Private Function ObtenerDatosProducto(codigoBarra As String) As (Producto As String, Marca As String, Categoria As String, Precio As Decimal, Stock As Integer)
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim cmd As New SqlCommand("SELECT p.codigo_barras, m.nombre as marca, c.nombre as categoria, p.precio, p.stock " &
                                          "FROM Producto p " &
                                          "INNER JOIN Marca m ON p.id_marca = m.id_marca " &
                                          "INNER JOIN Categoria c ON p.id_categoria = c.id_categoria " &
                                          "WHERE p.codigo_barras = @codigo AND p.estado = 'A'", cn)
                cmd.Parameters.AddWithValue("@codigo", codigoBarra)
                cn.Open()

                Using reader = cmd.ExecuteReader()
                    If reader.Read() Then
                        Dim producto = $"{reader("marca")} - {reader("categoria")}"
                        Dim marca = reader("marca").ToString()
                        Dim categoria = reader("categoria").ToString()
                        Dim precio = Convert.ToDecimal(reader("precio"))
                        Dim stock = Convert.ToInt32(reader("stock"))

                        Return (producto, marca, categoria, precio, stock)
                    End If
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al obtener datos del producto: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return ("", "", "", 0D, 0)
    End Function

    ' --- VALIDACIÓN DE STOCK FINAL ---
    Private Function ValidarStockFinal() As Boolean
        For Each row As DataRow In detalle.Rows
            If row.RowState <> DataRowState.Deleted Then
                Dim codigoBarra As String = row("CodigoBarra").ToString()
                Dim cantidad As Integer = Convert.ToInt32(row("Cantidad"))

                If Not ValidarStockDisponible(codigoBarra, cantidad) Then
                    MessageBox.Show($"Stock insuficiente para el producto: {row("Producto")}", "Validación",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Return False
                End If
            End If
        Next
        Return True
    End Function

    ' --- VALIDACIÓN COMPLETA ANTES DE CONFIRMAR VENTA ---
    Private Function ValidarVentaCompleta() As Boolean
        ' 1. Validar cliente
        If Not ValidarYResolverCliente() Then
            Return False
        End If

        ' 2. Validar que haya al menos un producto
        If detalle.Rows.Count = 0 Then
            MessageBox.Show("Agregue al menos un producto a la venta.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            Return False
        End If

        ' 3. Validar que ningún producto tenga cantidad cero
        For Each row As DataRow In detalle.Rows
            If row.RowState <> DataRowState.Deleted AndAlso Convert.ToInt32(row("Cantidad")) <= 0 Then
                MessageBox.Show("Todos los productos deben tener cantidad mayor a cero.", "Validación",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If
        Next

        ' 4. Validar que el total sea mayor a cero
        If totalVenta <= 0 Then
            MessageBox.Show("El total de la venta debe ser mayor a cero.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        ' 5. Validar stock final antes de confirmar
        If Not ValidarStockFinal() Then
            Return False
        End If

        Return True
    End Function

    ' ===========================================
    ' ====== SELECCIÓN EN EL DGV (EDITAR) =======
    ' ===========================================
    Private Sub DGVDetalleVenta_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVDetalleVenta.CellClick
        If e.RowIndex >= 0 Then
            selectedRowIndex = e.RowIndex
            Dim row = DGVDetalleVenta.Rows(selectedRowIndex)
            TBCodBarra.Text = row.Cells("CodigoBarra").Value.ToString()
            NUDCantidad.Value = Convert.ToDecimal(row.Cells("Cantidad").Value)
            BAgregarProducto.Text = "Actualizar"
        End If
    End Sub

    ' ===========================================
    ' ============ AGREGAR / ACTUALIZAR =========
    ' ===========================================
    Private Sub BAgregarProducto_Click(sender As Object, e As EventArgs) Handles BAgregarProducto.Click
        If Not ValidarProducto() Then Exit Sub

        ' Obtener datos del producto desde BD
        Dim datosProducto = ObtenerDatosProducto(TBCodBarra.Text.Trim())
        If datosProducto.Precio <= 0 Then
            MessageBox.Show("No se pudo obtener el precio del producto.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim cod As String = TBCodBarra.Text.Trim()
        Dim cant As Integer = Convert.ToInt32(NUDCantidad.Value)
        Dim precio As Decimal = datosProducto.Precio

        ' Si hay fila seleccionada -> ACTUALIZAR o ELIMINAR
        If selectedRowIndex >= 0 AndAlso selectedRowIndex < DGVDetalleVenta.Rows.Count Then
            Dim r As DataRow = CType(DGVDetalleVenta.Rows(selectedRowIndex).DataBoundItem, DataRowView).Row

            If cant = 0 Then
                ' Eliminar item
                r.Delete()
                LimpiarCamposProducto()
                RecalcularTotal()
                Return
            Else
                ' Validar stock para la actualización
                If Not ValidarStockDisponible(cod, cant) Then
                    Exit Sub
                End If

                ' Actualizar cantidad y subtotal
                r("Cantidad") = cant
                r("Subtotal") = cant * Convert.ToDecimal(r("PrecioUnitario"))
                RecalcularTotal()
                LimpiarCamposProducto()
                Return
            End If
        End If

        ' Si NO hay fila seleccionada -> AGREGAR o ACUMULAR
        Dim idxExistente = BuscarFilaPorCodigo(cod)
        If idxExistente >= 0 Then
            ' Acumular cantidad
            Dim r As DataRow = detalle.Rows(idxExistente)
            Dim nuevaCant As Integer = Convert.ToInt32(r("Cantidad")) + cant

            ' Validar stock para la acumulación
            If Not ValidarStockDisponible(cod, nuevaCant) Then
                Exit Sub
            End If

            r("Cantidad") = nuevaCant
            r("Subtotal") = nuevaCant * Convert.ToDecimal(r("PrecioUnitario"))
        Else
            ' Crear nueva fila
            Dim subtotal As Decimal = cant * precio
            detalle.Rows.Add(cod, datosProducto.Producto, datosProducto.Marca, datosProducto.Categoria, cant, precio, subtotal)
        End If

        RecalcularTotal()
        LimpiarCamposProducto()
    End Sub

    ' ===========================================
    ' ============ CONFIRMAR VENTA ==============
    ' ===========================================
    Private Sub BConfirmarVenta_Click(sender As Object, e As EventArgs) Handles BConfirmarVenta.Click
        If Not ValidarVentaCompleta() Then Exit Sub

        ' Confirmación final antes de procesar
        Dim confirmacion As DialogResult = MessageBox.Show(
            $"¿Confirmar venta por un total de {totalVenta.ToString("C2")}?" & vbCrLf &
            $"Cliente: {LNombreClienteConf.Text}" & vbCrLf &
            $"Productos: {detalle.Rows.Count}",
            "Confirmar Venta",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question)

        If confirmacion <> DialogResult.Yes Then
            Return
        End If

        Try
            ' Guardar venta en la base de datos
            If GuardarVentaEnBD() Then
                MessageBox.Show("Venta registrada correctamente.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                ResetearVenta()
            Else
                MessageBox.Show("Error al registrar la venta.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If

        Catch ex As Exception
            MessageBox.Show($"Error al registrar la venta: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GuardarVentaEnBD() As Boolean
        Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

        Using cn As New SqlConnection(ConnStr)
            cn.Open()
            Dim transaction = cn.BeginTransaction()

            Try
                ' 1. Insertar cabecera de venta
                Dim cmdVenta As New SqlCommand(
                    "INSERT INTO Venta (dni_cliente, nombre_usuario, total_venta) " &
                    "OUTPUT INSERTED.num_venta " &
                    "VALUES (@dni_cliente, @nombre_usuario, @total_venta)", cn, transaction)

                cmdVenta.Parameters.AddWithValue("@dni_cliente", TBDNICliente.Text.Trim())
                cmdVenta.Parameters.AddWithValue("@nombre_usuario", usuarioActual)
                cmdVenta.Parameters.AddWithValue("@total_venta", totalVenta)

                Dim numVenta = Convert.ToInt32(cmdVenta.ExecuteScalar())

                ' 2. Insertar detalles y actualizar stock
                For Each row As DataRow In detalle.Rows
                    If row.RowState <> DataRowState.Deleted Then
                        ' Obtener ID del producto
                        Dim cmdProducto As New SqlCommand("SELECT id_producto FROM Producto WHERE codigo_barras = @codigo", cn, transaction)
                        cmdProducto.Parameters.AddWithValue("@codigo", row("CodigoBarra").ToString())
                        Dim idProducto = Convert.ToInt32(cmdProducto.ExecuteScalar())

                        ' Insertar detalle
                        Dim cmdDetalle As New SqlCommand(
                            "INSERT INTO DetalleVenta (num_venta, id_producto, cantidad, subtotal) " &
                            "VALUES (@num_venta, @id_producto, @cantidad, @subtotal)", cn, transaction)

                        cmdDetalle.Parameters.AddWithValue("@num_venta", numVenta)
                        cmdDetalle.Parameters.AddWithValue("@id_producto", idProducto)
                        cmdDetalle.Parameters.AddWithValue("@cantidad", Convert.ToInt32(row("Cantidad")))
                        cmdDetalle.Parameters.AddWithValue("@subtotal", Convert.ToDecimal(row("Subtotal")))

                        cmdDetalle.ExecuteNonQuery()

                        ' Actualizar stock
                        Dim cmdStock As New SqlCommand(
                            "UPDATE Producto SET stock = stock - @cantidad WHERE id_producto = @id_producto", cn, transaction)

                        cmdStock.Parameters.AddWithValue("@cantidad", Convert.ToInt32(row("Cantidad")))
                        cmdStock.Parameters.AddWithValue("@id_producto", idProducto)
                        cmdStock.ExecuteNonQuery()
                    End If
                Next

                transaction.Commit()
                Return True

            Catch ex As Exception
                transaction.Rollback()
                Throw
            End Try
        End Using
    End Function

    ' ===========================================
    ' ============== CANCELAR ===================
    ' ===========================================
    Private Sub BCancelarVenta_Click(sender As Object, e As EventArgs) Handles BCancelar.Click
        If detalle.Rows.Count > 0 Then
            Dim resp = MessageBox.Show("¿Está seguro que desea cancelar la venta? Se perderán todos los productos agregados.",
                                       "Cancelar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If resp = DialogResult.Yes Then
                ResetearVenta()
            End If
        Else
            ResetearVenta()
        End If
    End Sub

    Private Sub ResetearVenta()
        detalle.Clear()
        totalVenta = 0D
        LTotal.Text = totalVenta.ToString("C2")
        TBDNICliente.Clear()
        LNombreClienteConf.Text = ""
        LimpiarCamposProducto()
        TBDNICliente.Focus()
    End Sub

    ' ===========================================
    ' ======== BOTÓN CREAR CLIENTE (UI) =========
    ' ===========================================
    Private Sub BCrearCliente_Click(sender As Object, e As EventArgs) Handles BCrearCliente.Click
        Using frm As New FormClientes()
            frm.CerrarAlAgregar = True
            If Not String.IsNullOrWhiteSpace(TBDNICliente.Text) Then
                frm.Prefill(TBDNICliente.Text.Trim())
            End If
            frm.StartPosition = FormStartPosition.CenterParent

            If frm.ShowDialog(Me) = DialogResult.OK AndAlso frm.ClienteAgregado IsNot Nothing Then
                Dim c As DataRow = frm.ClienteAgregado
                TBDNICliente.Text = c("DNI").ToString()
                LNombreClienteConf.Text = c("Nombre").ToString()
            End If
        End Using
    End Sub

    ' ===========================================
    ' ======== VALIDACIÓN AL CERRAR =============
    ' ===========================================
    Private Sub FormVentas_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If detalle.Rows.Count > 0 AndAlso totalVenta > 0 Then
            Dim respuesta As DialogResult = MessageBox.Show(
                "Tiene una venta en proceso. ¿Está seguro que desea salir?",
                "Venta en Proceso",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning)

            If respuesta = DialogResult.No Then
                e.Cancel = True
            End If
        End If
    End Sub

End Class


