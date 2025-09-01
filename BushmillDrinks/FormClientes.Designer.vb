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
        LTituloClientes = New Label()
        Panel2 = New Panel()
        Panel1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
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
        TableLayoutPanel1.ColumnCount = 3
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 33.3333321F))
        TableLayoutPanel1.Controls.Add(LTituloClientes, 1, 0)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 1
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 100F))
        TableLayoutPanel1.Size = New Size(798, 123)
        TableLayoutPanel1.TabIndex = 0
        ' 
        ' LTituloClientes
        ' 
        LTituloClientes.Anchor = AnchorStyles.None
        LTituloClientes.AutoSize = True
        LTituloClientes.Font = New Font("Algerian", 16.2F, FontStyle.Bold, GraphicsUnit.Point, CByte(0))
        LTituloClientes.Location = New Point(311, 30)
        LTituloClientes.Name = "LTituloClientes"
        LTituloClientes.RightToLeft = RightToLeft.No
        LTituloClientes.Size = New Size(173, 62)
        LTituloClientes.TabIndex = 3
        LTituloClientes.Text = "GESTIONAR CLIENTES"
        LTituloClientes.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' Panel2
        ' 
        Panel2.BackColor = Color.LightGray
        Panel2.Dock = DockStyle.Fill
        Panel2.Location = New Point(0, 125)
        Panel2.Name = "Panel2"
        Panel2.Size = New Size(800, 325)
        Panel2.TabIndex = 1
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
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LTituloClientes As Label
    Friend WithEvents Panel2 As Panel
End Class
