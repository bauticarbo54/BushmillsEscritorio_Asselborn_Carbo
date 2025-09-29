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
        LNombre = New Label()
        TBNombre = New TextBox()
        LRol = New Label()
        CBRol = New ComboBox()
        LContrasena = New Label()
        TBContrasena = New TextBox()
        LUsuario = New Label()
        TBUsuario = New TextBox()
        LApellido = New Label()
        TBApellido = New TextBox()
        BSuspender = New Button()
        BEditar = New Button()
        BAgregar = New Button()
        Panel2 = New Panel()
        Panel3 = New Panel()
        DGVUsuarios = New DataGridView()
        Panel1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        Panel3.SuspendLayout()
        CType(DGVUsuarios, ComponentModel.ISupportInitialize).BeginInit()
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
        Panel1.Size = New Size(800, 188)
        Panel1.TabIndex = 1
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.BackColor = Color.LightGray
        TableLayoutPanel1.ColumnCount = 4
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20.0250931F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 29.7365112F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 20.2007523F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 30.0376415F))
        TableLayoutPanel1.Controls.Add(LNombre, 0, 0)
        TableLayoutPanel1.Controls.Add(TBNombre, 1, 0)
        TableLayoutPanel1.Controls.Add(LRol, 0, 4)
        TableLayoutPanel1.Controls.Add(CBRol, 1, 4)
        TableLayoutPanel1.Controls.Add(LContrasena, 0, 3)
        TableLayoutPanel1.Controls.Add(TBContrasena, 1, 3)
        TableLayoutPanel1.Controls.Add(LUsuario, 0, 2)
        TableLayoutPanel1.Controls.Add(TBUsuario, 1, 2)
        TableLayoutPanel1.Controls.Add(LApellido, 0, 1)
        TableLayoutPanel1.Controls.Add(TBApellido, 1, 1)
        TableLayoutPanel1.Controls.Add(BSuspender, 3, 3)
        TableLayoutPanel1.Controls.Add(BEditar, 3, 2)
        TableLayoutPanel1.Controls.Add(BAgregar, 3, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 5
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 20.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 20.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 20.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 20.0F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 20.0F))
        TableLayoutPanel1.Size = New Size(798, 186)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' LNombre
        ' 
        LNombre.Anchor = AnchorStyles.Right
        LNombre.AutoSize = True
        LNombre.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        LNombre.Location = New Point(85, 8)
        LNombre.Name = "LNombre"
        LNombre.Size = New Size(71, 20)
        LNombre.TabIndex = 0
        LNombre.Text = "Nombre:"
        ' 
        ' TBNombre
        ' 
        TBNombre.Location = New Point(162, 3)
        TBNombre.Name = "TBNombre"
        TBNombre.Size = New Size(178, 27)
        TBNombre.TabIndex = 3
        ' 
        ' LRol
        ' 
        LRol.Anchor = AnchorStyles.Right
        LRol.AutoSize = True
        LRol.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        LRol.Location = New Point(120, 157)
        LRol.Name = "LRol"
        LRol.Size = New Size(36, 20)
        LRol.TabIndex = 7
        LRol.Text = "Rol:"
        ' 
        ' CBRol
        ' 
        CBRol.Anchor = AnchorStyles.Left
        CBRol.FormattingEnabled = True
        CBRol.Location = New Point(162, 153)
        CBRol.Name = "CBRol"
        CBRol.Size = New Size(151, 28)
        CBRol.TabIndex = 8
        ' 
        ' LContrasena
        ' 
        LContrasena.Anchor = AnchorStyles.Right
        LContrasena.AutoSize = True
        LContrasena.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        LContrasena.Location = New Point(64, 119)
        LContrasena.Name = "LContrasena"
        LContrasena.Size = New Size(92, 20)
        LContrasena.TabIndex = 1
        LContrasena.Text = "Contraseña:"
        ' 
        ' TBContrasena
        ' 
        TBContrasena.Location = New Point(162, 114)
        TBContrasena.Name = "TBContrasena"
        TBContrasena.PasswordChar = "*"c
        TBContrasena.Size = New Size(178, 27)
        TBContrasena.TabIndex = 6
        ' 
        ' LUsuario
        ' 
        LUsuario.Anchor = AnchorStyles.Right
        LUsuario.AutoSize = True
        LUsuario.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        LUsuario.Location = New Point(89, 82)
        LUsuario.Name = "LUsuario"
        LUsuario.Size = New Size(67, 20)
        LUsuario.TabIndex = 2
        LUsuario.Text = "Usuario:"
        ' 
        ' TBUsuario
        ' 
        TBUsuario.Location = New Point(162, 77)
        TBUsuario.Name = "TBUsuario"
        TBUsuario.Size = New Size(178, 27)
        TBUsuario.TabIndex = 5
        ' 
        ' LApellido
        ' 
        LApellido.Anchor = AnchorStyles.Right
        LApellido.AutoSize = True
        LApellido.Font = New Font("Segoe UI", 9.0F, FontStyle.Bold)
        LApellido.Location = New Point(85, 45)
        LApellido.Name = "LApellido"
        LApellido.Size = New Size(71, 20)
        LApellido.TabIndex = 9
        LApellido.Text = "Apellido:"
        ' 
        ' TBApellido
        ' 
        TBApellido.Location = New Point(162, 40)
        TBApellido.Name = "TBApellido"
        TBApellido.Size = New Size(178, 27)
        TBApellido.TabIndex = 10
        ' 
        ' BSuspender
        ' 
        BSuspender.Anchor = AnchorStyles.None
        BSuspender.Location = New Point(630, 114)
        BSuspender.Name = "BSuspender"
        BSuspender.Size = New Size(94, 31)
        BSuspender.TabIndex = 2
        BSuspender.Text = "Suspender"
        BSuspender.UseVisualStyleBackColor = True
        ' 
        ' BEditar
        ' 
        BEditar.Anchor = AnchorStyles.None
        BEditar.Location = New Point(630, 77)
        BEditar.Name = "BEditar"
        BEditar.Size = New Size(94, 31)
        BEditar.TabIndex = 1
        BEditar.Text = "Editar"
        BEditar.UseVisualStyleBackColor = True
        ' 
        ' BAgregar
        ' 
        BAgregar.Anchor = AnchorStyles.None
        BAgregar.Location = New Point(630, 40)
        BAgregar.Name = "BAgregar"
        BAgregar.Size = New Size(94, 31)
        BAgregar.TabIndex = 0
        BAgregar.Text = "Agregar"
        BAgregar.UseVisualStyleBackColor = True
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(Panel3)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 188)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(800, 262)
        Panel2.TabIndex = 2
        ' 
        ' Panel3
        ' 
        Panel3.BackColor = SystemColors.ControlDark
        Panel3.Controls.Add(DGVUsuarios)
        Panel3.Dock = DockStyle.Fill
        Panel3.Location = New Point(0, 0)
        Panel3.Name = "Panel3"
        Panel3.Size = New Size(800, 262)
        Panel3.TabIndex = 0
        ' 
        ' DGVUsuarios
        ' 
        DGVUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DGVUsuarios.Dock = DockStyle.Fill
        DGVUsuarios.Location = New Point(0, 0)
        DGVUsuarios.Name = "DGVUsuarios"
        DGVUsuarios.RowHeadersWidth = 51
        DGVUsuarios.Size = New Size(800, 262)
        DGVUsuarios.TabIndex = 1
        ' 
        ' FormUsuarios
        ' 
        AutoScaleDimensions = New SizeF(8.0F, 20.0F)
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
    Friend WithEvents LApellido As Label
    Friend WithEvents TBApellido As TextBox
End Class
