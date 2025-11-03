Imports System.Data.SqlClient
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports iTextSharp.text.pdf.draw
Public Class FormReportes

    Private ConnStr As String = System.Configuration.ConfigurationManager.ConnectionStrings("BushmillDb").ConnectionString

    Private Sub FormReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim hoy = Date.Today
        DPDesde.Value = New Date(hoy.Year, hoy.Month, 1)
        DPHasta.Value = DPDesde.Value.AddMonths(1).AddDays(-1)

        PrepararTabsPorRol()
        CargarReportes()
    End Sub

    Private Sub BActualizar_Click(sender As Object, e As EventArgs) Handles BActualizar.Click
        CargarReportes()
    End Sub

    ' ------------------ CONTROL POR ROL ------------------
    Private Sub PrepararTabsPorRol()
        For Each tp As TabPage In TC.TabPages
            tp.Parent = Nothing
        Next

        Dim rol As String = If(SessionUser.Rol, "").ToLower()

        Select Case rol
            Case "gerente"
                TPGerencia.Parent = TC
                TPAdmin.Parent = TC
                TPVendedor.Parent = TC
            Case "administrador"
                TPAdmin.Parent = TC
            Case "vendedor"
                TPVendedor.Parent = TC
            Case Else
                TPVendedor.Parent = TC
        End Select
    End Sub

    Private Sub CargarReportes()
        Dim d As Date = DPDesde.Value.Date
        Dim h As Date = DPHasta.Value.Date

        If TPGerencia.Parent IsNot Nothing Then CargarGerencia(d, h)
        If TPAdmin.Parent IsNot Nothing Then CargarAdmin(d, h)
        If TPVendedor.Parent IsNot Nothing Then CargarVendedor(d, h, SessionUser.Usuario)
    End Sub

    ' ------------------ REPORTES ------------------

    ' === GERENTE ===
    Private Sub CargarGerencia(desde As Date, hasta As Date)
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Dim sql As String =
                "SELECT FORMAT(v.fecha, 'yyyy-MM') AS Mes,
                        SUM(v.total_venta) AS Ingresos,
                        SUM(d.cantidad * p.precio * 0.6) AS Costos,
                        SUM(v.total_venta) - SUM(d.cantidad * p.precio * 0.6) AS Utilidad,
                        COUNT(DISTINCT v.num_venta) AS CantVentas
                 FROM Venta v
                 INNER JOIN DetalleVenta d ON v.num_venta = d.num_venta
                 INNER JOIN Producto p ON d.id_producto = p.id_producto
                 WHERE v.fecha BETWEEN @d AND @h
                 GROUP BY FORMAT(v.fecha, 'yyyy-MM')
                 ORDER BY Mes;"

            Using da As New SqlDataAdapter(sql, cn)
                da.SelectCommand.Parameters.AddWithValue("@d", desde)
                da.SelectCommand.Parameters.AddWithValue("@h", hasta)
                da.Fill(dt)
            End Using
        End Using

        DGVGerencia.DataSource = dt
        FormatearMonedas(DGVGerencia, {"Ingresos", "Costos", "Utilidad"})
        Autoajustar(DGVGerencia)
    End Sub

    ' === ADMINISTRADOR ===
    Private Sub CargarAdmin(desde As Date, hasta As Date)
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Dim sql As String =
                "SELECT TOP 10 p.nombre AS Producto,
                        SUM(d.cantidad) AS Cantidad,
                        SUM(d.subtotal) AS Monto
                 FROM DetalleVenta d
                 INNER JOIN Producto p ON d.id_producto = p.id_producto
                 INNER JOIN Venta v ON d.num_venta = v.num_venta
                 WHERE v.fecha BETWEEN @d AND @h
                 GROUP BY p.nombre
                 ORDER BY SUM(d.cantidad) DESC;"

            Using da As New SqlDataAdapter(sql, cn)
                da.SelectCommand.Parameters.AddWithValue("@d", desde)
                da.SelectCommand.Parameters.AddWithValue("@h", hasta)
                da.Fill(dt)
            End Using
        End Using

        DGVAdmin.DataSource = dt
        FormatearMonedas(DGVAdmin, {"Monto"})
        Autoajustar(DGVAdmin)
    End Sub

    ' === VENDEDOR ===
    Private Sub CargarVendedor(desde As Date, hasta As Date, usuario As String)
        Dim dt As New DataTable()

        Using cn As New SqlConnection(ConnStr)
            Dim sql As String =
            "SELECT v.num_venta,
                    CAST(v.fecha AS DATE) AS Día,
                    COUNT(d.id_producto) AS Items,
                    SUM(v.total_venta) AS Monto
             FROM Venta v
             INNER JOIN DetalleVenta d ON v.num_venta = d.num_venta
             WHERE v.nombre_usuario = @u AND v.fecha BETWEEN @d AND @h
             GROUP BY v.num_venta, CAST(v.fecha AS DATE)
             ORDER BY Día DESC;"

            Using da As New SqlDataAdapter(sql, cn)
                da.SelectCommand.Parameters.AddWithValue("@d", desde)
                da.SelectCommand.Parameters.AddWithValue("@h", hasta)
                da.SelectCommand.Parameters.AddWithValue("@u", usuario)
                da.Fill(dt)
            End Using
        End Using

        DGVVendedor.DataSource = dt
        DGVVendedor.Columns("Día").DefaultCellStyle.Format = "dd/MM/yyyy"
        FormatearMonedas(DGVVendedor, {"Monto"})
        Autoajustar(DGVVendedor)

        ' === Agregar columna de botón (solo si no existe) ===
        If Not DGVVendedor.Columns.Contains("Imprimir") Then
            Dim btnCol As New DataGridViewButtonColumn()
            btnCol.HeaderText = "Factura"
            btnCol.Text = "🧾 Imprimir"
            btnCol.UseColumnTextForButtonValue = True
            btnCol.Name = "Imprimir"
            btnCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
            DGVVendedor.Columns.Add(btnCol)
        End If
    End Sub

    Private Sub DGVVendedor_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DGVVendedor.CellContentClick
        ' Si se hizo click en la columna del botón
        If e.RowIndex >= 0 AndAlso DGVVendedor.Columns(e.ColumnIndex).Name = "Imprimir" Then
            Dim numVenta As Integer = Convert.ToInt32(DGVVendedor.Rows(e.RowIndex).Cells("num_venta").Value)
            GenerarFacturaPDF(numVenta)
        End If
    End Sub


    ' ------------------ HELPERS VISUALES ------------------
    Private Sub FormatearMonedas(dgv As DataGridView, cols() As String)
        For Each c In cols
            If dgv.Columns.Contains(c) Then
                dgv.Columns(c).DefaultCellStyle.Format = "C2"
                dgv.Columns(c).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
            End If
        Next
    End Sub

    Private Sub Autoajustar(dgv As DataGridView)
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.ReadOnly = True
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToDeleteRows = False
        dgv.RowHeadersVisible = False
    End Sub

    Private Sub GenerarFacturaPDF(numVenta As Integer)
        Try
            ' --- Consultar datos de la venta ---
            Dim venta As New DataTable()
            Dim detalles As New DataTable()

            Using cn As New SqlConnection(ConnStr)
                cn.Open()

                ' Cabecera (cliente, total, fecha, vendedor)
                Dim sqlVenta As String =
            "SELECT v.num_venta, v.fecha, v.total_venta, 
                    c.nombre AS cliente, c.dni, c.telefono,
                    v.nombre_usuario AS vendedor
             FROM Venta v
             INNER JOIN Cliente c ON v.dni_cliente = c.dni
             WHERE v.num_venta = @num;"
                Using da As New SqlDataAdapter(sqlVenta, cn)
                    da.SelectCommand.Parameters.AddWithValue("@num", numVenta)
                    da.Fill(venta)
                End Using

                ' Detalles de los productos (ahora incluye marca y volumen)
                Dim sqlDetalle As String =
            "SELECT p.nombre AS producto, m.nombre AS marca, p.volumen, 
                    d.cantidad, p.precio, d.subtotal
             FROM DetalleVenta d
             INNER JOIN Producto p ON d.id_producto = p.id_producto
             INNER JOIN Marca m ON p.id_marca = m.id_marca
             WHERE d.num_venta = @num;"
                Using da2 As New SqlDataAdapter(sqlDetalle, cn)
                    da2.SelectCommand.Parameters.AddWithValue("@num", numVenta)
                    da2.Fill(detalles)
                End Using
            End Using

            If venta.Rows.Count = 0 Then
                MessageBox.Show("No se encontró la venta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim fila = venta.Rows(0)
            Dim rutaArchivo As String = Path.Combine(My.Computer.FileSystem.SpecialDirectories.MyDocuments,
                                                 $"Factura_{numVenta}_{Now:yyyyMMddHHmmss}.pdf")

            ' --- Crear el PDF ---
            Dim doc As New Document(PageSize.A4, 40, 40, 60, 40)
            Dim writer = PdfWriter.GetInstance(doc, New FileStream(rutaArchivo, FileMode.Create))
            doc.Open()

            ' === COLORES Y FUENTES ===
            Dim verde As BaseColor = New BaseColor(34, 139, 34)
            Dim grisClaro As BaseColor = New BaseColor(245, 245, 245)
            Dim grisOscuro As BaseColor = New BaseColor(70, 70, 70)
            Dim fuenteTitulo As iTextSharp.text.Font = FontFactory.GetFont("Helvetica", 18, iTextSharp.text.Font.BOLD, verde)
            Dim fuenteSubtitulo As iTextSharp.text.Font = FontFactory.GetFont("Helvetica", 11, iTextSharp.text.Font.BOLD, grisOscuro)
            Dim fuenteNormal As iTextSharp.text.Font = FontFactory.GetFont("Helvetica", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK)

            ' === LOGO Y ENCABEZADO ===
            Dim logoPath As String = "C:\Users\santi\OneDrive\Documents\Proyecto\BushmillDrinks\BushmillDrinks\My Project\logo.png"

            Dim tablaEncabezado As New PdfPTable(2)
            tablaEncabezado.WidthPercentage = 100
            tablaEncabezado.SetWidths({20, 80})
            tablaEncabezado.DefaultCell.Border = Rectangle.NO_BORDER

            ' Logo si existe
            If File.Exists(logoPath) Then
                Dim logo = iTextSharp.text.Image.GetInstance(logoPath)
                logo.ScaleAbsolute(60, 60)
                Dim celdaLogo As New PdfPCell(logo)
                celdaLogo.Border = Rectangle.NO_BORDER
                celdaLogo.HorizontalAlignment = Element.ALIGN_LEFT
                celdaLogo.VerticalAlignment = Element.ALIGN_MIDDLE
                tablaEncabezado.AddCell(celdaLogo)
            Else
                ' Si no hay logo, celda vacía
                tablaEncabezado.AddCell(New PdfPCell(New Phrase("")) With {.Border = Rectangle.NO_BORDER})
            End If

            ' Título y subtítulo a la derecha
            Dim textoEncabezado As New Phrase()
            textoEncabezado.Add(New Chunk("Bushmill Drinks" & vbCrLf, fuenteTitulo))
            textoEncabezado.Add(New Chunk($"Factura N° {numVenta}" & vbCrLf, fuenteSubtitulo))

            Dim celdaTexto As New PdfPCell(textoEncabezado)
            celdaTexto.Border = Rectangle.NO_BORDER
            celdaTexto.HorizontalAlignment = Element.ALIGN_RIGHT
            celdaTexto.VerticalAlignment = Element.ALIGN_MIDDLE
            tablaEncabezado.AddCell(celdaTexto)

            doc.Add(tablaEncabezado)

            ' Separador verde debajo del encabezado
            doc.Add(New Paragraph(" "))
            doc.Add(New LineSeparator(0.5F, 100, verde, Element.ALIGN_CENTER, -2))
            doc.Add(New Paragraph(" "))


            ' === DATOS DEL CLIENTE Y VENTA ===
            Dim tablaInfo As New PdfPTable(2)
            tablaInfo.WidthPercentage = 100
            tablaInfo.SetWidths({50, 50})

            tablaInfo.AddCell(New PdfPCell(New Phrase("Cliente", fuenteSubtitulo)) With {.BackgroundColor = grisClaro, .Border = Rectangle.NO_BORDER})
            tablaInfo.AddCell(New PdfPCell(New Phrase("Vendedor", fuenteSubtitulo)) With {.BackgroundColor = grisClaro, .Border = Rectangle.NO_BORDER})

            tablaInfo.AddCell(New Phrase($"{fila("cliente")}" & vbCrLf &
                                    $"DNI: {fila("dni")}" & vbCrLf &
                                    $"Tel: {fila("telefono")}", fuenteNormal))
            tablaInfo.AddCell(New Phrase($"{fila("vendedor")}" & vbCrLf &
                                    $"Fecha: {CDate(fila("fecha")).ToString("dd/MM/yyyy")}", fuenteNormal))
            doc.Add(tablaInfo)
            doc.Add(New Paragraph(" "))
            doc.Add(New LineSeparator(0.5F, 100, BaseColor.GRAY, Element.ALIGN_CENTER, -2))
            doc.Add(New Paragraph(" "))

            ' === DETALLE DE PRODUCTOS ===
            Dim tablaProductos As New PdfPTable(4)
            tablaProductos.WidthPercentage = 100
            tablaProductos.SetWidths({50, 15, 15, 20})
            tablaProductos.SpacingBefore = 10
            tablaProductos.SpacingAfter = 10
            tablaProductos.DefaultCell.Padding = 6

            ' Encabezados
            Dim headers = {"Producto", "Cant.", "Precio", "Subtotal"}
            For Each h In headers
                Dim cell As New PdfPCell(New Phrase(h, fuenteSubtitulo))
                cell.BackgroundColor = verde
                cell.HorizontalAlignment = Element.ALIGN_CENTER
                cell.Padding = 6
                tablaProductos.AddCell(cell)
            Next

            ' Filas de productos con marca + volumen
            Dim alternar As Boolean = False
            For Each dr As DataRow In detalles.Rows
                Dim bg As BaseColor = If(alternar, grisClaro, BaseColor.WHITE)
                alternar = Not alternar

                Dim detalleProd As String = dr("producto").ToString()
                Dim marca As String = If(detalles.Columns.Contains("marca"), dr("marca").ToString(), "")
                Dim volumen As String = If(detalles.Columns.Contains("volumen"), dr("volumen").ToString(), "")

                If Not String.IsNullOrEmpty(marca) Then
                    detalleProd &= " – " & marca
                End If
                If Not String.IsNullOrEmpty(volumen) Then
                    detalleProd &= " (" & volumen & " ml)"
                End If

                tablaProductos.AddCell(New PdfPCell(New Phrase(detalleProd, fuenteNormal)) With {.BackgroundColor = bg})
                tablaProductos.AddCell(New PdfPCell(New Phrase(dr("cantidad").ToString(), fuenteNormal)) With {.HorizontalAlignment = Element.ALIGN_CENTER, .BackgroundColor = bg})
                tablaProductos.AddCell(New PdfPCell(New Phrase(FormatCurrency(dr("precio")), fuenteNormal)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .BackgroundColor = bg})
                tablaProductos.AddCell(New PdfPCell(New Phrase(FormatCurrency(dr("subtotal")), fuenteNormal)) With {.HorizontalAlignment = Element.ALIGN_RIGHT, .BackgroundColor = bg})
            Next

            doc.Add(tablaProductos)
            doc.Add(New Paragraph(" "))

            ' === TOTAL ===
            Dim tablaTotal As New PdfPTable(1)
            tablaTotal.WidthPercentage = 100
            Dim celdaTotal As New PdfPCell(New Phrase($"TOTAL: {FormatCurrency(fila("total_venta"))}", FontFactory.GetFont("Helvetica", 14, Font.Bold, verde)))
            celdaTotal.HorizontalAlignment = Element.ALIGN_RIGHT
            celdaTotal.Border = Rectangle.NO_BORDER
            celdaTotal.Padding = 5
            tablaTotal.AddCell(celdaTotal)
            doc.Add(tablaTotal)

            doc.Add(New Paragraph(" "))
            doc.Add(New LineSeparator(0.5F, 100, verde, Element.ALIGN_CENTER, -2))
            doc.Add(New Paragraph(" "))

            ' === PIE ===
            Dim pie As New Paragraph("Gracias por su compra. ¡Lo esperamos pronto!", FontFactory.GetFont("Helvetica", 10, Font.Italic, BaseColor.DARK_GRAY))
            pie.Alignment = Element.ALIGN_CENTER
            doc.Add(pie)

            doc.Close()
            writer.Close()

            Process.Start(New ProcessStartInfo With {.FileName = rutaArchivo, .UseShellExecute = True})

        Catch ex As Exception
            MessageBox.Show($"Error al generar factura: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class

