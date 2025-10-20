
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Imports System.Text


Module Conexion
    Public Conex As New SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString)

    Public Sub AbrirConexion()
        If Conex.State = ConnectionState.Closed Then
            Conex.Open()
        End If
    End Sub

    Public Sub CerrarConexion()
        If Conex.State = ConnectionState.Open Then
            Conex.Close()
        End If
    End Sub
End Module


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
      da As New SqlDataAdapter("
          SELECT u.id_usuario,
                 u.nombre,
                 u.apellido,
                 u.nombre_usuario,
                 r.tipo_rol AS rol,
                 u.estado
          FROM dbo.Usuario u
          INNER JOIN dbo.Rol r ON u.id_rol = r.id_rol
          ORDER BY u.nombre_usuario;", cn)

            Dim dt As New DataTable()
            da.Fill(dt)
            Return dt
        End Using
    End Function


    Public Function ExisteUsuario(usuario As String) As Boolean
        Const sql = "SELECT 1 FROM dbo.Usuario WHERE nombre_usuario=@u"
        Using cn As New SqlConnection(ConnStr),
          cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@u", usuario)
            cn.Open()
            Return cmd.ExecuteScalar() IsNot Nothing
        End Using
    End Function

    Public Function ExisteGerenteActivo() As Boolean
        Const sql = "SELECT 1 FROM dbo.Usuario u 
                 INNER JOIN dbo.Rol r ON u.id_rol = r.id_rol 
                 WHERE r.tipo_rol='gerente' AND u.estado='A'"
        Using cn As New SqlConnection(ConnStr),
          cmd As New SqlCommand(sql, cn)
            cn.Open()
            Return cmd.ExecuteScalar() IsNot Nothing
        End Using
    End Function

    '================== Mutaciones ==================
    Public Sub InsertUsuario(usuario As String, nombre As String, apellido As String, passwordPlano As String, idRol As Integer)
        Const sql = "
            INSERT INTO dbo.Usuario(nombre_usuario, contrasena, nombre, apellido, estado, id_rol)
            VALUES (@usuario, @hash, @nombre, @apellido, 'A', @id_rol);"
        Using cn As New SqlConnection(ConnStr),
          cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@usuario", usuario)
            cmd.Parameters.Add("@hash", SqlDbType.VarBinary, 32).Value = HashSha256(passwordPlano)
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@apellido", apellido)
            cmd.Parameters.AddWithValue("@id_rol", idRol)
            cn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    ' Permite cambiar el PK (usuario) si lo editás
    Public Sub UpdateUsuario(usuarioOriginal As String, usuarioNuevo As String, nombre As String, apellido As String,
                         Optional nuevaPassword As String = Nothing, Optional nuevoIdRol As Integer? = Nothing)
        Dim sb As New StringBuilder("UPDATE dbo.Usuario SET nombre_usuario=@nuevo, nombre=@nombre, apellido=@apellido")
        If nuevaPassword IsNot Nothing Then sb.Append(", contrasena=@hash")
        If nuevoIdRol.HasValue Then sb.Append(", id_rol=@id_rol")
        sb.Append(" WHERE nombre_usuario=@orig;")

        Using cn As New SqlConnection(ConnStr),
          cmd As New SqlCommand(sb.ToString(), cn)
            cmd.Parameters.AddWithValue("@nuevo", usuarioNuevo)
            cmd.Parameters.AddWithValue("@nombre", nombre)
            cmd.Parameters.AddWithValue("@apellido", apellido)
            If nuevaPassword IsNot Nothing Then
                cmd.Parameters.Add("@hash", SqlDbType.VarBinary, 32).Value = HashSha256(nuevaPassword)
            End If
            If nuevoIdRol.HasValue Then cmd.Parameters.AddWithValue("@id_rol", nuevoIdRol.Value)
            cmd.Parameters.AddWithValue("@orig", usuarioOriginal)
            cn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub CambiarEstadoUsuario(usuario As String, activar As Boolean)
        Const sql = "UPDATE dbo.Usuario SET estado = @e WHERE nombre_usuario=@u"
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

        Const sql As String = "SELECT u.contrasena, u.nombre, r.tipo_rol AS rol, u.estado
                       FROM dbo.Usuario u
                       INNER JOIN dbo.Rol r ON u.id_rol = r.id_rol
                       WHERE u.nombre_usuario=@u;"

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
                Dim rol = rd("rol").ToString().ToLower() ' Aseguramos minúsculas
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

    ' Agrega estos métodos a tu DataAccess existente

    Public Function GetRoles() As DataTable
        Using cn As New SqlConnection(ConnStr),
              da As New SqlDataAdapter("SELECT id_rol, tipo_rol FROM dbo.Rol ORDER BY id_rol", cn)
            Dim dt As New DataTable()
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Function GetIdRolPorTipo(tipoRol As String) As Integer
        Const sql = "SELECT id_rol FROM dbo.Rol WHERE tipo_rol = @tipo_rol"
        Using cn As New SqlConnection(ConnStr),
              cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@tipo_rol", tipoRol.ToLower())
            cn.Open()
            Return Convert.ToInt32(cmd.ExecuteScalar())
        End Using
    End Function



    '================== PRODUCTOS ==================

    Public Function GetProductos() As DataTable
        Const sql As String = "
        SELECT p.id_producto,
               p.codigo_barras,
               m.nombre AS marca,
               c.nombre AS categoria,
               p.volumen,
               p.graduacion,
               p.precio,
               p.stock,
               p.proveedor,
               p.estado
        FROM dbo.Producto p
        INNER JOIN dbo.Marca m ON p.id_marca = m.id_marca
        INNER JOIN dbo.Categoria c ON p.id_categoria = c.id_categoria
        ORDER BY p.codigo_barras;"
        Using cn As New SqlConnection(ConnStr),
              da As New SqlDataAdapter(sql, cn)
            Dim dt As New DataTable()
            da.Fill(dt)
            Return dt
        End Using
    End Function

    Public Sub InsertProducto(codigo As String, idMarca As Integer, idCategoria As Integer,
                              volumen As Decimal, graduacion As Decimal,
                              precio As Decimal, stock As Integer,
                              proveedor As String)
        Const sql As String = "
        INSERT INTO dbo.Producto
            (codigo_barras, id_marca, id_categoria, volumen, graduacion, precio, stock, proveedor, estado)
        VALUES
            (@codigo, @marca, @categoria, @volumen, @graduacion, @precio, @stock, @proveedor, 'A');"
        Using cn As New SqlConnection(ConnStr),
              cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@codigo", codigo)
            cmd.Parameters.AddWithValue("@marca", idMarca)
            cmd.Parameters.AddWithValue("@categoria", idCategoria)
            cmd.Parameters.AddWithValue("@volumen", volumen)
            cmd.Parameters.AddWithValue("@graduacion", graduacion)
            cmd.Parameters.AddWithValue("@precio", precio)
            cmd.Parameters.AddWithValue("@stock", stock)
            cmd.Parameters.AddWithValue("@proveedor", proveedor)
            cn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub UpdateProducto(idProducto As Integer, codigo As String, idMarca As Integer, idCategoria As Integer,
                              volumen As Decimal, graduacion As Decimal,
                              precio As Decimal, stock As Integer,
                              proveedor As String)
        Const sql As String = "
        UPDATE dbo.Producto
        SET codigo_barras=@codigo, id_marca=@marca, id_categoria=@categoria,
            volumen=@volumen, graduacion=@graduacion, precio=@precio,
            stock=@stock, proveedor=@proveedor
        WHERE id_producto=@id;"
        Using cn As New SqlConnection(ConnStr),
              cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@codigo", codigo)
            cmd.Parameters.AddWithValue("@marca", idMarca)
            cmd.Parameters.AddWithValue("@categoria", idCategoria)
            cmd.Parameters.AddWithValue("@volumen", volumen)
            cmd.Parameters.AddWithValue("@graduacion", graduacion)
            cmd.Parameters.AddWithValue("@precio", precio)
            cmd.Parameters.AddWithValue("@stock", stock)
            cmd.Parameters.AddWithValue("@proveedor", proveedor)
            cmd.Parameters.AddWithValue("@id", idProducto)
            cn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Public Sub CambiarEstadoProducto(idProducto As Integer, activar As Boolean)
        Const sql = "UPDATE dbo.Producto SET estado = @e WHERE id_producto=@id;"
        Using cn As New SqlConnection(ConnStr),
              cmd As New SqlCommand(sql, cn)
            cmd.Parameters.AddWithValue("@e", If(activar, "A"c, "S"c))
            cmd.Parameters.AddWithValue("@id", idProducto)
            cn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

End Module

