<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormProductos
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Panel2 = New Panel()
        Panel1 = New Panel()
        Panel4 = New Panel()
        DGVProductos = New DataGridView()
        TableLayoutPanel1 = New TableLayoutPanel()
        BSuspender = New Button()
        BAgregar = New Button()
        BEditar = New Button()
        Panel3 = New Panel()
        TableLayoutPanel2 = New TableLayoutPanel()
        LCodigoBarra = New Label()
        TBCodBarra = New TextBox()
        CBCategoria = New ComboBox()
        CBMarca = New ComboBox()
        LPrecio = New Label()
        NUDPrecio = New NumericUpDown()
        LVolumen = New Label()
        TBVolumen = New TextBox()
        LGraduacion = New Label()
        TBGraduacion = New TextBox()
        TBProveedor = New TextBox()
        LProveedor = New Label()
        LStock = New Label()
        NUDStock = New NumericUpDown()
        LNombre = New Label()
        TBNombre = New TextBox()
        Panel2.SuspendLayout()
        Panel1.SuspendLayout()
        Panel4.SuspendLayout()
        CType(DGVProductos, ComponentModel.ISupportInitialize).BeginInit()
        TableLayoutPanel1.SuspendLayout()
        Panel3.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        CType(NUDPrecio, ComponentModel.ISupportInitialize).BeginInit()
        CType(NUDStock, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.LightGray
        Panel2.Controls.Add(Panel1)
        Panel2.Controls.Add(Panel3)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 0)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(800, 451)
        Panel2.TabIndex = 1
        ' 
        ' Panel1
        ' 
        Panel1.AutoSize = True
        Panel1.Controls.Add(Panel4)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(0, 153)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(800, 298)
        Panel1.TabIndex = 3
        ' 
        ' Panel4
        ' 
        Panel4.Controls.Add(DGVProductos)
        Panel4.Controls.Add(TableLayoutPanel1)
        Panel4.Dock = DockStyle.Fill
        Panel4.Location = New Point(0, 0)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(800, 298)
        Panel4.TabIndex = 4
        ' 
        ' DGVProductos
        ' 
        DGVProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DGVProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVProductos.Dock = DockStyle.Fill
        DGVProductos.Location = New Point(0, 64)
        DGVProductos.Name = "DGVProductos"
        DGVProductos.RowHeadersWidth = 51
        DGVProductos.Size = New Size(800, 234)
        DGVProductos.TabIndex = 2
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.Controls.Add(BSuspender, 2, 0)
        TableLayoutPanel1.Controls.Add(BAgregar, 0, 0)
        TableLayoutPanel1.Controls.Add(BEditar, 1, 0)
        TableLayoutPanel1.Dock = DockStyle.Top
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(800, 64)
        TableLayoutPanel1.TabIndex = 3
        ' 
        ' BSuspender
        ' 
        BSuspender.Anchor = AnchorStyles.None
        BSuspender.Location = New Point(596, 13)
        BSuspender.Name = "BSuspender"
        BSuspender.Size = New Size(139, 37)
        BSuspender.TabIndex = 1
        BSuspender.Text = "Suspender"
        BSuspender.UseVisualStyleBackColor = True
        ' 
        ' BAgregar
        ' 
        BAgregar.Anchor = AnchorStyles.None
        BAgregar.Location = New Point(52, 14)
        BAgregar.Name = "BAgregar"
        BAgregar.Size = New Size(162, 36)
        BAgregar.TabIndex = 2
        BAgregar.Text = "Agregar"
        BAgregar.UseVisualStyleBackColor = True
        ' 
        ' BEditar
        ' 
        BEditar.Anchor = AnchorStyles.None
        BEditar.Location = New Point(281, 14)
        BEditar.Name = "BEditar"
        BEditar.Size = New Size(235, 35)
        BEditar.TabIndex = 0
        BEditar.Text = "Editar"
        BEditar.UseVisualStyleBackColor = True
        ' 
        ' Panel3
        ' 
        Panel3.Controls.Add(TableLayoutPanel2)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(800, 153)
        Panel3.TabIndex = 0
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 4
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30F))
        TableLayoutPanel2.Controls.Add(TBNombre, 3, 3)
        TableLayoutPanel2.Controls.Add(LNombre, 2, 3)
        TableLayoutPanel2.Controls.Add(LCodigoBarra, 0, 0)
        TableLayoutPanel2.Controls.Add(TBCodBarra, 1, 0)
        TableLayoutPanel2.Controls.Add(CBCategoria, 1, 1)
        TableLayoutPanel2.Controls.Add(CBMarca, 0, 1)
        TableLayoutPanel2.Controls.Add(LPrecio, 0, 2)
        TableLayoutPanel2.Controls.Add(NUDPrecio, 1, 2)
        TableLayoutPanel2.Controls.Add(LVolumen, 2, 0)
        TableLayoutPanel2.Controls.Add(TBVolumen, 3, 0)
        TableLayoutPanel2.Controls.Add(LGraduacion, 2, 1)
        TableLayoutPanel2.Controls.Add(TBGraduacion, 3, 1)
        TableLayoutPanel2.Controls.Add(TBProveedor, 3, 2)
        TableLayoutPanel2.Controls.Add(LProveedor, 2, 2)
        TableLayoutPanel2.Controls.Add(LStock, 0, 3)
        TableLayoutPanel2.Controls.Add(NUDStock, 1, 3)
        TableLayoutPanel2.Dock = DockStyle.Top
        TableLayoutPanel2.Location = New Point(0, 0)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 4
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel2.Size = New Size(800, 153)
        TableLayoutPanel2.TabIndex = 0
        ' 
        ' LCodigoBarra
        ' 
        LCodigoBarra.Anchor = AnchorStyles.Right
        LCodigoBarra.AutoSize = True
        LCodigoBarra.Location = New Point(30, 9)
        LCodigoBarra.Name = "LCodigoBarra"
        LCodigoBarra.Size = New Size(127, 20)
        LCodigoBarra.TabIndex = 0
        LCodigoBarra.Text = "Código de Barras:"
        ' 
        ' TBCodBarra
        ' 
        TBCodBarra.Anchor = AnchorStyles.Left
        TBCodBarra.Location = New Point(163, 5)
        TBCodBarra.Name = "TBCodBarra"
        TBCodBarra.Size = New Size(150, 27)
        TBCodBarra.TabIndex = 1
        ' 
        ' CBCategoria
        ' 
        CBCategoria.Anchor = AnchorStyles.Left
        CBCategoria.FormattingEnabled = True
        CBCategoria.Location = New Point(163, 43)
        CBCategoria.Name = "CBCategoria"
        CBCategoria.Size = New Size(151, 28)
        CBCategoria.TabIndex = 2
        CBCategoria.Text = "CATEGORIAS"
        ' 
        ' CBMarca
        ' 
        CBMarca.Anchor = AnchorStyles.Right
        CBMarca.FormattingEnabled = True
        CBMarca.Location = New Point(6, 43)
        CBMarca.Name = "CBMarca"
        CBMarca.Size = New Size(151, 28)
        CBMarca.TabIndex = 3
        CBMarca.Text = "MARCA"
        ' 
        ' LPrecio
        ' 
        LPrecio.Anchor = AnchorStyles.Right
        LPrecio.AutoSize = True
        LPrecio.Location = New Point(104, 85)
        LPrecio.Name = "LPrecio"
        LPrecio.Size = New Size(53, 20)
        LPrecio.TabIndex = 4
        LPrecio.Text = "Precio:"
        ' 
        ' NUDPrecio
        ' 
        NUDPrecio.Anchor = AnchorStyles.Left
        NUDPrecio.DecimalPlaces = 2
        NUDPrecio.Location = New Point(163, 81)
        NUDPrecio.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        NUDPrecio.Name = "NUDPrecio"
        NUDPrecio.Size = New Size(150, 27)
        NUDPrecio.TabIndex = 5
        ' 
        ' LVolumen
        ' 
        LVolumen.Anchor = AnchorStyles.Right
        LVolumen.AutoSize = True
        LVolumen.Location = New Point(487, 9)
        LVolumen.Name = "LVolumen"
        LVolumen.Size = New Size(70, 20)
        LVolumen.TabIndex = 6
        LVolumen.Text = "Volumen:"
        ' 
        ' TBVolumen
        ' 
        TBVolumen.Anchor = AnchorStyles.Left
        TBVolumen.Location = New Point(563, 5)
        TBVolumen.Name = "TBVolumen"
        TBVolumen.Size = New Size(125, 27)
        TBVolumen.TabIndex = 7
        ' 
        ' LGraduacion
        ' 
        LGraduacion.Anchor = AnchorStyles.Right
        LGraduacion.AutoSize = True
        LGraduacion.Location = New Point(477, 47)
        LGraduacion.Name = "LGraduacion"
        LGraduacion.Size = New Size(80, 20)
        LGraduacion.TabIndex = 8
        LGraduacion.Text = "Gradución:"
        ' 
        ' TBGraduacion
        ' 
        TBGraduacion.Anchor = AnchorStyles.Left
        TBGraduacion.Location = New Point(563, 43)
        TBGraduacion.Name = "TBGraduacion"
        TBGraduacion.Size = New Size(125, 27)
        TBGraduacion.TabIndex = 9
        ' 
        ' TBProveedor
        ' 
        TBProveedor.Anchor = AnchorStyles.Left
        TBProveedor.Location = New Point(563, 81)
        TBProveedor.Name = "TBProveedor"
        TBProveedor.Size = New Size(125, 27)
        TBProveedor.TabIndex = 11
        ' 
        ' LProveedor
        ' 
        LProveedor.Anchor = AnchorStyles.Right
        LProveedor.AutoSize = True
        LProveedor.Location = New Point(477, 85)
        LProveedor.Name = "LProveedor"
        LProveedor.Size = New Size(80, 20)
        LProveedor.TabIndex = 10
        LProveedor.Text = "Proveedor:"
        ' 
        ' LStock
        ' 
        LStock.Anchor = AnchorStyles.Right
        LStock.AutoSize = True
        LStock.Location = New Point(109, 123)
        LStock.Name = "LStock"
        LStock.Size = New Size(48, 20)
        LStock.TabIndex = 12
        LStock.Text = "Stock:"
        LStock.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' NUDStock
        ' 
        NUDStock.Anchor = AnchorStyles.Left
        NUDStock.Location = New Point(163, 120)
        NUDStock.Maximum = New Decimal(New Integer() {10000, 0, 0, 0})
        NUDStock.Name = "NUDStock"
        NUDStock.Size = New Size(150, 27)
        NUDStock.TabIndex = 13
        ' 
        ' LNombre
        ' 
        LNombre.Anchor = AnchorStyles.Right
        LNombre.AutoSize = True
        LNombre.Location = New Point(439, 123)
        LNombre.Name = "LNombre"
        LNombre.Size = New Size(118, 20)
        LNombre.TabIndex = 14
        LNombre.Text = "Nombre Bebida:"
        LNombre.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TBNombre
        ' 
        TBNombre.Anchor = AnchorStyles.Left
        TBNombre.Location = New Point(563, 120)
        TBNombre.Name = "TBNombre"
        TBNombre.Size = New Size(125, 27)
        TBNombre.TabIndex = 15
        ' 
        ' FormProductos
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 451)
        Controls.Add(Panel2)
        Name = "FormProductos"
        Text = "FormProductos"
        Panel2.ResumeLayout(False)
        Panel2.PerformLayout()
        Panel1.ResumeLayout(False)
        Panel4.ResumeLayout(False)
        CType(DGVProductos, ComponentModel.ISupportInitialize).EndInit()
        TableLayoutPanel1.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        TableLayoutPanel2.ResumeLayout(False)
        TableLayoutPanel2.PerformLayout()
        CType(NUDPrecio, ComponentModel.ISupportInitialize).EndInit()
        CType(NUDStock, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents LCodigoBarra As Label
    Friend WithEvents TBCodBarra As TextBox
    Friend WithEvents CBCategoria As ComboBox
    Friend WithEvents CBMarca As ComboBox
    Friend WithEvents LPrecio As Label
    Friend WithEvents NUDPrecio As NumericUpDown
    Friend WithEvents LVolumen As Label
    Friend WithEvents TBVolumen As TextBox
    Friend WithEvents LGraduacion As Label
    Friend WithEvents TBGraduacion As TextBox
    Friend WithEvents LProveedor As Label
    Friend WithEvents TBProveedor As TextBox
    Friend WithEvents DGVProductos As DataGridView
    Friend WithEvents BEditar As Button
    Friend WithEvents BSuspender As Button
    Friend WithEvents BAgregar As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents LStock As Label
    Friend WithEvents NUDStock As NumericUpDown
    Friend WithEvents TBNombre As TextBox
    Friend WithEvents LNombre As Label
End Class
