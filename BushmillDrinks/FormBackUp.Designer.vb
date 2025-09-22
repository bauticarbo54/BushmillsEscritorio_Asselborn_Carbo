<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FormBackUp
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
        LDb = New Label()
        CBDb = New ComboBox()
        BBackup = New Button()
        BRestore = New Button()
        PB = New ProgressBar()
        LStatus = New Label()
        Panel1.SuspendLayout()
        TableLayoutPanel1.SuspendLayout()
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
        Panel1.Size = New Size(800, 153)
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
        TableLayoutPanel1.Controls.Add(LDb, 0, 0)
        TableLayoutPanel1.Controls.Add(CBDb, 1, 0)
        TableLayoutPanel1.Controls.Add(BBackup, 0, 1)
        TableLayoutPanel1.Controls.Add(BRestore, 2, 1)
        TableLayoutPanel1.Controls.Add(PB, 1, 2)
        TableLayoutPanel1.Controls.Add(LStatus, 0, 3)
        TableLayoutPanel1.Dock = DockStyle.Fill
        TableLayoutPanel1.Location = New Point(0, 0)
        TableLayoutPanel1.Name = "TableLayoutPanel1"
        TableLayoutPanel1.RowCount = 4
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.Percent, 25F))
        TableLayoutPanel1.Size = New Size(798, 151)
        TableLayoutPanel1.TabIndex = 2
        ' 
        ' LDb
        ' 
        LDb.Anchor = AnchorStyles.Right
        LDb.AutoSize = True
        LDb.Location = New Point(51, 8)
        LDb.Name = "LDb"
        LDb.Size = New Size(105, 20)
        LDb.TabIndex = 2
        LDb.Text = "Base de datos:"
        ' 
        ' CBDb
        ' 
        CBDb.Anchor = AnchorStyles.Left
        CBDb.FormattingEnabled = True
        CBDb.Location = New Point(162, 3)
        CBDb.Name = "CBDb"
        CBDb.Size = New Size(151, 28)
        CBDb.TabIndex = 7
        ' 
        ' BBackup
        ' 
        BBackup.Anchor = AnchorStyles.None
        BBackup.Location = New Point(17, 41)
        BBackup.Name = "BBackup"
        BBackup.Size = New Size(124, 29)
        BBackup.TabIndex = 8
        BBackup.Text = "Crear Backup"
        BBackup.UseVisualStyleBackColor = True
        ' 
        ' BRestore
        ' 
        BRestore.Anchor = AnchorStyles.None
        BRestore.Location = New Point(415, 41)
        BRestore.Name = "BRestore"
        BRestore.Size = New Size(124, 29)
        BRestore.TabIndex = 9
        BRestore.Text = "Restaurar"
        BRestore.UseVisualStyleBackColor = True
        ' 
        ' PB
        ' 
        PB.Location = New Point(162, 77)
        PB.Name = "PB"
        PB.Size = New Size(125, 29)
        PB.TabIndex = 10
        ' 
        ' LStatus
        ' 
        LStatus.Anchor = AnchorStyles.Right
        LStatus.AutoSize = True
        LStatus.Location = New Point(99, 121)
        LStatus.Name = "LStatus"
        LStatus.Size = New Size(57, 20)
        LStatus.TabIndex = 11
        LStatus.Text = "Estado:"
        ' 
        ' FormBackUp
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.ControlDark
        ClientSize = New Size(800, 450)
        Controls.Add(Panel1)
        Name = "FormBackUp"
        Text = "Back Up"
        Panel1.ResumeLayout(False)
        TableLayoutPanel1.ResumeLayout(False)
        TableLayoutPanel1.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents LDb As Label
    Friend WithEvents CBDb As ComboBox
    Friend WithEvents BBackup As Button
    Friend WithEvents BRestore As Button
    Friend WithEvents PB As ProgressBar
    Friend WithEvents LStatus As Label
End Class
