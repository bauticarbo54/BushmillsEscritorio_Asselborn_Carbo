Imports System.Data.SqlClient

Public Class FormProductos

    Private productos As DataTable
    Private codigoBarraOriginal As String = "" ' Variable para guardar el código original al editar

    Private Sub FormProductos_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Gestión de Productos y Stock"
        Me.WindowState = FormWindowState.Normal

        ' Configurar validación de campos
        ConfigurarValidaciones()


        CargarProductosDesdeBD()
        FormatearDataGridView()

        CargarMarcas()
        CargarCategorias()


        ' Formato de columnas
        FormatearDataGridView()

        ' Datos de prueba para combos
        'If CBCategoria.Items.Count = 0 Then
        '    CBCategoria.Items.AddRange({"Vino", "Cerveza", "Whisky", "Vodka", "Gin", "Ron", "Fernet", "Aperitivo"})
        'End If
        'If CBMarca.Items.Count = 0 Then
        '    CBMarca.Items.AddRange({"Luigi Bosca", "Quilmes", "Johnnie Walker", "Absolut", "Bacardi", "Tanqueray", "Branca", "Campari"})
        'End If

        ' Configurar controles
        ConfigurarControles()




    End Sub

    Private Sub CargarProductosDesdeBD()
        Try
            AbrirConexion()
            Dim query As String = "SELECT * FROM Producto"
            Dim adaptador As New SqlDataAdapter(query, Conex)
            Dim dt As New DataTable()
            adaptador.Fill(dt)
            productos = dt
            DGVProductos.DataSource = productos
            CerrarConexion()
        Catch ex As Exception
            MessageBox.Show("Error al cargar productos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarMarcas()
        Try
            Conex.Open()
            Dim cmd As New SqlCommand("SELECT id_marca, nombre FROM Marca", Conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            Dim dt As New DataTable()
            dt.Load(reader)

            CBMarca.DataSource = dt
            CBMarca.DisplayMember = "nombre"   ' Lo que se muestra
            CBMarca.ValueMember = "id_marca"         ' Valor interno

            Conex.Close()
        Catch ex As Exception
            MessageBox.Show("Error al cargar marcas: " & ex.Message)
        Finally
            If Conex.State = ConnectionState.Open Then Conex.Close()
        End Try
    End Sub

    Private Sub CargarCategorias()
        Try
            Conex.Open()
            Dim cmd As New SqlCommand("SELECT id_categoria, nombre FROM Categoria", Conex)
            Dim reader As SqlDataReader = cmd.ExecuteReader()

            Dim dt As New DataTable()
            dt.Load(reader)

            CBCategoria.DataSource = dt
            CBCategoria.DisplayMember = "nombre"
            CBCategoria.ValueMember = "id_categoria"

            Conex.Close()
        Catch ex As Exception
            MessageBox.Show("Error al cargar categorías: " & ex.Message)
        Finally
            If Conex.State = ConnectionState.Open Then Conex.Close()
        End Try
    End Sub

    Private Sub ConfigurarValidaciones()
        ' Configurar máximo de caracteres
        TBCodBarra.MaxLength = 20
        TBVolumen.MaxLength = 6 ' Máximo 999999 cm³
        TBGraduacion.MaxLength = 10
        TBProveedor.MaxLength = 100

        ' Configurar NUDs
        NUDPrecio.DecimalPlaces = 2
        NUDPrecio.Minimum = 0
        NUDPrecio.Maximum = 100000
        NUDPrecio.Increment = 0.5D

        NUDStock.DecimalPlaces = 0
        NUDStock.Minimum = 0
        NUDStock.Maximum = 10000
        NUDStock.Increment = 1
        NUDStock.Value = 0
    End Sub

    Private Sub ConfigurarControles()
        ' ToolTips para ayudar al usuario
        Dim toolTip As New ToolTip()
        toolTip.SetToolTip(TBCodBarra, "Ingrese código de barras numérico (máx. 20 dígitos)")
        toolTip.SetToolTip(TBVolumen, "Volumen en centímetros cúbicos (solo números, ej: 750, 1000)")
        toolTip.SetToolTip(TBGraduacion, "Ejemplo: 12.5%, 40% vol")
        toolTip.SetToolTip(TBProveedor, "Nombre del proveedor (máx. 100 caracteres)")
        toolTip.SetToolTip(NUDStock, "Cantidad en stock (0-10000 unidades)")
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
        productos.Columns.Add("Stock", GetType(Integer))
        productos.Columns.Add("Estado", GetType(String))
    End Sub

    Private Sub FormatearDataGridView()
        ' Formato del precio como moneda
        If DGVProductos.Columns.Contains("Precio") Then
            DGVProductos.Columns("Precio").DefaultCellStyle.Format = "C2"
            DGVProductos.Columns("Precio").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        End If

        ' Formato del stock como número entero
        If DGVProductos.Columns.Contains("Stock") Then
            DGVProductos.Columns("Stock").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            DGVProductos.Columns("Stock").DefaultCellStyle.Format = "N0"
        End If

        ' Autoajustar columnas
        DGVProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVProductos.ReadOnly = True
        DGVProductos.AllowUserToAddRows = False
        DGVProductos.RowHeadersVisible = False
    End Sub

    ' --- VALIDACIONES INDIVIDUALES ---
    Private Function ValidarCodigoBarra() As Boolean
        Dim codigo As String = TBCodBarra.Text.Trim()

        If String.IsNullOrWhiteSpace(codigo) Then
            MessageBox.Show("El código de barras es obligatorio.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            Return False
        End If

        If codigo.Length < 3 Then
            MessageBox.Show("El código de barras debe tener al menos 3 dígitos.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            TBCodBarra.SelectAll()
            Return False
        End If

        ' Validar que sea solo numérico
        If Not System.Text.RegularExpressions.Regex.IsMatch(codigo, "^\d+$") Then
            MessageBox.Show("El código de barras solo puede contener números.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            TBCodBarra.SelectAll()
            Return False
        End If

        Return True
    End Function

    Private Function ValidarCategoria() As Boolean
        If CBCategoria.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una categoría.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CBCategoria.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function ValidarMarca() As Boolean
        If CBMarca.SelectedIndex = -1 Then
            MessageBox.Show("Debe seleccionar una marca.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CBMarca.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function ValidarPrecio() As Boolean
        If NUDPrecio.Value <= 0 Then
            MessageBox.Show("El precio debe ser mayor a 0.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NUDPrecio.Focus()
            Return False
        End If

        If NUDPrecio.Value > 100000 Then
            MessageBox.Show("El precio no puede ser mayor a $100,000.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NUDPrecio.Focus()
            NUDPrecio.Select(0, NUDPrecio.Text.Length)
            Return False
        End If

        Return True
    End Function

    Private Function ValidarVolumen() As Boolean
        Dim volumen As String = TBVolumen.Text.Trim()

        If String.IsNullOrWhiteSpace(volumen) Then
            MessageBox.Show("El volumen es obligatorio.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBVolumen.Focus()
            Return False
        End If

        ' Validar que sea solo numérico
        If Not System.Text.RegularExpressions.Regex.IsMatch(volumen, "^\d+$") Then
            MessageBox.Show("El volumen debe contener solo números enteros (cm³).", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBVolumen.Focus()
            TBVolumen.SelectAll()
            Return False
        End If

        ' Validar rango numérico
        Dim volumenNum As Integer
        If Integer.TryParse(volumen, volumenNum) Then
            If volumenNum <= 0 Then
                MessageBox.Show("El volumen debe ser mayor a 0 cm³.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBVolumen.Focus()
                TBVolumen.SelectAll()
                Return False
            End If

            If volumenNum > 999999 Then
                MessageBox.Show("El volumen no puede ser mayor a 999,999 cm³.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBVolumen.Focus()
                TBVolumen.SelectAll()
                Return False
            End If
        Else
            MessageBox.Show("El volumen debe ser un número válido.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBVolumen.Focus()
            TBVolumen.SelectAll()
            Return False
        End If

        Return True
    End Function

    Private Function ValidarGraduacion() As Boolean
        Dim graduacion As String = TBGraduacion.Text.Trim()

        If String.IsNullOrWhiteSpace(graduacion) Then
            MessageBox.Show("La graduación alcohólica es obligatoria.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBGraduacion.Focus()
            Return False
        End If

        ' Validar formato de graduación (debe contener número y %)
        If Not graduacion.Contains("%") Then
            MessageBox.Show("La graduación debe incluir el símbolo % (ej: 12.5%, 40% vol).", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBGraduacion.Focus()
            TBGraduacion.SelectAll()
            Return False
        End If

        Return True
    End Function

    Private Function ValidarProveedor() As Boolean
        Dim proveedor As String = TBProveedor.Text.Trim()

        If String.IsNullOrWhiteSpace(proveedor) Then
            MessageBox.Show("El proveedor es obligatorio.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBProveedor.Focus()
            Return False
        End If

        If proveedor.Length < 3 Then
            MessageBox.Show("El nombre del proveedor debe tener al menos 3 caracteres.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBProveedor.Focus()
            TBProveedor.SelectAll()
            Return False
        End If

        ' Validar que solo contenga letras, números y espacios
        For Each c As Char In proveedor
            If Not Char.IsLetterOrDigit(c) AndAlso c <> " "c AndAlso c <> "."c Then
                MessageBox.Show("El nombre del proveedor solo puede contener letras, números, espacios y puntos.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBProveedor.Focus()
                TBProveedor.SelectAll()
                Return False
            End If
        Next

        Return True
    End Function

    Private Function ValidarStock() As Boolean
        If NUDStock.Value < 0 Then
            MessageBox.Show("El stock no puede ser negativo.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NUDStock.Focus()
            Return False
        End If

        If NUDStock.Value > 10000 Then
            MessageBox.Show("El stock no puede ser mayor a 10,000 unidades.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NUDStock.Focus()
            Return False
        End If

        Return True
    End Function

    ' --- VALIDACIÓN COMPLETA PARA AGREGAR ---
    Private Function ValidarCamposParaAgregar() As Boolean
        If Not ValidarCodigoBarra() Then Return False

        ' Verificar si ya existe (solo para agregar)
        Dim codigo As String = TBCodBarra.Text.Trim()
        Dim encontrado = productos.Select($"codigo_barras = '{codigo.Replace("'", "''")}'")
        If encontrado.Length > 0 Then
            MessageBox.Show("Este código de barras ya está registrado.", "Validación",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            TBCodBarra.SelectAll()
            Return False
        End If

        Return ValidarCategoria() AndAlso
               ValidarMarca() AndAlso
               ValidarPrecio() AndAlso
               ValidarVolumen() AndAlso
               ValidarGraduacion() AndAlso
               ValidarProveedor() AndAlso
               ValidarStock()
    End Function

    ' --- VALIDACIÓN COMPLETA PARA EDITAR ---
    Private Function ValidarCamposParaEditar() As Boolean
        If Not ValidarCodigoBarra() Then Return False

        ' Validación especial para código de barras en edición
        Dim codigo As String = TBCodBarra.Text.Trim()
        If codigo <> codigoBarraOriginal Then
            ' Si cambió el código, verificar que no exista otro
            Dim encontrado = productos.Select($"CodigoBarra = '{codigo.Replace("'", "''")}' AND CodigoBarra <> '{codigoBarraOriginal.Replace("'", "''")}'")
            If encontrado.Length > 0 Then
                MessageBox.Show("Este código de barras ya está registrado en otro producto.", "Validación",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBCodBarra.Focus()
                TBCodBarra.SelectAll()
                Return False
            End If
        End If

        Return ValidarCategoria() AndAlso
               ValidarMarca() AndAlso
               ValidarPrecio() AndAlso
               ValidarVolumen() AndAlso
               ValidarGraduacion() AndAlso
               ValidarProveedor() AndAlso
               ValidarStock()
    End Function

    ' --- VALIDACIÓN EN TIEMPO REAL ---
    Private Sub TBCodBarra_TextChanged(sender As Object, e As EventArgs) Handles TBCodBarra.TextChanged
        ' Limitar a caracteres numéricos
        Dim texto As String = TBCodBarra.Text
        If texto.Length > 0 AndAlso Not System.Text.RegularExpressions.Regex.IsMatch(texto, "^\d*$") Then
            ' Mantener solo los caracteres numéricos
            Dim soloNumeros As String = System.Text.RegularExpressions.Regex.Replace(texto, "[^\d]", "")
            TBCodBarra.Text = soloNumeros
            TBCodBarra.SelectionStart = TBCodBarra.Text.Length
        End If
    End Sub

    Private Sub TBVolumen_TextChanged(sender As Object, e As EventArgs) Handles TBVolumen.TextChanged
        ' Limitar a caracteres numéricos
        Dim texto As String = TBVolumen.Text
        If texto.Length > 0 AndAlso Not System.Text.RegularExpressions.Regex.IsMatch(texto, "^\d*$") Then
            ' Mantener solo los caracteres numéricos
            Dim soloNumeros As String = System.Text.RegularExpressions.Regex.Replace(texto, "[^\d]", "")
            TBVolumen.Text = soloNumeros
            TBVolumen.SelectionStart = TBVolumen.Text.Length
        End If
    End Sub

    Private Sub TBGraduacion_TextChanged(sender As Object, e As EventArgs) Handles TBGraduacion.TextChanged
        ' Sugerir formato de graduación
        If TBGraduacion.Text.Trim().Length > 0 AndAlso Not TBGraduacion.Text.Contains("%") Then
            TBGraduacion.ForeColor = Color.Red
        Else
            TBGraduacion.ForeColor = Color.Black
        End If
    End Sub

    ' --- ESCANEO O INGRESO MANUAL DEL CÓDIGO ---
    Private Sub TBCodBarra_KeyDown(sender As Object, e As KeyEventArgs) Handles TBCodBarra.KeyDown
        If e.KeyCode = Keys.Enter Then
            e.SuppressKeyPress = True
            If ValidarCodigoBarra() Then
                CBCategoria.Focus()
            End If
        End If
    End Sub

    ' --- BOTÓN AGREGAR ---
    Private Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click
        If Not ValidarCamposParaAgregar() Then
            Exit Sub
        End If

        Try
            AbrirConexion()
            Dim query As String = "INSERT INTO Producto (codigo_barras, id_categoria, id_marca, precio, volumen, graduacion, proveedor, stock, estado)
                           VALUES (@codigo, @categoria, @marca, @precio, @volumen, @graduacion, @proveedor, @stock, 'Activo')"
            Dim cmd As New SqlCommand(query, Conex)
            cmd.Parameters.AddWithValue("@codigo", TBCodBarra.Text.Trim())
            cmd.Parameters.AddWithValue("@categoria", CBCategoria.Text)
            cmd.Parameters.AddWithValue("@marca", CBMarca.Text)
            cmd.Parameters.AddWithValue("@precio", NUDPrecio.Value)
            cmd.Parameters.AddWithValue("@volumen", TBVolumen.Text.Trim() & " cm³")
            cmd.Parameters.AddWithValue("@graduacion", TBGraduacion.Text.Trim())
            cmd.Parameters.AddWithValue("@proveedor", TBProveedor.Text.Trim())
            cmd.Parameters.AddWithValue("@stock", CInt(NUDStock.Value))

            cmd.ExecuteNonQuery()
            CerrarConexion()

            MessageBox.Show("Producto agregado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

            CargarProductosDesdeBD()
            LimpiarCampos()

        Catch ex As Exception
            MessageBox.Show("Error al agregar producto: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- BOTÓN EDITAR ---
    Private Sub BEditar_Click(sender As Object, e As EventArgs) Handles BEditar.Click
        If DGVProductos.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un producto para editar.", "Atención",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Not ValidarCamposParaEditar() Then
            Exit Sub
        End If

        Try
            AbrirConexion()
            Dim query As String = "UPDATE Producto SET categoria=@categoria, marca=@marca, precio=@precio, volumen=@volumen,
                           graduacion=@graduacion, proveedor=@proveedor, stock=@stock WHERE codigo_barra=@codigo"
            Dim cmd As New SqlCommand(query, Conex)
            cmd.Parameters.AddWithValue("@codigo", TBCodBarra.Text.Trim())
            cmd.Parameters.AddWithValue("@categoria", CBCategoria.Text)
            cmd.Parameters.AddWithValue("@marca", CBMarca.Text)
            cmd.Parameters.AddWithValue("@precio", NUDPrecio.Value)
            cmd.Parameters.AddWithValue("@volumen", TBVolumen.Text.Trim() & " cm³")
            cmd.Parameters.AddWithValue("@graduacion", TBGraduacion.Text.Trim())
            cmd.Parameters.AddWithValue("@proveedor", TBProveedor.Text.Trim())
            cmd.Parameters.AddWithValue("@stock", CInt(NUDStock.Value))
            cmd.ExecuteNonQuery()
            CerrarConexion()

            MessageBox.Show("Producto actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

            CargarProductosDesdeBD()
            LimpiarCampos()

        Catch ex As Exception
            MessageBox.Show("Error al actualizar producto: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- BOTÓN SUSPENDER/REACTIVAR ---
    Private Sub BSuspender_Click(sender As Object, e As EventArgs) Handles BSuspender.Click
        If DGVProductos.SelectedRows.Count = 0 Then
            MessageBox.Show("Seleccione un producto primero.", "Atención",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Dim row As DataGridViewRow = DGVProductos.SelectedRows(0)
            Dim codigo As String = row.Cells("codigo_barra").Value.ToString()
            Dim estadoActual As String = row.Cells("estado").Value.ToString()

            Dim nuevoEstado As String = If(estadoActual = "Activo", "Inactivo", "Activo")

            AbrirConexion()
            Dim query As String = "UPDATE Producto SET estado=@estado WHERE codigo_barra=@codigo"
            Dim cmd As New SqlCommand(query, Conex)
            cmd.Parameters.AddWithValue("@estado", nuevoEstado)
            cmd.Parameters.AddWithValue("@codigo", codigo)
            cmd.ExecuteNonQuery()
            CerrarConexion()

            MessageBox.Show($"El producto fue actualizado a '{nuevoEstado}'.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)

            CargarProductosDesdeBD()

        Catch ex As Exception
            MessageBox.Show($"Error al cambiar estado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- CARGAR DATOS AL SELECCIONAR FILA ---
    Private Sub DGVProductos_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVProductos.CellClick
        If e.RowIndex >= 0 Then
            Try
                Dim row As DataGridViewRow = DGVProductos.Rows(e.RowIndex)

                ' Guardar el código original para edición
                codigoBarraOriginal = row.Cells("CodigoBarra").Value.ToString()

                TBCodBarra.Text = codigoBarraOriginal
                CBCategoria.Text = row.Cells("Categoria").Value.ToString()
                CBMarca.Text = row.Cells("Marca").Value.ToString()
                NUDPrecio.Value = Convert.ToDecimal(row.Cells("Precio").Value)

                ' Quitar " cm³" del volumen al cargar para edición
                Dim volumenTexto As String = row.Cells("Volumen").Value.ToString()
                If volumenTexto.EndsWith(" cm³") Then
                    TBVolumen.Text = volumenTexto.Replace(" cm³", "")
                Else
                    TBVolumen.Text = volumenTexto
                End If

                TBGraduacion.Text = row.Cells("Graduacion").Value.ToString()
                TBProveedor.Text = row.Cells("Proveedor").Value.ToString()
                NUDStock.Value = Convert.ToDecimal(row.Cells("Stock").Value)

                ' Actualizar texto del botón según estado
                Dim estadoActual As String = row.Cells("Estado").Value.ToString()
                ActualizarBotonSuspender(estadoActual)

            Catch ex As Exception
                MessageBox.Show($"Error al cargar datos del producto: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' --- FORMATEAR FILAS SEGÚN ESTADO Y STOCK ---
    Private Sub DGVProductos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGVProductos.CellFormatting
        If e.RowIndex >= 0 AndAlso e.Value IsNot Nothing Then
            Dim row As DataGridViewRow = DGVProductos.Rows(e.RowIndex)

            ' Formato por estado
            If DGVProductos.Columns(e.ColumnIndex).Name = "Estado" Then
                If e.Value.ToString() = "Inactivo" Then
                    row.DefaultCellStyle.ForeColor = Color.Gray
                    row.DefaultCellStyle.BackColor = Color.LightYellow
                Else
                    row.DefaultCellStyle.ForeColor = Color.Black
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            End If

            ' Formato por stock bajo
            If DGVProductos.Columns(e.ColumnIndex).Name = "Stock" Then
                Dim stock As Integer
                If Integer.TryParse(e.Value.ToString(), stock) Then
                    If stock = 0 Then
                        e.CellStyle.BackColor = Color.LightCoral
                        e.CellStyle.ForeColor = Color.DarkRed
                    ElseIf stock < 10 Then
                        e.CellStyle.BackColor = Color.LightYellow
                        e.CellStyle.ForeColor = Color.OrangeRed
                    End If
                End If
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
        NUDStock.Value = 0
        codigoBarraOriginal = "" ' Limpiar también el código original
        TBCodBarra.Focus()
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

    Private Sub CBMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBMarca.SelectedIndexChanged

    End Sub

    Private Sub CBCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBCategoria.SelectedIndexChanged

    End Sub
End Class