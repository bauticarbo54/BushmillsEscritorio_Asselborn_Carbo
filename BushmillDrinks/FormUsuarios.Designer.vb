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
        LTituloUsuarios = New Label()
        Panel1 = New Panel()
        TableLayoutPanel1 = New TableLayoutPanel()
        Panel2 = New Panel()
        TableLayoutPanel2 = New TableLayoutPanel()
        BAgregar = New Button()
        BEditar = New Button()
        BEliminar = New Button()
        Panel1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
        Panel2.SuspendLayout()
        TableLayoutPanel2.SuspendLayout()
        SuspendLayout()
        ' 
        ' LTituloUsuarios
        ' 
        LTituloUsuarios.Anchor = AnchorStyles.None
        LTituloUsuarios.AutoSize = True
        LTituloUsuarios.Font = New Font("Algerian", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LTituloUsuarios.Location = New Point(309, 30)
        LTituloUsuarios.Name = "LTituloUsuarios"
        LTituloUsuarios.RightToLeft = RightToLeft.No
        LTituloUsuarios.Size = New Size(176, 62)
        LTituloUsuarios.TabIndex = 0
        LTituloUsuarios.Text = "Gestion de Usuarios"
        LTituloUsuarios.TextAlign = ContentAlignment.MiddleCenter
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
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.Controls.Add(LTituloUsuarios, 1, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(798, 123)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' Panel2
        ' 
        Panel2.Controls.Add(TableLayoutPanel2)
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 125)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(800, 325)
        Panel2.TabIndex = 2
        ' 
        ' TableLayoutPanel2
        ' 
        TableLayoutPanel2.ColumnCount = 3
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel2.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel2.Controls.Add(BAgregar, 0, 2)
        TableLayoutPanel2.Controls.Add(BEditar, 1, 2)
        TableLayoutPanel2.Controls.Add(BEliminar, 2, 2)
        TableLayoutPanel2.Dock = DockStyle.Fill
        TableLayoutPanel2.Location = New Point(0, 0)
        TableLayoutPanel2.Name = "TableLayoutPanel2"
        TableLayoutPanel2.RowCount = 3
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel2.RowStyles.Add(New RowStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel2.Size = New Size(800, 325)
        TableLayoutPanel2.TabIndex = 1
        ' 
        ' BAgregar
        ' 
        BAgregar.Anchor = AnchorStyles.None
        BAgregar.Location = New Point(86, 256)
        BAgregar.Name = "BAgregar"
        BAgregar.Size = New Size(94, 29)
        BAgregar.TabIndex = 0
        BAgregar.Text = "Agregar"
        BAgregar.UseVisualStyleBackColor = True
        ' 
        ' BEditar
        ' 
        BEditar.Anchor = AnchorStyles.None
        BEditar.Location = New Point(352, 256)
        BEditar.Name = "BEditar"
        BEditar.Size = New Size(94, 29)
        BEditar.TabIndex = 1
        BEditar.Text = "Editar"
        BEditar.UseVisualStyleBackColor = True
        ' 
        ' BEliminar
        ' 
        BEliminar.Anchor = AnchorStyles.None
        BEliminar.Location = New Point(619, 256)
        BEliminar.Name = "BEliminar"
        BEliminar.Size = New Size(94, 29)
        BEliminar.TabIndex = 2
        BEliminar.Text = "Eliminar"
        BEliminar.UseVisualStyleBackColor = True
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
        TableLayoutPanel2.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents LTituloUsuarios As Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents TableLayoutPanel2 As TableLayoutPanel
    Friend WithEvents BAgregar As Button
    Friend WithEvents BEditar As Button
    Friend WithEvents BEliminar As Button
End Class
