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

        ' Configurar controles
        ConfigurarControles()

        ' Configurar AutoComplete para ComboBox
        ConfigurarAutoComplete()

        TBCodBarra.Focus()
    End Sub

    Private Sub ConfigurarAutoComplete()
        ' Configurar AutoComplete para Marca
        CBMarca.DropDownStyle = ComboBoxStyle.DropDown
        CBMarca.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        CBMarca.AutoCompleteSource = AutoCompleteSource.ListItems

        ' Configurar AutoComplete para Categoría
        CBCategoria.DropDownStyle = ComboBoxStyle.DropDown
        CBCategoria.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        CBCategoria.AutoCompleteSource = AutoCompleteSource.ListItems
    End Sub

    Private Sub CargarProductosDesdeBD()
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim query As String = "SELECT p.codigo_barras, p.nombre, c.nombre as categoria, m.nombre as marca, p.precio, " &
                                      "p.volumen, p.graduacion, p.proveedor, p.stock, p.estado " &
                                      "FROM Producto p " &
                                      "INNER JOIN Categoria c ON p.id_categoria = c.id_categoria " &
                                      "INNER JOIN Marca m ON p.id_marca = m.id_marca " &
                                      "ORDER BY p.codigo_barras"
                Dim adaptador As New SqlDataAdapter(query, cn)
                productos = New DataTable()
                adaptador.Fill(productos)
                DGVProductos.DataSource = productos
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar productos: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            PrepararTabla()
        End Try
    End Sub

    Private Sub CargarMarcas()
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim cmd As New SqlCommand("SELECT id_marca, nombre FROM Marca ORDER BY nombre", cn)
                cn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                Dim dt As New DataTable()
                dt.Load(reader)

                CBMarca.DataSource = dt
                CBMarca.DisplayMember = "nombre"
                CBMarca.ValueMember = "id_marca"
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar marcas: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CargarCategorias()
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim cmd As New SqlCommand("SELECT id_categoria, nombre FROM Categoria ORDER BY nombre", cn)
                cn.Open()
                Dim reader As SqlDataReader = cmd.ExecuteReader()

                Dim dt As New DataTable()
                dt.Load(reader)

                CBCategoria.DataSource = dt
                CBCategoria.DisplayMember = "nombre"
                CBCategoria.ValueMember = "id_categoria"
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al cargar categorías: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigurarValidaciones()
        ' Configurar máximo de caracteres
        TBCodBarra.MaxLength = 50
        TBNombre.MaxLength = 100 ' Nuevo campo nombre
        TBVolumen.MaxLength = 10
        TBGraduacion.MaxLength = 20
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
        toolTip.SetToolTip(TBCodBarra, "Ingrese código de barras")
        toolTip.SetToolTip(TBNombre, "Nombre específico de la bebida (ej: Stout, Malbec, Red Label)") ' Nuevo tooltip
        toolTip.SetToolTip(TBVolumen, "Volumen en litros (ej: 0.75, 1.00)")
        toolTip.SetToolTip(TBGraduacion, "Ejemplo: 12.5, 40.0 (sin símbolo %)")
        toolTip.SetToolTip(TBProveedor, "Nombre del proveedor")
        toolTip.SetToolTip(NUDStock, "Cantidad en stock (0-10000 unidades)")

        ' ToolTips para ComboBox con AutoComplete
        toolTip.SetToolTip(CBMarca, "Seleccione una marca existente o escriba una nueva")
        toolTip.SetToolTip(CBCategoria, "Seleccione una categoría existente o escriba una nueva")
    End Sub

    Private Sub PrepararTabla()
        productos = New DataTable()
        productos.Columns.Add("CodigoBarra", GetType(String))
        productos.Columns.Add("Nombre", GetType(String)) ' Nueva columna
        productos.Columns.Add("Categoria", GetType(String))
        productos.Columns.Add("Marca", GetType(String))
        productos.Columns.Add("Precio", GetType(Decimal))
        productos.Columns.Add("Volumen", GetType(String))
        productos.Columns.Add("Graduacion", GetType(String))
        productos.Columns.Add("Proveedor", GetType(String))
        productos.Columns.Add("Stock", GetType(Integer))
        productos.Columns.Add("Estado", GetType(String))
        DGVProductos.DataSource = productos
    End Sub

    Private Sub FormatearDataGridView()
        ' Autoajustar columnas
        DGVProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DGVProductos.ReadOnly = True
        DGVProductos.AllowUserToAddRows = False
        DGVProductos.RowHeadersVisible = False

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
    End Sub

    ' --- FUNCIONES PARA MANEJAR MARCAS Y CATEGORÍAS NUEVAS ---
    Private Function ObtenerOInsertarMarca(nombreMarca As String) As Integer
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                ' Primero verificar si existe
                Dim cmdVerificar As New SqlCommand("SELECT id_marca FROM Marca WHERE nombre = @nombre", cn)
                cmdVerificar.Parameters.AddWithValue("@nombre", nombreMarca.Trim())
                cn.Open()

                Dim resultado = cmdVerificar.ExecuteScalar()
                If resultado IsNot Nothing Then
                    Return Convert.ToInt32(resultado)
                End If

                ' Si no existe, insertar nueva marca
                Dim cmdInsertar As New SqlCommand("INSERT INTO Marca (nombre) OUTPUT INSERTED.id_marca VALUES (@nombre)", cn)
                cmdInsertar.Parameters.AddWithValue("@nombre", nombreMarca.Trim())
                Dim nuevoId = Convert.ToInt32(cmdInsertar.ExecuteScalar())

                ' Recargar combo de marcas
                CargarMarcas()
                CBMarca.Text = nombreMarca.Trim()

                Return nuevoId
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al gestionar marca: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return -1
        End Try
    End Function

    Private Function ObtenerOInsertarCategoria(nombreCategoria As String) As Integer
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                ' Primero verificar si existe
                Dim cmdVerificar As New SqlCommand("SELECT id_categoria FROM Categoria WHERE nombre = @nombre", cn)
                cmdVerificar.Parameters.AddWithValue("@nombre", nombreCategoria.Trim())
                cn.Open()

                Dim resultado = cmdVerificar.ExecuteScalar()
                If resultado IsNot Nothing Then
                    Return Convert.ToInt32(resultado)
                End If

                ' Si no existe, insertar nueva categoría
                Dim cmdInsertar As New SqlCommand("INSERT INTO Categoria (nombre) OUTPUT INSERTED.id_categoria VALUES (@nombre)", cn)
                cmdInsertar.Parameters.AddWithValue("@nombre", nombreCategoria.Trim())
                Dim nuevoId = Convert.ToInt32(cmdInsertar.ExecuteScalar())

                ' Recargar combo de categorías
                CargarCategorias()
                CBCategoria.Text = nombreCategoria.Trim()

                Return nuevoId
            End Using
        Catch ex As Exception
            MessageBox.Show("Error al gestionar categoría: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return -1
        End Try
    End Function

    ' --- VALIDACIONES INDIVIDUALES ---
    Private Function ValidarCodigoBarra() As Boolean
        Dim codigo As String = TBCodBarra.Text.Trim()

        If String.IsNullOrWhiteSpace(codigo) Then
            MessageBox.Show("El código de barras es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            Return False
        End If

        If codigo.Length < 3 Then
            MessageBox.Show("El código de barras debe tener al menos 3 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            TBCodBarra.SelectAll()
            Return False
        End If

        Return True
    End Function

    ' --- NUEVA VALIDACIÓN PARA NOMBRE ---
    Private Function ValidarNombre() As Boolean
        Dim nombre As String = TBNombre.Text.Trim()

        If String.IsNullOrWhiteSpace(nombre) Then
            MessageBox.Show("El nombre de la bebida es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBNombre.Focus()
            Return False
        End If

        If nombre.Length < 2 Then
            MessageBox.Show("El nombre debe tener al menos 2 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBNombre.Focus()
            TBNombre.SelectAll()
            Return False
        End If

        If nombre.Length > 100 Then
            MessageBox.Show("El nombre no puede tener más de 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBNombre.Focus()
            TBNombre.SelectAll()
            Return False
        End If

        ' Validar que contenga solo letras, números, espacios y caracteres especiales comunes
        For Each c As Char In nombre
            If Not Char.IsLetterOrDigit(c) AndAlso c <> " "c AndAlso c <> "-"c AndAlso c <> "."c AndAlso c <> ","c Then
                MessageBox.Show("El nombre solo puede contener letras, números, espacios, guiones, puntos y comas.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBNombre.Focus()
                TBNombre.SelectAll()
                Return False
            End If
        Next

        Return True
    End Function

    Private Function ValidarCategoria() As Boolean
        If String.IsNullOrWhiteSpace(CBCategoria.Text.Trim()) Then
            MessageBox.Show("Debe ingresar una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CBCategoria.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function ValidarMarca() As Boolean
        If String.IsNullOrWhiteSpace(CBMarca.Text.Trim()) Then
            MessageBox.Show("Debe ingresar una marca.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            CBMarca.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function ValidarPrecio() As Boolean
        If NUDPrecio.Value <= 0 Then
            MessageBox.Show("El precio debe ser mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            NUDPrecio.Focus()
            Return False
        End If
        Return True
    End Function

    Private Function ValidarVolumen() As Boolean
        Dim volumen As String = TBVolumen.Text.Trim()

        If String.IsNullOrWhiteSpace(volumen) Then
            MessageBox.Show("El volumen es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBVolumen.Focus()
            Return False
        End If

        ' Validar que sea numérico (puede contener decimales)
        Dim volumenNum As Decimal
        If Not Decimal.TryParse(volumen, volumenNum) Then
            MessageBox.Show("El volumen debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBVolumen.Focus()
            TBVolumen.SelectAll()
            Return False
        End If

        If volumenNum <= 0 Then
            MessageBox.Show("El volumen debe ser mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBVolumen.Focus()
            TBVolumen.SelectAll()
            Return False
        End If

        Return True
    End Function

    Private Function ValidarGraduacion() As Boolean
        Dim graduacion As String = TBGraduacion.Text.Trim()

        If String.IsNullOrWhiteSpace(graduacion) Then
            MessageBox.Show("La graduación alcohólica es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBGraduacion.Focus()
            Return False
        End If

        ' Validar que sea numérico (puede contener decimales)
        Dim graduacionNum As Decimal
        If Not Decimal.TryParse(graduacion, graduacionNum) Then
            MessageBox.Show("La graduación debe ser un número válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBGraduacion.Focus()
            TBGraduacion.SelectAll()
            Return False
        End If

        Return True
    End Function

    Private Function ValidarProveedor() As Boolean
        Dim proveedor As String = TBProveedor.Text.Trim()

        If String.IsNullOrWhiteSpace(proveedor) Then
            MessageBox.Show("El proveedor es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBProveedor.Focus()
            Return False
        End If

        Return True
    End Function

    Private Function ValidarStock() As Boolean
        If NUDStock.Value < 0 Then
            MessageBox.Show("El stock no puede ser negativo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
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
        If ExisteProducto(codigo) Then
            MessageBox.Show("Este código de barras ya está registrado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TBCodBarra.Focus()
            TBCodBarra.SelectAll()
            Return False
        End If

        Return ValidarNombre() AndAlso
               ValidarCategoria() AndAlso
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
            If ExisteProducto(codigo) Then
                MessageBox.Show("Este código de barras ya está registrado en otro producto.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                TBCodBarra.Focus()
                TBCodBarra.SelectAll()
                Return False
            End If
        End If

        Return ValidarNombre() AndAlso
               ValidarCategoria() AndAlso
               ValidarMarca() AndAlso
               ValidarPrecio() AndAlso
               ValidarVolumen() AndAlso
               ValidarGraduacion() AndAlso
               ValidarProveedor() AndAlso
               ValidarStock()
    End Function

    Private Function ExisteProducto(codigoBarra As String) As Boolean
        Try
            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim cmd As New SqlCommand("SELECT 1 FROM Producto WHERE codigo_barras = @codigo", cn)
                cmd.Parameters.AddWithValue("@codigo", codigoBarra)
                cn.Open()
                Return cmd.ExecuteScalar() IsNot Nothing
            End Using
        Catch ex As Exception
            Return False
        End Try
    End Function

    ' --- BOTÓN AGREGAR ---
    Private Sub BAgregar_Click(sender As Object, e As EventArgs) Handles BAgregar.Click
        If Not ValidarCamposParaAgregar() Then
            Exit Sub
        End If

        Try
            ' Obtener o crear marca y categoría
            Dim idMarca As Integer = ObtenerOInsertarMarca(CBMarca.Text.Trim())
            Dim idCategoria As Integer = ObtenerOInsertarCategoria(CBCategoria.Text.Trim())

            If idMarca = -1 OrElse idCategoria = -1 Then
                MessageBox.Show("Error al gestionar marca o categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim query As String = "INSERT INTO Producto (codigo_barras, nombre, id_categoria, id_marca, precio, volumen, graduacion, proveedor, stock, estado) " &
                                      "VALUES (@codigo, @nombre, @id_categoria, @id_marca, @precio, @volumen, @graduacion, @proveedor, @stock, 'A')"
                Dim cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@codigo", TBCodBarra.Text.Trim())
                cmd.Parameters.AddWithValue("@nombre", TBNombre.Text.Trim()) ' Nuevo parámetro
                cmd.Parameters.AddWithValue("@id_categoria", idCategoria)
                cmd.Parameters.AddWithValue("@id_marca", idMarca)
                cmd.Parameters.AddWithValue("@precio", NUDPrecio.Value)
                cmd.Parameters.AddWithValue("@volumen", Convert.ToDecimal(TBVolumen.Text.Trim()))
                cmd.Parameters.AddWithValue("@graduacion", Convert.ToDecimal(TBGraduacion.Text.Trim()))
                cmd.Parameters.AddWithValue("@proveedor", TBProveedor.Text.Trim())
                cmd.Parameters.AddWithValue("@stock", CInt(NUDStock.Value))

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

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
            MessageBox.Show("Seleccione un producto para editar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        If Not ValidarCamposParaEditar() Then
            Exit Sub
        End If

        Try
            ' Obtener o crear marca y categoría
            Dim idMarca As Integer = ObtenerOInsertarMarca(CBMarca.Text.Trim())
            Dim idCategoria As Integer = ObtenerOInsertarCategoria(CBCategoria.Text.Trim())

            If idMarca = -1 OrElse idCategoria = -1 Then
                MessageBox.Show("Error al gestionar marca o categoría.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim query As String = "UPDATE Producto SET nombre=@nombre, id_categoria=@id_categoria, id_marca=@id_marca, precio=@precio, " &
                                      "volumen=@volumen, graduacion=@graduacion, proveedor=@proveedor, stock=@stock " &
                                      "WHERE codigo_barras=@codigo"
                Dim cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@codigo", TBCodBarra.Text.Trim())
                cmd.Parameters.AddWithValue("@nombre", TBNombre.Text.Trim()) ' Nuevo parámetro
                cmd.Parameters.AddWithValue("@id_categoria", idCategoria)
                cmd.Parameters.AddWithValue("@id_marca", idMarca)
                cmd.Parameters.AddWithValue("@precio", NUDPrecio.Value)
                cmd.Parameters.AddWithValue("@volumen", Convert.ToDecimal(TBVolumen.Text.Trim()))
                cmd.Parameters.AddWithValue("@graduacion", Convert.ToDecimal(TBGraduacion.Text.Trim()))
                cmd.Parameters.AddWithValue("@proveedor", TBProveedor.Text.Trim())
                cmd.Parameters.AddWithValue("@stock", CInt(NUDStock.Value))

                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

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
            MessageBox.Show("Seleccione un producto primero.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Try
            Dim row As DataGridViewRow = DGVProductos.SelectedRows(0)
            Dim codigo As String = row.Cells("codigo_barras").Value.ToString()
            Dim estadoActual As String = row.Cells("estado").Value.ToString()

            Dim nuevoEstado As String = If(estadoActual = "A", "I", "A")

            Dim ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

            Using cn As New SqlConnection(ConnStr)
                Dim query As String = "UPDATE Producto SET estado=@estado WHERE codigo_barras=@codigo"
                Dim cmd As New SqlCommand(query, cn)
                cmd.Parameters.AddWithValue("@estado", nuevoEstado)
                cmd.Parameters.AddWithValue("@codigo", codigo)
                cn.Open()
                cmd.ExecuteNonQuery()
            End Using

            MessageBox.Show($"El producto fue actualizado a '{If(nuevoEstado = "A", "Activo", "Inactivo")}'.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
                codigoBarraOriginal = row.Cells("codigo_barras").Value.ToString()

                TBCodBarra.Text = codigoBarraOriginal
                TBNombre.Text = row.Cells("nombre").Value.ToString() ' Nuevo campo
                CBCategoria.Text = row.Cells("categoria").Value.ToString()
                CBMarca.Text = row.Cells("marca").Value.ToString()
                NUDPrecio.Value = Convert.ToDecimal(row.Cells("precio").Value)
                TBVolumen.Text = row.Cells("volumen").Value.ToString()
                TBGraduacion.Text = row.Cells("graduacion").Value.ToString()
                TBProveedor.Text = row.Cells("proveedor").Value.ToString()
                NUDStock.Value = Convert.ToDecimal(row.Cells("stock").Value)

                ' Actualizar texto del botón según estado
                Dim estadoActual As String = row.Cells("estado").Value.ToString()
                ActualizarBotonSuspender(estadoActual)

            Catch ex As Exception
                MessageBox.Show($"Error al cargar datos del producto: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    ' --- FORMATEAR FILAS SEGÚN ESTADO Y STOCK ---
    Private Sub DGVProductos_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DGVProductos.CellFormatting
        If e.RowIndex >= 0 AndAlso e.Value IsNot Nothing Then
            Dim row As DataGridViewRow = DGVProductos.Rows(e.RowIndex)

            ' Formato por estado
            If DGVProductos.Columns(e.ColumnIndex).Name = "estado" Then
                If e.Value.ToString() = "I" Then
                    row.DefaultCellStyle.ForeColor = Color.Gray
                    row.DefaultCellStyle.BackColor = Color.LightYellow
                Else
                    row.DefaultCellStyle.ForeColor = Color.Black
                    row.DefaultCellStyle.BackColor = Color.White
                End If
            End If

            ' Formato por stock bajo
            If DGVProductos.Columns(e.ColumnIndex).Name = "stock" Then
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
        TBNombre.Clear() ' Nuevo campo
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
        If estado = "A" Then
            BSuspender.Text = "Suspender"
            BSuspender.BackColor = Color.Red
            BSuspender.ForeColor = Color.White
        Else
            BSuspender.Text = "Reactivar"
            BSuspender.BackColor = Color.Green
            BSuspender.ForeColor = Color.White
        End If
    End Sub

    ' --- EVENTOS DE COMBOBOX ---
    Private Sub CBMarca_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBMarca.SelectedIndexChanged
        ' Manejar cuando el usuario selecciona una marca existente
    End Sub

    Private Sub CBCategoria_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBCategoria.SelectedIndexChanged
        ' Manejar cuando el usuario selecciona una categoría existente
    End Sub

    ' --- VALIDACIÓN EN TIEMPO REAL PARA NOMBRE ---
    Private Sub TBNombre_TextChanged(sender As Object, e As EventArgs) Handles TBNombre.TextChanged
        ' Validar longitud en tiempo real
        If TBNombre.Text.Length > 100 Then
            TBNombre.Text = TBNombre.Text.Substring(0, 100)
            TBNombre.SelectionStart = TBNombre.Text.Length
            MessageBox.Show("El nombre no puede tener más de 100 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    ' --- VALIDACIÓN EN TIEMPO REAL ---
    Private Sub TBCodBarra_TextChanged(sender As Object, e As EventArgs) Handles TBCodBarra.TextChanged
        ' Permitir cualquier carácter para código de barras
    End Sub

    Private Sub TBVolumen_TextChanged(sender As Object, e As EventArgs) Handles TBVolumen.TextChanged
        ' Permitir números y punto decimal
        Dim texto As String = TBVolumen.Text
        If texto.Length > 0 AndAlso Not System.Text.RegularExpressions.Regex.IsMatch(texto, "^[\d\.]*$") Then
            ' Mantener solo números y punto
            Dim soloNumeros As String = System.Text.RegularExpressions.Regex.Replace(texto, "[^\d\.]", "")
            TBVolumen.Text = soloNumeros
            TBVolumen.SelectionStart = TBVolumen.Text.Length
        End If
    End Sub

    Private Sub TBGraduacion_TextChanged(sender As Object, e As EventArgs) Handles TBGraduacion.TextChanged
        ' Permitir números y punto decimal
        Dim texto As String = TBGraduacion.Text
        If texto.Length > 0 AndAlso Not System.Text.RegularExpressions.Regex.IsMatch(texto, "^[\d\.]*$") Then
            ' Mantener solo números y punto
            Dim soloNumeros As String = System.Text.RegularExpressions.Regex.Replace(texto, "[^\d\.]", "")
            TBGraduacion.Text = soloNumeros
            TBGraduacion.SelectionStart = TBGraduacion.Text.Length
        End If
    End Sub
End Class