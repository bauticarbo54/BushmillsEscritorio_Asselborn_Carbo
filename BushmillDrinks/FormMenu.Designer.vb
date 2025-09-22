<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FormMenu))
        POperaciones = New Panel()
        TableLayoutPanel1 = New TableLayoutPanel()
        BSalir = New Button()
        BUsuarios = New Button()
        BReportes = New Button()
        BBuckup = New Button()
        BProductos = New Button()
        BVentas = New Button()
        BClientes = New Button()
        PanelGeneral = New Panel()
        POperaciones.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        SuspendLayout()
        ' 
        ' POperaciones
        ' 
        POperaciones.BackColor = Color.Gray
        POperaciones.Controls.Add(TableLayoutPanel1)
        POperaciones.Dock = DockStyle.Top
        POperaciones.Location = New Point(0, 0)
        POperaciones.Name = "POperaciones"
        POperaciones.Size = New Size(929, 125)
        POperaciones.TabIndex = 0
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 7
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857141F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857141F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857141F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857141F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857141F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857141F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 14.2857141F))
        TableLayoutPanel1.Controls.Add(BSalir, 6, 0)
        TableLayoutPanel1.Controls.Add(BUsuarios, 0, 0)
        TableLayoutPanel1.Controls.Add(BReportes, 5, 0)
        TableLayoutPanel1.Controls.Add(BBuckup, 1, 0)
        TableLayoutPanel1.Controls.Add(BProductos, 4, 0)
        TableLayoutPanel1.Controls.Add(BVentas, 2, 0)
        TableLayoutPanel1.Controls.Add(BClientes, 3, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(929, 125)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' BSalir
        ' 
        BSalir.BackgroundImage = My.Resources.Resources.iconoSalir
        BSalir.BackgroundImageLayout = ImageLayout.Zoom
        BSalir.Dock = DockStyle.Fill
        BSalir.FlatStyle = FlatStyle.Flat
        BSalir.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BSalir.ForeColor = Color.White
        BSalir.ImageAlign = ContentAlignment.TopCenter
        BSalir.Location = New Point(795, 3)
        BSalir.Name = "BSalir"
        BSalir.Size = New Size(131, 119)
        BSalir.TabIndex = 6
        BSalir.Text = "SALIR"
        BSalir.TextAlign = ContentAlignment.BottomCenter
        BSalir.TextImageRelation = TextImageRelation.ImageAboveText
        BSalir.UseVisualStyleBackColor = True
        ' 
        ' BUsuarios
        ' 
        BUsuarios.BackgroundImage = My.Resources.Resources.iconoUsuarios
        BUsuarios.BackgroundImageLayout = ImageLayout.Zoom
        BUsuarios.Dock = DockStyle.Fill
        BUsuarios.FlatStyle = FlatStyle.Flat
        BUsuarios.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BUsuarios.ForeColor = Color.White
        BUsuarios.ImageAlign = ContentAlignment.TopCenter
        BUsuarios.Location = New Point(3, 3)
        BUsuarios.Name = "BUsuarios"
        BUsuarios.Size = New Size(126, 119)
        BUsuarios.TabIndex = 0
        BUsuarios.Text = "USUARIOS"
        BUsuarios.TextAlign = ContentAlignment.BottomCenter
        BUsuarios.TextImageRelation = TextImageRelation.ImageAboveText
        BUsuarios.UseVisualStyleBackColor = True
        ' 
        ' BReportes
        ' 
        BReportes.BackgroundImage = My.Resources.Resources.iconoReportes
        BReportes.BackgroundImageLayout = ImageLayout.Zoom
        BReportes.Dock = DockStyle.Fill
        BReportes.FlatStyle = FlatStyle.Flat
        BReportes.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BReportes.ForeColor = Color.White
        BReportes.ImageAlign = ContentAlignment.TopCenter
        BReportes.Location = New Point(663, 3)
        BReportes.Name = "BReportes"
        BReportes.Size = New Size(126, 119)
        BReportes.TabIndex = 5
        BReportes.Text = "REPORTES"
        BReportes.TextAlign = ContentAlignment.BottomCenter
        BReportes.TextImageRelation = TextImageRelation.ImageAboveText
        BReportes.UseVisualStyleBackColor = True
        ' 
        ' BBuckup
        ' 
        BBuckup.BackgroundImage = My.Resources.Resources.iconoBackup
        BBuckup.BackgroundImageLayout = ImageLayout.Zoom
        BBuckup.Dock = DockStyle.Fill
        BBuckup.FlatStyle = FlatStyle.Flat
        BBuckup.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BBuckup.ForeColor = Color.White
        BBuckup.ImageAlign = ContentAlignment.TopCenter
        BBuckup.Location = New Point(135, 3)
        BBuckup.Name = "BBuckup"
        BBuckup.Size = New Size(126, 119)
        BBuckup.TabIndex = 1
        BBuckup.Text = "BACK UP"
        BBuckup.TextAlign = ContentAlignment.BottomCenter
        BBuckup.TextImageRelation = TextImageRelation.ImageAboveText
        BBuckup.UseVisualStyleBackColor = True
        ' 
        ' BProductos
        ' 
        BProductos.BackgroundImage = My.Resources.Resources.iconoProductos
        BProductos.BackgroundImageLayout = ImageLayout.Zoom
        BProductos.Dock = DockStyle.Fill
        BProductos.FlatStyle = FlatStyle.Flat
        BProductos.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BProductos.ForeColor = Color.White
        BProductos.ImageAlign = ContentAlignment.TopCenter
        BProductos.Location = New Point(531, 3)
        BProductos.Name = "BProductos"
        BProductos.Size = New Size(126, 119)
        BProductos.TabIndex = 4
        BProductos.Text = "PRODUCTOS"
        BProductos.TextAlign = ContentAlignment.BottomCenter
        BProductos.TextImageRelation = TextImageRelation.ImageAboveText
        BProductos.UseVisualStyleBackColor = True
        ' 
        ' BVentas
        ' 
        BVentas.BackgroundImage = My.Resources.Resources.iconoFinanzas
        BVentas.BackgroundImageLayout = ImageLayout.Zoom
        BVentas.Dock = DockStyle.Fill
        BVentas.FlatStyle = FlatStyle.Flat
        BVentas.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BVentas.ForeColor = Color.White
        BVentas.ImageAlign = ContentAlignment.TopCenter
        BVentas.Location = New Point(267, 3)
        BVentas.Name = "BVentas"
        BVentas.Size = New Size(126, 119)
        BVentas.TabIndex = 2
        BVentas.Text = "VENTAS"
        BVentas.TextAlign = ContentAlignment.BottomCenter
        BVentas.TextImageRelation = TextImageRelation.ImageAboveText
        BVentas.UseVisualStyleBackColor = True
        ' 
        ' BClientes
        ' 
        BClientes.BackgroundImage = My.Resources.Resources.iconoClientes
        BClientes.BackgroundImageLayout = ImageLayout.Zoom
        BClientes.Dock = DockStyle.Fill
        BClientes.FlatStyle = FlatStyle.Flat
        BClientes.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        BClientes.ForeColor = Color.White
        BClientes.ImageAlign = ContentAlignment.TopCenter
        BClientes.Location = New Point(399, 3)
        BClientes.Name = "BClientes"
        BClientes.Size = New Size(126, 119)
        BClientes.TabIndex = 3
        BClientes.Text = "CLIENTES"
        BClientes.TextAlign = ContentAlignment.BottomCenter
        BClientes.TextImageRelation = TextImageRelation.ImageAboveText
        BClientes.UseVisualStyleBackColor = True
        ' 
        ' PanelGeneral
        ' 
        PanelGeneral.AutoSize = True
        PanelGeneral.BackColor = Color.LightGray
        PanelGeneral.Dock = DockStyle.Fill
        PanelGeneral.Location = New Point(0, 125)
        PanelGeneral.Name = "PanelGeneral"
        PanelGeneral.Size = New Size(929, 325)
        PanelGeneral.TabIndex = 1
        ' 
        ' FormMenu
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(929, 450)
        Controls.Add(PanelGeneral)
        Controls.Add(POperaciones)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "FormMenu"
        Text = "Menú"
        WindowState = FormWindowState.Maximized
        POperaciones.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents POperaciones As Panel
    Friend WithEvents PanelGeneral As Panel
    Friend WithEvents BUsuarios As Button
    Friend WithEvents BSalir As Button
    Friend WithEvents BReportes As Button
    Friend WithEvents BProductos As Button
    Friend WithEvents BClientes As Button
    Friend WithEvents BVentas As Button
    Friend WithEvents BBuckup As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
End Class
