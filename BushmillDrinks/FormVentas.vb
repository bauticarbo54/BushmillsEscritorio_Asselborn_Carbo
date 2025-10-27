Public Class FormVentas

    Private detalle As DataTable
    Private totalVenta As Decimal = 0D

    ' Índice de fila seleccionada en el DGV (-1 = ninguna)
    Private selectedRowIndex As Integer = -1

    ' =============================
    ' ============ LOAD ===========
    ' =============================
    Private Sub FormVentas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Registro de Ventas"
        Me.WindowState = FormWindowState.Normal

        ' Simulación de vendedor (luego vendrá del login)
        LVendedorID.Text = "6"
        LNombreVend.Text = "Santiago"

        ' Tabla del detalle
        PrepararTabla()
        DGVDetalleVenta.DataSource = detalle
        DGVDetalleVenta.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVDetalleVenta.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVDetalleVenta.MultiSelect = False

        ' Configurar validación de entrada
        ConfigureInputValidation()

        ' Total inicial: solo el importe (la label "Total:" déjala fija aparte)
        LTotal.Text = totalVenta.ToString("C2")

        ' Foco
        TBDNICliente.Focus()
    End Sub

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
        detalle.Columns.Add("Tipo", GetType(String))
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
        If dr IsNot Nothing AndAlso dr("Estado").ToString() = "Activo" Then
            LNombreClienteConf.Text = dr("Nombre").ToString()
            Return True
        End If

        ' No existe o está inactivo -> proponer alta
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
        ' Limitar longitud del código de barras (ejemplo: 13 dígitos EAN-13)
        If TBCodBarra.Text.Length > 13 Then
            TBCodBarra.Text = TBCodBarra.Text.Substring(0, 13)
            TBCodBarra.SelectionStart = TBCodBarra.Text.Length
            MessageBox.Show("El código de barras no puede tener más de 13 dígitos.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- VALIDACIÓN DE CANTIDAD MÁXIMA ---
    Private Sub NUDCantidad_ValueChanged(sender As Object, e As EventArgs)
        ' Establecer un límite máximo razonable para la cantidad
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
        ' Simulación - luego esto vendrá de tu base de datos
        Dim productosExistentes As New List(Of String) From {"1234567890123", "123", "456", "789", "111", "222"}

        If Not productosExistentes.Contains(codigoBarra) Then
            MessageBox.Show("El código de barras no corresponde a un producto existente.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
    End Function

    ' --- VALIDACIÓN DE STOCK DISPONIBLE ---
    Private Function ValidarStockDisponible(codigoBarra As String, cantidadSolicitada As Integer) As Boolean
        ' Simulación de validación de stock - luego vendrá de BD
        Dim stockDisponible As Integer = 0

        Select Case codigoBarra
            Case "1234567890123", "123"
                stockDisponible = 50
            Case "456"
                stockDisponible = 25
            Case "789"
                stockDisponible = 100
            Case "111"
                stockDisponible = 10
            Case "222"
                stockDisponible = 5
            Case Else
                stockDisponible = 0
        End Select

        If cantidadSolicitada > stockDisponible Then
            MessageBox.Show($"Stock insuficiente. Disponible: {stockDisponible} unidades", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Return True
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

    ' --- VALIDACIÓN DE PRECIO EN SIMULACIÓN ---
    Private Function ObtenerPrecioProducto(codigoBarra As String) As Decimal
        ' Simulación de precios - luego vendrá de BD
        Dim precios As New Dictionary(Of String, Decimal) From {
            {"1234567890123", 120D},
            {"123", 150D},
            {"456", 200D},
            {"789", 75.5D},
            {"111", 300D},
            {"222", 89.9D}
        }

        If precios.ContainsKey(codigoBarra) Then
            Return precios(codigoBarra)
        Else
            MessageBox.Show("No se pudo obtener el precio para este producto.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return 0D
        End If
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

        ' Obtener datos del producto (simulación - luego vendrá de BD)
        Dim producto As String = ""
        Dim marca As String = ""
        Dim tipo As String = ""

        Select Case TBCodBarra.Text.Trim()
            Case "1234567890123", "123"
                producto = "Cerveza"
                marca = "Quilmes"
                tipo = "Lager"
            Case "456"
                producto = "Vino"
                marca = "Trapiche"
                tipo = "Malbec"
            Case "789"
                producto = "Gaseosa"
                marca = "Coca-Cola"
                tipo = "Cola"
            Case "111"
                producto = "Whisky"
                marca = "Johnnie Walker"
                tipo = "Red Label"
            Case "222"
                producto = "Agua Mineral"
                marca = "Villavicencio"
                tipo = "Sin Gas"
        End Select

        Dim precio As Decimal = ObtenerPrecioProducto(TBCodBarra.Text.Trim())
        If precio <= 0 Then Exit Sub

        Dim cod As String = TBCodBarra.Text.Trim()
        Dim cant As Integer = Convert.ToInt32(NUDCantidad.Value)

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
            detalle.Rows.Add(cod, producto, marca, tipo, cant, precio, subtotal)
        End If

        RecalcularTotal()
        LimpiarCamposProducto()
    End Sub

    ' ===========================================
    ' ============ CONFIRMAR VENTA ==============
    ' ===========================================
    Private Sub BConfirmarVenta_Click(sender As Object, e As EventArgs) Handles BConfirmarVenta.Click
        ' Usar validación completa en lugar de solo validar cliente
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

        ' Aquí iría el guardado real (try/catch + persistencia)
        Try
            ' Simulación de guardado
            MessageBox.Show("Venta registrada correctamente.", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Reset para nueva venta
            ResetearVenta()

        Catch ex As Exception
            MessageBox.Show($"Error al registrar la venta: {ex.Message}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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


