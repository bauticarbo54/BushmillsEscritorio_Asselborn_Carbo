<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormVentas
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
        Panel1 = New Panel()
        TableLayoutPanel1 = New TableLayoutPanel()
        NUDCantidad = New NumericUpDown()
        LCantidad = New Label()
        TBCodBarra = New TextBox()
        LNombreClienteConf = New Label()
        LCodBarra = New Label()
        LVendedorID = New Label()
        LVendedor = New Label()
        LNombre = New Label()
        LNombreVend = New Label()
        LDNICliente = New Label()
        TBDNICliente = New TextBox()
        LNombreCliente = New Label()
        LNuevoCliente = New Label()
        BCrearCliente = New Button()
        Panel2 = New Panel()
        TableLayoutPanel2 = New TableLayoutPanel()
        BCancelar = New Button()
        BAgregarProducto = New Button()
        BConfirmarVenta = New Button()
        Panel3 = New Panel()
        DGVDetalleVenta = New DataGridView()
        Panel4 = New Panel()
        TableLayoutPanel3 = New TableLayoutPanel()
        LTotalTexto = New Label()
        LTotal = New Label()
        Panel1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        CType(NUDCantidad, ComponentModel.ISupportInitialize).BeginInit()
        Panel2.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        Panel3.SuspendLayout()
        CType(DGVDetalleVenta, ComponentModel.ISupportInitialize).BeginInit()
        Panel4.SuspendLayout()
        TableLayoutPanel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.LightGray
        Panel1.Controls.Add(TableLayoutPanel1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(800, 163)
        Panel1.TabIndex = 0
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.BackColor = Color.LightGray
        TableLayoutPanel1.ColumnCount = 4
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30F))
        TableLayoutPanel1.Controls.Add(NUDCantidad, 3, 3)
        TableLayoutPanel1.Controls.Add(LCantidad, 2, 3)
        TableLayoutPanel1.Controls.Add(TBCodBarra, 1, 3)
        TableLayoutPanel1.Controls.Add(LNombreClienteConf, 3, 1)
        TableLayoutPanel1.Controls.Add(LCodBarra, 0, 3)
        TableLayoutPanel1.Controls.Add(LVendedorID, 1, 0)
        TableLayoutPanel1.Controls.Add(LVendedor, 0, 0)
        TableLayoutPanel1.Controls.Add(LNombre, 0, 1)
        TableLayoutPanel1.Controls.Add(LNombreVend, 1, 1)
        TableLayoutPanel1.Controls.Add(LDNICliente, 2, 0)
        TableLayoutPanel1.Controls.Add(TBDNICliente, 3, 0)
        TableLayoutPanel1.Controls.Add(LNombreCliente, 2, 1)
        TableLayoutPanel1.Controls.Add(LNuevoCliente, 1, 2)
        TableLayoutPanel1.Controls.Add(BCrearCliente, 2, 2)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 4
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.Size = New Size(800, 163)
        TableLayoutPanel1.TabIndex = 1
        ' 
        ' NUDCantidad
        ' 
        NUDCantidad.Anchor = AnchorStyles.Left
        NUDCantidad.Location = New Point(563, 128)
        NUDCantidad.Name = "NUDCantidad"
        NUDCantidad.Size = New Size(150, 27)
        NUDCantidad.TabIndex = 14
        ' 
        ' LCantidad
        ' 
        LCantidad.Anchor = AnchorStyles.Right
        LCantidad.AutoSize = True
        LCantidad.Location = New Point(485, 131)
        LCantidad.Name = "LCantidad"
        LCantidad.Size = New Size(72, 20)
        LCantidad.TabIndex = 9
        LCantidad.Text = "Cantidad:"
        ' 
        ' TBCodBarra
        ' 
        TBCodBarra.Anchor = AnchorStyles.Left
        TBCodBarra.Location = New Point(163, 128)
        TBCodBarra.Name = "TBCodBarra"
        TBCodBarra.Size = New Size(178, 27)
        TBCodBarra.TabIndex = 13
        ' 
        ' LNombreClienteConf
        ' 
        LNombreClienteConf.Anchor = AnchorStyles.Left
        LNombreClienteConf.AutoSize = True
        LNombreClienteConf.Location = New Point(563, 50)
        LNombreClienteConf.Name = "LNombreClienteConf"
        LNombreClienteConf.Size = New Size(13, 20)
        LNombreClienteConf.TabIndex = 10
        LNombreClienteConf.Text = " "
        ' 
        ' LCodBarra
        ' 
        LCodBarra.Anchor = AnchorStyles.Right
        LCodBarra.AutoSize = True
        LCodBarra.Location = New Point(36, 131)
        LCodBarra.Name = "LCodBarra"
        LCodBarra.Size = New Size(121, 20)
        LCodBarra.TabIndex = 0
        LCodBarra.Text = "Código de barra:"
        ' 
        ' LVendedorID
        ' 
        LVendedorID.Anchor = AnchorStyles.Left
        LVendedorID.AutoSize = True
        LVendedorID.Location = New Point(163, 10)
        LVendedorID.Name = "LVendedorID"
        LVendedorID.Size = New Size(116, 20)
        LVendedorID.TabIndex = 7
        LVendedorID.Text = "ID del vendedor"
        ' 
        ' LVendedor
        ' 
        LVendedor.Anchor = AnchorStyles.Right
        LVendedor.AutoSize = True
        LVendedor.Location = New Point(62, 10)
        LVendedor.Name = "LVendedor"
        LVendedor.Size = New Size(95, 20)
        LVendedor.TabIndex = 0
        LVendedor.Text = "ID Vendedor:"
        ' 
        ' LNombre
        ' 
        LNombre.Anchor = AnchorStyles.Right
        LNombre.AutoSize = True
        LNombre.Location = New Point(90, 50)
        LNombre.Name = "LNombre"
        LNombre.Size = New Size(67, 20)
        LNombre.TabIndex = 2
        LNombre.Text = "Nombre:"
        ' 
        ' LNombreVend
        ' 
        LNombreVend.Anchor = AnchorStyles.Left
        LNombreVend.AutoSize = True
        LNombreVend.Location = New Point(163, 50)
        LNombreVend.Name = "LNombreVend"
        LNombreVend.Size = New Size(13, 20)
        LNombreVend.TabIndex = 8
        LNombreVend.Text = " "
        ' 
        ' LDNICliente
        ' 
        LDNICliente.Anchor = AnchorStyles.Right
        LDNICliente.AutoSize = True
        LDNICliente.Location = New Point(469, 10)
        LDNICliente.Name = "LDNICliente"
        LDNICliente.Size = New Size(88, 20)
        LDNICliente.TabIndex = 9
        LDNICliente.Text = "DNI Cliente:"
        ' 
        ' TBDNICliente
        ' 
        TBDNICliente.Anchor = AnchorStyles.Left
        TBDNICliente.Location = New Point(563, 6)
        TBDNICliente.Name = "TBDNICliente"
        TBDNICliente.Size = New Size(178, 27)
        TBDNICliente.TabIndex = 6
        ' 
        ' LNombreCliente
        ' 
        LNombreCliente.Anchor = AnchorStyles.Right
        LNombreCliente.AutoSize = True
        LNombreCliente.Location = New Point(440, 50)
        LNombreCliente.Name = "LNombreCliente"
        LNombreCliente.Size = New Size(117, 20)
        LNombreCliente.TabIndex = 1
        LNombreCliente.Text = "Nombre Cliente:"
        ' 
        ' LNuevoCliente
        ' 
        LNuevoCliente.Anchor = AnchorStyles.Right
        LNuevoCliente.AutoSize = True
        LNuevoCliente.Location = New Point(306, 90)
        LNuevoCliente.Name = "LNuevoCliente"
        LNuevoCliente.Size = New Size(91, 20)
        LNuevoCliente.TabIndex = 11
        LNuevoCliente.Text = "Crear nuevo:"
        ' 
        ' BCrearCliente
        ' 
        BCrearCliente.Anchor = AnchorStyles.Left
        BCrearCliente.Location = New Point(403, 85)
        BCrearCliente.Name = "BCrearCliente"
        BCrearCliente.Size = New Size(94, 29)
        BCrearCliente.TabIndex = 12
        BCrearCliente.Text = "Cliente"
        BCrearCliente.UseVisualStyleBackColor = True
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.LightGray
        Panel2.Controls.Add(TableLayoutPanel2)
        Panel2.Dock = DockStyle.Top
        Panel2.Location = New Point(0, 163)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(800, 66)
        Panel2.TabIndex = 1
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 3
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel2.Controls.Add(BCancelar, 2, 0)
        TableLayoutPanel2.Controls.Add(BAgregarProducto, 0, 0)
        TableLayoutPanel2.Controls.Add(BConfirmarVenta, 1, 0)
        TableLayoutPanel2.Dock = DockStyle.Top
        TableLayoutPanel2.Location = New Point(0, 0)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 1
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel2.Size = New Size(800, 64)
        TableLayoutPanel2.TabIndex = 4
        ' 
        ' BCancelar
        ' 
        BCancelar.Anchor = AnchorStyles.None
        BCancelar.Location = New Point(596, 13)
        BCancelar.Name = "BCancelar"
        BCancelar.Size = New Size(140, 37)
        BCancelar.TabIndex = 1
        BCancelar.Text = "Cancelar Venta"
        BCancelar.UseVisualStyleBackColor = True
        ' 
        ' BAgregarProducto
        ' 
        BAgregarProducto.Anchor = AnchorStyles.None
        BAgregarProducto.Location = New Point(52, 14)
        BAgregarProducto.Name = "BAgregarProducto"
        BAgregarProducto.Size = New Size(162, 36)
        BAgregarProducto.TabIndex = 2
        BAgregarProducto.Text = "Agregar Producto"
        BAgregarProducto.UseVisualStyleBackColor = True
        ' 
        ' BConfirmarVenta
        ' 
        BConfirmarVenta.Anchor = AnchorStyles.None
        BConfirmarVenta.Location = New Point(281, 14)
        BConfirmarVenta.Name = "BConfirmarVenta"
        BConfirmarVenta.Size = New Size(236, 35)
        BConfirmarVenta.TabIndex = 0
        BConfirmarVenta.Text = "Confirmar Venta"
        BConfirmarVenta.UseVisualStyleBackColor = True
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = SystemColors.ControlDark
        Panel3.Controls.Add(DGVDetalleVenta)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(0, 229)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(800, 221)
        Panel3.TabIndex = 2
        ' 
        ' DGVDetalleVenta
        ' 
        DGVDetalleVenta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVDetalleVenta.Dock = DockStyle.Fill
        DGVDetalleVenta.Location = New Point(0, 0)
        DGVDetalleVenta.Name = "DGVDetalleVenta"
        DGVDetalleVenta.RowHeadersWidth = 51
        DGVDetalleVenta.Size = New Size(800, 221)
        DGVDetalleVenta.TabIndex = 0
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.LightGray
        Panel4.Controls.Add(TableLayoutPanel3)
        Panel4.Dock = DockStyle.Bottom
        Panel4.Location = New Point(0, 393)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(800, 57)
        Panel4.TabIndex = 3
        ' 
        ' TableLayoutPanel3
        ' 
        TableLayoutPanel3.ColumnCount = 2
        TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel3.Controls.Add(LTotalTexto, 0, 0)
        TableLayoutPanel3.Controls.Add(LTotal, 1, 0)
        TableLayoutPanel3.Dock = DockStyle.Fill
        TableLayoutPanel3.Location = New Point(0, 0)
        TableLayoutPanel3.Name = "TableLayoutPanel3"
        TableLayoutPanel3.RowCount = 1
        TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel3.Size = New Size(800, 57)
        TableLayoutPanel3.TabIndex = 0
        ' 
        ' LTotalTexto
        ' 
        LTotalTexto.Anchor = AnchorStyles.Right
        LTotalTexto.AutoSize = True
        LTotalTexto.Location = New Point(352, 18)
        LTotalTexto.Name = "LTotalTexto"
        LTotalTexto.Size = New Size(45, 20)
        LTotalTexto.TabIndex = 0
        LTotalTexto.Text = "Total:"
        ' 
        ' LTotal
        ' 
        LTotal.Anchor = AnchorStyles.Left
        LTotal.AutoSize = True
        LTotal.Location = New Point(403, 18)
        LTotal.Name = "LTotal"
        LTotal.Size = New Size(17, 20)
        LTotal.TabIndex = 1
        LTotal.Text = "$"
        ' 
        ' FormVentas
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Panel4)
        Controls.Add(Panel3)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "FormVentas"
        Text = "FormVentas"
        Panel1.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        CType(NUDCantidad, ComponentModel.ISupportInitialize).EndInit()
        Panel2.ResumeLayout(False)
        TableLayoutPanel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        CType(DGVDetalleVenta, ComponentModel.ISupportInitialize).EndInit()
        Panel4.ResumeLayout(False)
        TableLayoutPanel3.ResumeLayout(False)
        TableLayoutPanel3.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents BSuspender As Button
    Friend WithEvents BEditar As Button
    Friend WithEvents LVendedor As Label
    Friend WithEvents BAgregar As Button
    Friend WithEvents LNombre As Label
    Friend WithEvents LNombreCliente As Label
    Friend WithEvents TBDNICliente As TextBox
    Friend WithEvents LVendedorID As Label
    Friend WithEvents LNombreVend As Label
    Friend WithEvents LDNICliente As Label
    Friend WithEvents LNombreClienteConf As Label
    Friend WithEvents LNuevoCliente As Label
    Friend WithEvents BCrearCliente As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents LCodBarra As Label
    Friend WithEvents LCantidad As Label
    Friend WithEvents TBCodBarra As TextBox
    Friend WithEvents NUDCantidad As NumericUpDown
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents BCancelar As Button
    Friend WithEvents BAgregarProducto As Button
    Friend WithEvents BConfirmarVenta As Button
    Friend WithEvents Panel3 As Panel
    Friend WithEvents DGVDetalleVenta As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel
    Friend WithEvents LTotalTexto As Label
    Friend WithEvents LTotal As Label
End Class
