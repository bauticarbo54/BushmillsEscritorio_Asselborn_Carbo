<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormReportes
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
        BActualizar = New Button()
        LFiltrar = New Label()
        DPDesde = New DateTimePicker()
        DPHasta = New DateTimePicker()
        Panel2 = New Panel()
        Panel3 = New Panel()
        TableLayoutPanel2 = New TableLayoutPanel()
        TC = New TabControl()
        TPGerencia = New TabPage()
        DGVGerencia = New DataGridView()
        TPAdmin = New TabPage()
        DGVAdmin = New DataGridView()
        TPVendedor = New TabPage()
        DGVVendedor = New DataGridView()
        Panel1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        TC.SuspendLayout()
        TPGerencia.SuspendLayout()
        CType(DGVGerencia, ComponentModel.ISupportInitialize).BeginInit()
        TPAdmin.SuspendLayout()
        CType(DGVAdmin, ComponentModel.ISupportInitialize).BeginInit()
        TPVendedor.SuspendLayout()
        CType(DGVVendedor, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.LightGray
        Panel1.BorderStyle = BorderStyle.FixedSingle
        Panel1.Controls.Add(TableLayoutPanel1)
        Panel1.Dock = DockStyle.Top
        Panel1.Location = New Point(0, 0)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(800, 125)
        Panel1.TabIndex = 0
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.BackColor = Color.LightGray
        TableLayoutPanel1.ColumnCount = 4
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.Controls.Add(BActualizar, 3, 0)
        TableLayoutPanel1.Controls.Add(LFiltrar, 0, 0)
        TableLayoutPanel1.Controls.Add(DPDesde, 1, 0)
        TableLayoutPanel1.Controls.Add(DPHasta, 2, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.Size = New Size(798, 123)
        TableLayoutPanel1.TabIndex = 2
        ' 
        ' BActualizar
        ' 
        BActualizar.Anchor = AnchorStyles.None
        BActualizar.Location = New Point(650, 47)
        BActualizar.Name = "BActualizar"
        BActualizar.Size = New Size(94, 29)
        BActualizar.TabIndex = 0
        BActualizar.Text = "Actualizar"
        BActualizar.UseVisualStyleBackColor = True
        ' 
        ' LFiltrar
        ' 
        LFiltrar.Anchor = AnchorStyles.Right
        LFiltrar.AutoSize = True
        LFiltrar.Location = New Point(102, 51)
        LFiltrar.Name = "LFiltrar"
        LFiltrar.Size = New Size(94, 20)
        LFiltrar.TabIndex = 2
        LFiltrar.Text = "Filtrar desde:"
        ' 
        ' DPDesde
        ' 
        DPDesde.Anchor = AnchorStyles.Left
        DPDesde.Location = New Point(202, 48)
        DPDesde.Name = "DPDesde"
        DPDesde.Size = New Size(193, 27)
        DPDesde.TabIndex = 7
        ' 
        ' DPHasta
        ' 
        DPHasta.Anchor = AnchorStyles.Left
        DPHasta.Location = New Point(401, 48)
        DPHasta.Name = "DPHasta"
        DPHasta.Size = New Size(193, 27)
        DPHasta.TabIndex = 8
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = SystemColors.ControlDark
        Panel2.Controls.Add(Panel3)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 125)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(800, 325)
        Panel2.TabIndex = 1
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = Color.LightGray
        Panel3.Controls.Add(TableLayoutPanel2)
        Panel3.Dock = DockStyle.Top
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(800, 125)
        Panel3.TabIndex = 10
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 1
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.Controls.Add(TC, 0, 0)
        TableLayoutPanel2.Dock = DockStyle.Fill
        TableLayoutPanel2.Location = New Point(0, 0)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 1
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.Size = New Size(800, 125)
        TableLayoutPanel2.TabIndex = 10
        ' 
        ' TC
        ' 
        TC.Controls.Add(TPGerencia)
        TC.Controls.Add(TPAdmin)
        TC.Controls.Add(TPVendedor)
        TC.Dock = DockStyle.Fill
        TC.Location = New Point(3, 3)
        TC.Name = "TC"
        TC.SelectedIndex = 0
        TC.Size = New Size(794, 119)
        TC.TabIndex = 0
        ' 
        ' TPGerencia
        ' 
        TPGerencia.Controls.Add(DGVGerencia)
        TPGerencia.Location = New Point(4, 29)
        TPGerencia.Name = "TPGerencia"
        TPGerencia.Padding = New Padding(3)
        TPGerencia.Size = New Size(786, 86)
        TPGerencia.TabIndex = 0
        TPGerencia.Text = "Gerencia"
        TPGerencia.UseVisualStyleBackColor = True
        ' 
        ' DGVGerencia
        ' 
        DGVGerencia.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVGerencia.Dock = DockStyle.Fill
        DGVGerencia.Location = New Point(3, 3)
        DGVGerencia.Name = "DGVGerencia"
        DGVGerencia.RowHeadersWidth = 51
        DGVGerencia.Size = New Size(780, 80)
        DGVGerencia.TabIndex = 0
        ' 
        ' TPAdmin
        ' 
        TPAdmin.Controls.Add(DGVAdmin)
        TPAdmin.Location = New Point(4, 29)
        TPAdmin.Name = "TPAdmin"
        TPAdmin.Padding = New Padding(3)
        TPAdmin.Size = New Size(786, 86)
        TPAdmin.TabIndex = 1
        TPAdmin.Text = "Administracion"
        TPAdmin.UseVisualStyleBackColor = True
        ' 
        ' DGVAdmin
        ' 
        DGVAdmin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVAdmin.Dock = DockStyle.Fill
        DGVAdmin.Location = New Point(3, 3)
        DGVAdmin.Name = "DGVAdmin"
        DGVAdmin.RowHeadersWidth = 51
        DGVAdmin.Size = New Size(780, 80)
        DGVAdmin.TabIndex = 0
        ' 
        ' TPVendedor
        ' 
        TPVendedor.Controls.Add(DGVVendedor)
        TPVendedor.Location = New Point(4, 29)
        TPVendedor.Name = "TPVendedor"
        TPVendedor.Padding = New Padding(3)
        TPVendedor.Size = New Size(786, 86)
        TPVendedor.TabIndex = 2
        TPVendedor.Text = "Vendedor"
        TPVendedor.UseVisualStyleBackColor = True
        ' 
        ' DGVVendedor
        ' 
        DGVVendedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVVendedor.Dock = DockStyle.Fill
        DGVVendedor.Location = New Point(3, 3)
        DGVVendedor.Name = "DGVVendedor"
        DGVVendedor.RowHeadersWidth = 51
        DGVVendedor.Size = New Size(780, 80)
        DGVVendedor.TabIndex = 0
        ' 
        ' FormReportes
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "FormReportes"
        Text = "FormReportes"
        Panel1.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        TableLayoutPanel2.ResumeLayout(False)
        TC.ResumeLayout(False)
        TPGerencia.ResumeLayout(False)
        CType(DGVGerencia, ComponentModel.ISupportInitialize).EndInit()
        TPAdmin.ResumeLayout(False)
        CType(DGVAdmin, ComponentModel.ISupportInitialize).EndInit()
        TPVendedor.ResumeLayout(False)
        CType(DGVVendedor, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents BActualizar As Button
    Friend WithEvents LFiltrar As Label
    Friend WithEvents DPDesde As DateTimePicker
    Friend WithEvents DPHasta As DateTimePicker
    Friend WithEvents Panel3 As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents TC As TabControl
    Friend WithEvents TPGerencia As TabPage
    Friend WithEvents TPAdmin As TabPage
    Friend WithEvents DGVGerencia As DataGridView
    Friend WithEvents TPVendedor As TabPage
    Friend WithEvents DGVAdmin As DataGridView
    Friend WithEvents DGVVendedor As DataGridView
End Class
