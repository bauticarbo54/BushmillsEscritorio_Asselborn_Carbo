Imports System.Configuration
Imports System.Data.SqlClient
Imports System.IO


Public Class FormBackup
    Private ReadOnly ConnStr As String =
        ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

    Private Const DbName As String = "BushmillDrinks"

    Private Sub FormBackup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Text = "Backups - BushmillDrinks"
        PB.Value = 0
        LStatus.Text = ""
        CBDb.DropDownStyle = ComboBoxStyle.DropDownList
        CargarBases()                  ' <<--- IMPORTANTE
    End Sub

    Private Sub CargarBases()
        Try
            ' Conectarse a master por si la DB no existe aún
            Dim csMaster = New SqlConnectionStringBuilder(ConnStr) With {.InitialCatalog = "master"}.ToString()

            Using cn As New SqlConnection(csMaster),
              cmd As New SqlCommand("
                SELECT name
                FROM sys.databases
                WHERE name NOT IN ('master','model','msdb','tempdb')
                ORDER BY name;", cn)

                cn.Open()
                Using rd = cmd.ExecuteReader()
                    CBDb.Items.Clear()
                    While rd.Read()
                        CBDb.Items.Add(rd.GetString(0))
                    End While
                End Using
            End Using

            ' Preseleccionar BushmillDrinks si está
            Dim ix = CBDb.FindStringExact("BushmillDrinks")
            If ix >= 0 Then
                CBDb.SelectedIndex = ix
            ElseIf CBDb.Items.Count > 0 Then
                CBDb.SelectedIndex = 0
            Else
                MessageBox.Show("No se encontraron bases de datos de usuario en la instancia.",
                            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show("No pude listar las bases de datos:" & Environment.NewLine & ex.Message,
                        "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SetBusy(b As Boolean)
        UseWaitCursor = b
        BBackup.Enabled = Not b
        BRestore.Enabled = Not b
        Refresh()
    End Sub

    '------ BACKUP (elige destino con SaveFileDialog) ------
    Private Sub BBackup_Click(sender As Object, e As EventArgs) Handles BBackup.Click
        Using sfd As New SaveFileDialog()
            sfd.Filter = "Backup SQL (*.bak)|*.bak"
            sfd.FileName = $"BushmillDrinks_{Date.Now:yyyyMMdd_HHmm}.bak"
            If sfd.ShowDialog(Me) <> DialogResult.OK Then Exit Sub

            Dim targetPath = sfd.FileName
            Try
                SetBusy(True) : PB.Value = 0 : LStatus.Text = "Iniciando backup..."

                Using cn As New SqlConnection(ConnStr)
                    AddHandler cn.InfoMessage, AddressOf OnSqlInfo
                    cn.FireInfoMessageEventOnUserErrors = True
                    cn.Open()
                    Using cmd As New SqlCommand($"BACKUP DATABASE [{DbName}] TO DISK=@f WITH INIT, STATS=5;", cn)
                        cmd.Parameters.AddWithValue("@f", targetPath)
                        cmd.CommandTimeout = 0
                        cmd.ExecuteNonQuery()
                    End Using
                    RemoveHandler cn.InfoMessage, AddressOf OnSqlInfo
                End Using

                PB.Value = 100 : LStatus.Text = "Backup completado."
                MessageBox.Show("Backup creado en:" & Environment.NewLine & targetPath,
                                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al crear backup: " & ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                SetBusy(False)
            End Try
        End Using
    End Sub

    '------ RESTORE (elige .bak con OpenFileDialog) ------
    Private Sub BRestore_Click(sender As Object, e As EventArgs) Handles BRestore.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Backup SQL (*.bak)|*.bak"
            If ofd.ShowDialog(Me) <> DialogResult.OK Then Exit Sub

            Dim bak = ofd.FileName
            If MessageBox.Show($"Se restaurará '{DbName}' desde:{Environment.NewLine}{bak}",
                               "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Exit Sub

            Try
                SetBusy(True) : PB.Value = 0 : LStatus.Text = "Preparando restore..."

                Dim csMaster = New SqlConnectionStringBuilder(ConnStr) With {.InitialCatalog = "master"}.ToString()
                Using cn As New SqlConnection(csMaster)
                    AddHandler cn.InfoMessage, AddressOf OnSqlInfo
                    cn.FireInfoMessageEventOnUserErrors = True
                    cn.Open()

                    Using cmd As New SqlCommand("", cn)
                        cmd.CommandTimeout = 0
                        cmd.CommandText = $"ALTER DATABASE [{DbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;"
                        cmd.ExecuteNonQuery()

                        cmd.CommandText = "RESTORE DATABASE [" & DbName & "] FROM DISK=@f WITH REPLACE, STATS=5;"
                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@f", bak)
                        cmd.ExecuteNonQuery()

                        cmd.Parameters.Clear()
                        cmd.CommandText = $"ALTER DATABASE [{DbName}] SET MULTI_USER;"
                        cmd.ExecuteNonQuery()
                    End Using

                    RemoveHandler cn.InfoMessage, AddressOf OnSqlInfo
                End Using

                PB.Value = 100 : LStatus.Text = "Restore completado."
                MessageBox.Show("Restauración exitosa.", "Éxito",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al restaurar: " & ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                SetBusy(False)
            End Try
        End Using
    End Sub

    ' Progreso (lee "XX percent processed.")
    Private Sub OnSqlInfo(sender As Object, e As SqlInfoMessageEventArgs)
        For Each er As SqlError In e.Errors
            Dim msg = er.Message
            Dim i = msg.IndexOf(" percent processed", StringComparison.OrdinalIgnoreCase)
            If i > 0 Then
                Dim num = msg.Substring(0, i).Trim().Split(" "c).Last()
                Dim p As Integer
                If Integer.TryParse(num, p) Then
                    BeginInvoke(Sub()
                                    PB.Value = Math.Max(0, Math.Min(100, p))
                                    LStatus.Text = $"Progreso: {PB.Value}%"
                                End Sub)
                End If
            End If
        Next
    End Sub
End Class
