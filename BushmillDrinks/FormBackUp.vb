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
        CargarBases()
    End Sub

    Private Sub CargarBases()
        Try
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

            Dim ix = CBDb.FindStringExact("BushmillDrinks")
            If ix >= 0 Then
                CBDb.SelectedIndex = ix
            ElseIf CBDb.Items.Count > 0 Then
                CBDb.SelectedIndex = 0
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

    '------ BACKUP CORREGIDO ------
    Private Sub BBackup_Click(sender As Object, e As EventArgs) Handles BBackup.Click
        Try
            SetBusy(True)
            PB.Value = 0
            LStatus.Text = "Iniciando backup..."

            ' Usar conexión a master para el backup
            Dim csMaster = New SqlConnectionStringBuilder(ConnStr) With {.InitialCatalog = "master"}.ToString()

            Using cn As New SqlConnection(csMaster)
                AddHandler cn.InfoMessage, AddressOf OnSqlInfo
                cn.FireInfoMessageEventOnUserErrors = True
                cn.Open()

                ' Primero, obtener el directorio por defecto de backups de SQL Server
                Dim defaultBackupPath As String = ""
                Using cmdDefault As New SqlCommand("SELECT SERVERPROPERTY('InstanceDefaultBackupPath')", cn)
                    defaultBackupPath = cmdDefault.ExecuteScalar().ToString()
                End Using

                ' Si no hay directorio por defecto, usar uno temporal
                If String.IsNullOrEmpty(defaultBackupPath) Then
                    defaultBackupPath = "C:\Temp\"
                End If

                ' Crear nombre de archivo
                Dim backupFileName = $"BushmillDrinks_{DateTime.Now:yyyyMMdd_HHmmss}.bak"
                Dim sqlServerBackupPath = Path.Combine(defaultBackupPath, backupFileName)

                ' Crear comando de backup
                Dim backupCommand = $"
BACKUP DATABASE [{DbName}] 
TO DISK = @backupPath 
WITH 
    INIT, 
    STATS = 5,
    NAME = 'Backup Completo de BushmillDrinks';"

                Using cmd As New SqlCommand(backupCommand, cn)
                    cmd.Parameters.AddWithValue("@backupPath", sqlServerBackupPath)
                    cmd.CommandTimeout = 0
                    cmd.ExecuteNonQuery()
                End Using

                RemoveHandler cn.InfoMessage, AddressOf OnSqlInfo

                ' Verificar que el archivo se creó en la ubicación de SQL Server
                If File.Exists(sqlServerBackupPath) Then
                    ' Preguntar al usuario dónde quiere guardar una copia
                    Using sfd As New SaveFileDialog()
                        sfd.Filter = "Backup SQL (*.bak)|*.bak"
                        sfd.FileName = backupFileName
                        sfd.Title = "Guardar copia del backup en..."

                        If sfd.ShowDialog() = DialogResult.OK Then
                            File.Copy(sqlServerBackupPath, sfd.FileName, True)

                            Dim fileInfo = New FileInfo(sfd.FileName)
                            MessageBox.Show($"Backup creado exitosamente:" & Environment.NewLine &
                                      $"Ubicación original: {sqlServerBackupPath}" & Environment.NewLine &
                                      $"Copia guardada en: {sfd.FileName}" & Environment.NewLine &
                                      $"Tamaño: {FormatFileSize(fileInfo.Length)}",
                                      "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            ' Mostrar solo la ubicación original
                            Dim fileInfo = New FileInfo(sqlServerBackupPath)
                            MessageBox.Show($"Backup creado en ubicación de SQL Server:" & Environment.NewLine &
                                      $"{sqlServerBackupPath}" & Environment.NewLine &
                                      $"Tamaño: {FormatFileSize(fileInfo.Length)}",
                                      "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        End If
                    End Using
                Else
                    MessageBox.Show($"Backup completado pero el archivo no se encuentra en:" & Environment.NewLine &
                              $"{sqlServerBackupPath}" & Environment.NewLine &
                              "El archivo puede estar en el directorio por defecto de SQL Server.",
                              "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            End Using

            PB.Value = 100
            LStatus.Text = "Backup completado."

        Catch ex As SqlException
            MessageBox.Show($"Error SQL al crear backup: {ex.Message}" & Environment.NewLine &
                      $"Número de error: {ex.Number}",
                      "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show($"Error al crear backup: {ex.Message}",
                      "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            SetBusy(False)
        End Try
    End Sub
    '------ RESTORE CORREGIDO ------
    Private Sub BRestore_Click(sender As Object, e As EventArgs) Handles BRestore.Click
        Using ofd As New OpenFileDialog()
            ofd.Filter = "Backup SQL (*.bak)|*.bak|Todos los archivos (*.*)|*.*"
            ofd.Title = "Seleccionar archivo de backup para restaurar"

            If ofd.ShowDialog() <> DialogResult.OK Then Exit Sub

            Dim backupFile = ofd.FileName

            ' Verificar que el archivo existe
            If Not File.Exists(backupFile) Then
                MessageBox.Show("El archivo de backup seleccionado no existe.",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If

            If MessageBox.Show($"¿Está seguro que desea restaurar la base de datos '{DbName}'?" & Environment.NewLine &
                             $"Desde: {backupFile}" & Environment.NewLine &
                             $"Esta acción sobreescribirá la base de datos actual.",
                             "Confirmar Restauración",
                             MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> DialogResult.Yes Then Exit Sub

            Try
                SetBusy(True)
                PB.Value = 0
                LStatus.Text = "Preparando restore..."

                Dim csMaster = New SqlConnectionStringBuilder(ConnStr) With {.InitialCatalog = "master"}.ToString()

                Using cn As New SqlConnection(csMaster)
                    AddHandler cn.InfoMessage, AddressOf OnSqlInfo
                    cn.FireInfoMessageEventOnUserErrors = True
                    cn.Open()

                    Using cmd As New SqlCommand("", cn)
                        cmd.CommandTimeout = 0

                        ' Poner la base de datos en modo single user
                        LStatus.Text = "Cerrando conexiones..."
                        cmd.CommandText = $"ALTER DATABASE [{DbName}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;"
                        cmd.ExecuteNonQuery()

                        ' Restaurar la base de datos
                        LStatus.Text = "Restaurando base de datos..."
                        cmd.CommandText = $"RESTORE DATABASE [{DbName}] FROM DISK = @backupPath WITH REPLACE, STATS = 5;"
                        cmd.Parameters.Clear()
                        cmd.Parameters.AddWithValue("@backupPath", backupFile)
                        cmd.ExecuteNonQuery()

                        ' Volver a multi user
                        LStatus.Text = "Finalizando..."
                        cmd.CommandText = $"ALTER DATABASE [{DbName}] SET MULTI_USER;"
                        cmd.ExecuteNonQuery()
                    End Using

                    RemoveHandler cn.InfoMessage, AddressOf OnSqlInfo
                End Using

                PB.Value = 100
                LStatus.Text = "Restore completado."

                MessageBox.Show("Restauración completada exitosamente.",
                              "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information)

            Catch ex As SqlException
                MessageBox.Show($"Error SQL al restaurar: {ex.Message}" & Environment.NewLine &
                              $"Número de error: {ex.Number}",
                              "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Catch ex As Exception
                MessageBox.Show($"Error al restaurar: {ex.Message}",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                SetBusy(False)
            End Try
        End Using
    End Sub

    ' Progreso del backup/restore
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

    ' Función para verificar permisos de escritura
    Private Function TienePermisosEscritura(directorio As String) As Boolean
        Try
            Dim testFile = Path.Combine(directorio, "test_write.tmp")
            File.WriteAllText(testFile, "test")
            File.Delete(testFile)
            Return True
        Catch
            Return False
        End Try
    End Function

    ' Función para formatear tamaño de archivo
    Private Function FormatFileSize(bytes As Long) As String
        Dim sizes() As String = {"B", "KB", "MB", "GB", "TB"}
        If bytes = 0 Then Return "0 B"

        Dim i As Integer = CInt(Math.Floor(Math.Log(bytes) / Math.Log(1024)))
        Return Math.Round(bytes / Math.Pow(1024, i), 2) & " " & sizes(i)
    End Function


End Class
