<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormClientes
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
        BSuspender = New Button()
        TBDNI = New TextBox()
        BEditar = New Button()
        LNombre = New Label()
        BAgregar = New Button()
        LDNI = New Label()
        TBNombre = New TextBox()
        LTelefono = New Label()
        TBTelefono = New TextBox()
        Panel2 = New Panel()
        Panel3 = New Panel()
        DGVClientes = New DataGridView()
        Panel1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        CType(DGVClientes, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Panel1
        ' 
        Panel1.BackColor = Color.Silver
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
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30F))
        TableLayoutPanel1.Controls.Add(BSuspender, 3, 2)
        TableLayoutPanel1.Controls.Add(BEditar, 3, 1)
        TableLayoutPanel1.Controls.Add(BAgregar, 3, 0)
        TableLayoutPanel1.Controls.Add(LTelefono, 0, 2)
        TableLayoutPanel1.Controls.Add(TBTelefono, 1, 2)
        TableLayoutPanel1.Controls.Add(LDNI, 0, 0)
        TableLayoutPanel1.Controls.Add(TBNombre, 1, 1)
        TableLayoutPanel1.Controls.Add(LNombre, 0, 1)
        TableLayoutPanel1.Controls.Add(TBDNI, 1, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Absolute, 20F))
        TableLayoutPanel1.Size = New Size(798, 123)
        TableLayoutPanel1.TabIndex = 1
        ' 
        ' BSuspender
        ' 
        BSuspender.Anchor = AnchorStyles.None
        BSuspender.Location = New Point(630, 88)
        BSuspender.Name = "BSuspender"
        BSuspender.Size = New Size(94, 29)
        BSuspender.TabIndex = 2
        BSuspender.Text = "Suspender"
        BSuspender.UseVisualStyleBackColor = True
        ' 
        ' TBDNI
        ' 
        TBDNI.Location = New Point(162, 3)
        TBDNI.Name = "TBDNI"
        TBDNI.Size = New Size(178, 27)
        TBDNI.TabIndex = 5
        ' 
        ' BEditar
        ' 
        BEditar.Anchor = AnchorStyles.None
        BEditar.Location = New Point(630, 47)
        BEditar.Name = "BEditar"
        BEditar.Size = New Size(94, 29)
        BEditar.TabIndex = 1
        BEditar.Text = "Editar"
        BEditar.UseVisualStyleBackColor = True
        ' 
        ' LNombre
        ' 
        LNombre.Anchor = AnchorStyles.Right
        LNombre.AutoSize = True
        LNombre.Location = New Point(89, 51)
        LNombre.Name = "LNombre"
        LNombre.Size = New Size(67, 20)
        LNombre.TabIndex = 0
        LNombre.Text = "Nombre:"
        ' 
        ' BAgregar
        ' 
        BAgregar.Anchor = AnchorStyles.None
        BAgregar.Location = New Point(630, 6)
        BAgregar.Name = "BAgregar"
        BAgregar.Size = New Size(94, 29)
        BAgregar.TabIndex = 0
        BAgregar.Text = "Agregar"
        BAgregar.UseVisualStyleBackColor = True
        ' 
        ' LDNI
        ' 
        LDNI.Anchor = AnchorStyles.Right
        LDNI.AutoSize = True
        LDNI.Location = New Point(118, 10)
        LDNI.Name = "LDNI"
        LDNI.Size = New Size(38, 20)
        LDNI.TabIndex = 2
        LDNI.Text = "DNI:"
        ' 
        ' TBNombre
        ' 
        TBNombre.Location = New Point(162, 44)
        TBNombre.Name = "TBNombre"
        TBNombre.Size = New Size(178, 27)
        TBNombre.TabIndex = 3
        ' 
        ' LTelefono
        ' 
        LTelefono.Anchor = AnchorStyles.Right
        LTelefono.AutoSize = True
        LTelefono.Location = New Point(86, 92)
        LTelefono.Name = "LTelefono"
        LTelefono.Size = New Size(70, 20)
        LTelefono.TabIndex = 1
        LTelefono.Text = "Teléfono:"
        ' 
        ' TBTelefono
        ' 
        TBTelefono.Location = New Point(162, 85)
        TBTelefono.Name = "TBTelefono"
        TBTelefono.Size = New Size(178, 27)
        TBTelefono.TabIndex = 6
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.LightGray
        Panel2.Controls.Add(Panel3)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 125)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(800, 325)
        Panel2.TabIndex = 1
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = SystemColors.ControlDark
        Panel3.Controls.Add(DGVClientes)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(800, 325)
        Panel3.TabIndex = 0
        ' 
        ' DGVClientes
        ' 
        DGVClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVClientes.Dock = DockStyle.Fill
        DGVClientes.Location = New Point(0, 0)
        DGVClientes.Name = "DGVClientes"
        DGVClientes.RowHeadersWidth = 51
        DGVClientes.Size = New Size(800, 325)
        DGVClientes.TabIndex = 1
        ' 
        ' FormClientes
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "FormClientes"
        Text = "FormClientes"
        Panel1.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        CType(DGVClientes, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents TBTelefono As TextBox
    Friend WithEvents TBDNI As TextBox
    Friend WithEvents LTelefono As Label
    Friend WithEvents LNombre As Label
    Friend WithEvents LDNI As Label
    Friend WithEvents TBNombre As TextBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents DGVClientes As DataGridView
    Friend WithEvents BSuspender As Button
    Friend WithEvents BEditar As Button
    Friend WithEvents BAgregar As Button
End Class
