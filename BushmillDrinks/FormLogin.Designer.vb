<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormLogin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        LUsuario = New Label()
        TBContrasenia = New TextBox()
        BSalir = New Button()
        LContrasenia = New Label()
        TBUsuario = New TextBox()
        BIngresar = New Button()
        TableLayoutPanel1 = New TableLayoutPanel()
        Panel1 = New Panel()
        TableLayoutPanel3 = New TableLayoutPanel()
        TableLayoutPanel1.SuspendLayout()
        Panel1.SuspendLayout()
        TableLayoutPanel3.SuspendLayout()
        SuspendLayout()
        ' 
        ' LUsuario
        ' 
        LUsuario.AutoSize = True
        LUsuario.Dock = DockStyle.Fill
        LUsuario.Font = New Font("Algerian", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LUsuario.Location = New Point(3, 0)
        LUsuario.Name = "LUsuario"
        LUsuario.Size = New Size(154, 68)
        LUsuario.TabIndex = 0
        LUsuario.Text = "Usuario"
        LUsuario.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TBContrasenia
        ' 
        TBContrasenia.Anchor = AnchorStyles.None
        TBContrasenia.Location = New Point(163, 88)
        TBContrasenia.Name = "TBContrasenia"
        TBContrasenia.Size = New Size(154, 27)
        TBContrasenia.TabIndex = 3
        TBContrasenia.UseSystemPasswordChar = True
        ' 
        ' BSalir
        ' 
        BSalir.Dock = DockStyle.Fill
        BSalir.Font = New Font("Algerian", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        BSalir.Location = New Point(163, 139)
        BSalir.Name = "BSalir"
        BSalir.Size = New Size(154, 67)
        BSalir.TabIndex = 5
        BSalir.Text = "Salir"
        BSalir.UseVisualStyleBackColor = True
        ' 
        ' LContrasenia
        ' 
        LContrasenia.AutoSize = True
        LContrasenia.Dock = DockStyle.Fill
        LContrasenia.Font = New Font("Algerian", 12F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        LContrasenia.Location = New Point(3, 68)
        LContrasenia.Name = "LContrasenia"
        LContrasenia.Size = New Size(154, 68)
        LContrasenia.TabIndex = 2
        LContrasenia.Text = "Contraseña"
        LContrasenia.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' TBUsuario
        ' 
        TBUsuario.Anchor = AnchorStyles.None
        TBUsuario.Location = New Point(163, 20)
        TBUsuario.Name = "TBUsuario"
        TBUsuario.Size = New Size(154, 27)
        TBUsuario.TabIndex = 1
        ' 
        ' BIngresar
        ' 
        BIngresar.Dock = DockStyle.Fill
        BIngresar.Font = New Font("Algerian", 9F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        BIngresar.Location = New Point(3, 139)
        BIngresar.Name = "BIngresar"
        BIngresar.Size = New Size(154, 67)
        BIngresar.TabIndex = 4
        BIngresar.Text = "Ingresar"
        BIngresar.UseVisualStyleBackColor = True
        ' 
        ' TableLayoutPanel1
        ' 
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 34F))
        TableLayoutPanel1.Controls.Add(Panel1, 1, 1)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 3
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 33F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 33F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 34F))
        TableLayoutPanel1.Size = New Size(988, 654)
        TableLayoutPanel1.TabIndex = 6
        ' 
        ' Panel1
        ' 
        Panel1.Controls.Add(TableLayoutPanel3)
        Panel1.Dock = DockStyle.Fill
        Panel1.Location = New Point(329, 218)
        Panel1.Name = "Panel1"
        Panel1.Size = New Size(320, 209)
        Panel1.TabIndex = 0
        ' 
        ' TableLayoutPanel3
        ' 
        TableLayoutPanel3.ColumnCount = 2
        TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel3.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50F))
        TableLayoutPanel3.Controls.Add(BSalir, 1, 2)
        TableLayoutPanel3.Controls.Add(LUsuario, 0, 0)
        TableLayoutPanel3.Controls.Add(BIngresar, 0, 2)
        TableLayoutPanel3.Controls.Add(LContrasenia, 0, 1)
        TableLayoutPanel3.Controls.Add(TBContrasenia, 1, 1)
        TableLayoutPanel3.Controls.Add(TBUsuario, 1, 0)
        TableLayoutPanel3.Dock = DockStyle.Fill
        TableLayoutPanel3.Location = New Point(0, 0)
        TableLayoutPanel3.Name = "TableLayoutPanel3"
        TableLayoutPanel3.RowCount = 3
        TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Percent, 33F))
        TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Percent, 33F))
        TableLayoutPanel3.RowStyles.Add(New RowStyle(SizeType.Percent, 34F))
        TableLayoutPanel3.Size = New Size(320, 209)
        TableLayoutPanel3.TabIndex = 0
        ' 
        ' FormLogin
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.LightGray
        ClientSize = New Size(988, 654)
        Controls.Add(TableLayoutPanel1)
        Name = "FormLogin"
        Text = "Inicio Sesion"
        WindowState = FormWindowState.Maximized
        TableLayoutPanel1.ResumeLayout(False)
        Panel1.ResumeLayout(False)
        TableLayoutPanel3.ResumeLayout(False)
        TableLayoutPanel3.PerformLayout()
        ResumeLayout(False)
    End Sub
    Friend WithEvents LUsuario As Label
    Friend WithEvents TBContrasenia As TextBox
    Friend WithEvents BSalir As Button
    Friend WithEvents LContrasenia As Label
    Friend WithEvents TBUsuario As TextBox
    Friend WithEvents BIngresar As Button
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel3 As TableLayoutPanel

End Class
