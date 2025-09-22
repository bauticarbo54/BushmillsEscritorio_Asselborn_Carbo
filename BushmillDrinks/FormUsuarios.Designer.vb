<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormUsuarios
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
        TBUsuario = New TextBox()
        BEditar = New Button()
        LNombre = New Label()
        BAgregar = New Button()
        LUsuario = New Label()
        TBNombre = New TextBox()
        LContrasena = New Label()
        TBContrasena = New TextBox()
        CBRol = New ComboBox()
        LRol = New Label()
        Panel2 = New Panel()
        Panel3 = New Panel()
        DGVUsuarios = New DataGridView()
        Panel4 = New Panel()
        TableLayoutPanel2 = New TableLayoutPanel()
        Panel1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        CType(DGVUsuarios, ComponentModel.ISupportInitialize).BeginInit()
        Panel4.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
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
        Panel1.TabIndex = 1
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
        TableLayoutPanel1.Controls.Add(TBUsuario, 1, 1)
        TableLayoutPanel1.Controls.Add(BEditar, 3, 1)
        TableLayoutPanel1.Controls.Add(LNombre, 0, 0)
        TableLayoutPanel1.Controls.Add(BAgregar, 3, 0)
        TableLayoutPanel1.Controls.Add(LUsuario, 0, 1)
        TableLayoutPanel1.Controls.Add(TBNombre, 1, 0)
        TableLayoutPanel1.Controls.Add(LContrasena, 0, 2)
        TableLayoutPanel1.Controls.Add(TBContrasena, 1, 2)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.Size = New Size(798, 123)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' BSuspender
        ' 
        BSuspender.Anchor = AnchorStyles.None
        BSuspender.Location = New Point(630, 86)
        BSuspender.Name = "BSuspender"
        BSuspender.Size = New Size(94, 33)
        BSuspender.TabIndex = 2
        BSuspender.Text = "Suspender"
        BSuspender.UseVisualStyleBackColor = True
        ' 
        ' TBUsuario
        ' 
        TBUsuario.Location = New Point(162, 44)
        TBUsuario.Name = "TBUsuario"
        TBUsuario.Size = New Size(178, 27)
        TBUsuario.TabIndex = 5
        ' 
        ' BEditar
        ' 
        BEditar.Anchor = AnchorStyles.None
        BEditar.Location = New Point(630, 44)
        BEditar.Name = "BEditar"
        BEditar.Size = New Size(94, 35)
        BEditar.TabIndex = 1
        BEditar.Text = "Editar"
        BEditar.UseVisualStyleBackColor = True
        ' 
        ' LNombre
        ' 
        LNombre.Anchor = AnchorStyles.Right
        LNombre.AutoSize = True
        LNombre.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LNombre.Location = New Point(85, 10)
        LNombre.Name = "LNombre"
        LNombre.Size = New Size(71, 20)
        LNombre.TabIndex = 0
        LNombre.Text = "Nombre:"
        ' 
        ' BAgregar
        ' 
        BAgregar.Anchor = AnchorStyles.None
        BAgregar.Location = New Point(630, 3)
        BAgregar.Name = "BAgregar"
        BAgregar.Size = New Size(94, 35)
        BAgregar.TabIndex = 0
        BAgregar.Text = "Agregar"
        BAgregar.UseVisualStyleBackColor = True
        ' 
        ' LUsuario
        ' 
        LUsuario.Anchor = AnchorStyles.Right
        LUsuario.AutoSize = True
        LUsuario.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LUsuario.Location = New Point(89, 51)
        LUsuario.Name = "LUsuario"
        LUsuario.Size = New Size(67, 20)
        LUsuario.TabIndex = 2
        LUsuario.Text = "Usuario:"
        ' 
        ' TBNombre
        ' 
        TBNombre.Location = New Point(162, 3)
        TBNombre.Name = "TBNombre"
        TBNombre.Size = New Size(178, 27)
        TBNombre.TabIndex = 3
        ' 
        ' LContrasena
        ' 
        LContrasena.Anchor = AnchorStyles.Right
        LContrasena.AutoSize = True
        LContrasena.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LContrasena.Location = New Point(64, 92)
        LContrasena.Name = "LContrasena"
        LContrasena.Size = New Size(92, 20)
        LContrasena.TabIndex = 1
        LContrasena.Text = "Contraseña:"
        ' 
        ' TBContrasena
        ' 
        TBContrasena.Location = New Point(162, 85)
        TBContrasena.Name = "TBContrasena"
        TBContrasena.PasswordChar = "*"c
        TBContrasena.Size = New Size(178, 27)
        TBContrasena.TabIndex = 6
        ' 
        ' CBRol
        ' 
        CBRol.Anchor = AnchorStyles.Left
        CBRol.FormattingEnabled = True
        CBRol.Location = New Point(403, 16)
        CBRol.Name = "CBRol"
        CBRol.Size = New Size(151, 28)
        CBRol.TabIndex = 8
        ' 
        ' LRol
        ' 
        LRol.Anchor = AnchorStyles.Right
        LRol.AutoSize = True
        LRol.Font = New Font("Segoe UI", 9F, FontStyle.Bold)
        LRol.Location = New Point(361, 20)
        LRol.Name = "LRol"
        LRol.Size = New Size(36, 20)
        LRol.TabIndex = 7
        LRol.Text = "Rol:"
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(Panel3)
        Panel2.Controls.Add(Panel4)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 125)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(800, 325)
        Panel2.TabIndex = 2
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = SystemColors.ControlDark
        Panel3.Controls.Add(DGVUsuarios)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(0, 61)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(800, 264)
        Panel3.TabIndex = 0
        ' 
        ' DGVUsuarios
        ' 
        DGVUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVUsuarios.Dock = DockStyle.Fill
        DGVUsuarios.Location = New Point(0, 0)
        DGVUsuarios.Name = "DGVUsuarios"
        DGVUsuarios.RowHeadersWidth = 51
        DGVUsuarios.Size = New Size(800, 264)
        DGVUsuarios.TabIndex = 1
        ' 
        ' Panel4
        ' 
        Panel4.BackColor = Color.LightGray
        Panel4.Controls.Add(TableLayoutPanel2)
        Panel4.Dock = DockStyle.Top
        Panel4.Location = New Point(0, 0)
        Panel4.Name = "Panel4"
        Panel4.Size = New Size(800, 61)
        Panel4.TabIndex = 3
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 2
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.Controls.Add(LRol, 0, 0)
        TableLayoutPanel2.Controls.Add(CBRol, 1, 0)
        TableLayoutPanel2.Dock = DockStyle.Fill
        TableLayoutPanel2.Location = New Point(0, 0)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 1
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 50F))
        TableLayoutPanel2.Size = New Size(800, 61)
        TableLayoutPanel2.TabIndex = 2
        ' 
        ' FormUsuarios
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.LightGray
        ClientSize = New Size(800, 450)
        Controls.Add(Panel2)
        Controls.Add(Panel1)
        Name = "FormUsuarios"
        Text = "Usuarios"
        Panel1.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        Panel2.ResumeLayout(False)
        Panel3.ResumeLayout(False)
        CType(DGVUsuarios, ComponentModel.ISupportInitialize).EndInit()
        Panel4.ResumeLayout(False)
        TableLayoutPanel2.ResumeLayout(False)
        TableLayoutPanel2.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LNombre As Label
    Friend WithEvents LContrasena As Label
    Friend WithEvents LUsuario As Label
    Friend WithEvents TBNombre As TextBox
    Friend WithEvents TBContrasena As TextBox
    Friend WithEvents TBUsuario As TextBox
    Friend WithEvents LRol As Label
    Friend WithEvents CBRol As ComboBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents BSuspender As Button
    Friend WithEvents BEditar As Button
    Friend WithEvents BAgregar As Button
    Friend WithEvents DGVUsuarios As DataGridView
    Friend WithEvents Panel4 As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
End Class
