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

        ' Total inicial: solo el importe (la label "Total:" déjala fija aparte)
        LTotal.Text = totalVenta.ToString("C2")

        ' Foco
        TBDNICliente.Focus()
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
        NUDCantidad.Value = 0
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
    Private Function ValidarProducto() As Boolean
        If String.IsNullOrWhiteSpace(TBCodBarra.Text) Then
            MessageBox.Show("Ingrese el código de barra.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            Return False
        End If

        If NUDCantidad.Value < 0 Then
            MessageBox.Show("La cantidad no puede ser negativa.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NUDCantidad.Focus()
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

        ' Simulación de búsqueda de producto (luego vendrá de BD)
        Dim producto As String = "Cerveza"
        Dim marca As String = "Quilmes"
        Dim tipo As String = "Lager"
        Dim precio As Decimal = 120D

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
        If Not ValidarYResolverCliente() Then Exit Sub

        If detalle.Rows.Count = 0 Then
            MessageBox.Show("Agregue al menos un producto.", "Validación",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            Exit Sub
        End If

        ' Aquí iría el guardado real (try/catch + persistencia)
        MessageBox.Show("Venta registrada correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information)

        ' Reset para nueva venta
        ResetearVenta()
    End Sub

    ' ===========================================
    ' ============== CANCELAR ===================
    ' ===========================================
    Private Sub BCancelarVenta_Click(sender As Object, e As EventArgs) Handles BCancelar.Click
        Dim resp = MessageBox.Show("¿Está seguro que desea cancelar la venta?",
                                   "Cancelar Venta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If resp = DialogResult.Yes Then
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

End Class


