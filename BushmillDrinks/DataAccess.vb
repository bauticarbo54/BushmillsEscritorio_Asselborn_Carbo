
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text


Public Module DataAccess

    Private ReadOnly ConnStr As String =
        System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

    '================== Utilidades ==================
    Private Function HashSha256(text As String) As Byte()
        Using sha As SHA256 = SHA256.Create()
            Return sha.ComputeHash(Encoding.UTF8.GetBytes(text))
        End Using
    End Function

    '================== Consultas ==================
    Public Function GetUsuarios() As DataTable
        Using cn As New SqlConnection(ConnStr),
              da As New SqlDataAdapter("SELECT * FROM dbo.v_Usuarios ORDER BY usuario", cn)
            Dim dt As New DataTable()
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Function ExisteUsuario(usuario As String) As Boolean
        Const sql = "SELECT 1 FROM dbo.Usuarios WHERE usuario=@u"
        Using cn As New SqlConnection(ConnStr),
              cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@u", usuario)
            cn.Open()
            Return cmd.ExecuteScalar() IsNot Nothing
        End Using
    End Function

    Public Function ExisteGerenteActivo() As Boolean
        Const sql = "SELECT 1 FROM dbo.Usuarios WHERE rol='Gerente' AND estado='A'"
        Using cn As New SqlConnection(ConnStr),
              cmd As New SqlCommand(sql, cn)
            cn.Open()
            Return cmd.ExecuteScalar() IsNot Nothing
        End Using
    End Function

    '================== Mutaciones ==================
    Public Sub InsertUsuario(usuario As String, nombre As String, passwordPlano As String, rol As String)
        Const sql = "
INSERT INTO dbo.Usuarios(usuario, contrasena, nombre, estado, rol)
VALUES (@usuario, @hash, @nombre, 'A', @rol);"
        Using cn As New SqlConnection(ConnStr),
              cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@usuario", usuario)
            cmd.Parameters.Add("@hash", SqlDbType.VarBinary, 32).Value = HashSha256(passwordPlano)
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@rol", rol)
            cn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' Permite cambiar el PK (usuario) si lo editás
    Public Sub UpdateUsuario(usuarioOriginal As String, usuarioNuevo As String, nombre As String,
                             Optional nuevaPassword As String = Nothing, Optional nuevoRol As String = Nothing)
        Dim sb As New StringBuilder("UPDATE dbo.Usuarios SET usuario=@nuevo, nombre=@nombre")
        If nuevaPassword IsNot Nothing Then sb.Append(", contrasena=@hash")
        If nuevoRol IsNot Nothing Then sb.Append(", rol=@rol")
        sb.Append(" WHERE usuario=@orig;")

        Using cn As New SqlConnection(ConnStr),
              cmd As New SqlCommand(sb.ToString(), cn)
            cmd.Parameters.AddWithValue("@nuevo", usuarioNuevo)
            cmd.Parameters.AddWithValue("@nombre", nombre)
            If nuevaPassword IsNot Nothing Then
                cmd.Parameters.Add("@hash", SqlDbType.VarBinary, 32).Value = HashSha256(nuevaPassword)
            End If
            If nuevoRol IsNot Nothing Then cmd.Parameters.AddWithValue("@rol", nuevoRol)
            cmd.Parameters.AddWithValue("@orig", usuarioOriginal)
            cn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub CambiarEstadoUsuario(usuario As String, activar As Boolean)
        Const sql = "UPDATE dbo.Usuarios SET estado = @e WHERE usuario=@u"
        Using cn As New SqlConnection(ConnStr),
              cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@e", If(activar, "A"c, "I"c))
            cmd.Parameters.AddWithValue("@u", usuario)
            cn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Function TryLogin(usuario As String, passwordPlano As String) _
    As (Ok As Boolean, Nombre As String, Rol As String, Estado As String)

        Const sql As String = "SELECT contrasena, nombre, rol, estado
                           FROM dbo.Usuarios WHERE usuario=@u;"

        Using cn As New SqlClient.SqlConnection(ConnStr),
              cmd As New SqlClient.SqlCommand(sql, cn)

            cmd.Parameters.AddWithValue("@u", usuario)
            cn.Open()

            Using rd = cmd.ExecuteReader()
                If Not rd.Read() Then
                    Return (False, Nothing, Nothing, Nothing)  ' usuario no existe
                End If

                Dim hashDb = DirectCast(rd("contrasena"), Byte())
                Dim nombre = rd("nombre").ToString()
                Dim rol = rd("rol").ToString()
                Dim estado = rd("estado").ToString() ' 'A' o 'I'

                Dim hashIngresada = HashSha256(passwordPlano)

                ' comparar byte a byte
                Dim ok As Boolean = (hashDb.Length = hashIngresada.Length)
                If ok Then
                    For i = 0 To hashDb.Length - 1
                        If hashDb(i) <> hashIngresada(i) Then
                            ok = False : Exit For
                        End If
                    Next
                End If

                Return (ok AndAlso estado = "A", nombre, rol, estado)
            End Using
        End Using
    End Function

End Module

