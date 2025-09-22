Imports System.Data

Public Class FormReportes

    Private ReadOnly ListaProductos As String() =
        {"Cerveza Lager", "Cerveza IPA", "Vino Malbec", "Vino Cabernet", "Vodka", "Gin", "Fernet", "Whisky", "Aperol", "Ron"}

    Private Sub FormReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Rango por defecto: mes actual
        Dim hoy = Date.Today
        DPDesde.Value = New Date(hoy.Year, hoy.Month, 1)
        DPHasta.Value = DPDesde.Value.AddMonths(1).AddDays(-1)

        PrepararTabsPorRol()
        CargarReportes()
    End Sub

    Private Sub BActualizar_Click(sender As Object, e As EventArgs) Handles BActualizar.Click
        CargarReportes()
    End Sub

    ' --------- mostrar solo las pestañas que corresponden al rol ----------
    Private Sub PrepararTabsPorRol()
        For Each tp As TabPage In TC.TabPages
            tp.Parent = Nothing
        Next

        Select Case SessionUser.Rol
            Case "Gerente"
                TPGerencia.Parent = TC
                TPAdmin.Parent = TC
                TPVendedor.Parent = TC
            Case "Administrador"
                TPAdmin.Parent = TC
            Case "Vendedor"
                TPVendedor.Parent = TC
            Case Else
                ' si no hay sesión, dejo vendedor para testear
                TPVendedor.Parent = TC
        End Select
    End Sub

    Private Sub CargarReportes()
        Dim d As Date = DPDesde.Value.Date
        Dim h As Date = DPHasta.Value.Date

        If TPGerencia.Parent IsNot Nothing Then CargarGerencia(d, h)
        If TPAdmin.Parent IsNot Nothing Then CargarAdmin(d, h)
        If TPVendedor.Parent IsNot Nothing Then CargarVendedor(d, h, If(String.IsNullOrWhiteSpace(SessionUser.Usuario), "vendedor", SessionUser.Usuario))
    End Sub

    ' ================= GERENCIA: resumen por mes =================
    Private Sub CargarGerencia(desde As Date, hasta As Date)
        Dim dt As New DataTable()
        dt.Columns.Add("Mes", GetType(String))
        dt.Columns.Add("Ingresos", GetType(Decimal))
        dt.Columns.Add("Costos", GetType(Decimal))
        dt.Columns.Add("Utilidad", GetType(Decimal))
        dt.Columns.Add("CantVentas", GetType(Integer))

        Dim m As Date = New Date(desde.Year, desde.Month, 1)
        Dim fin As Date = New Date(hasta.Year, hasta.Month, 1)

        While m <= fin
            Dim r As New Random(m.Year * 100 + m.Month) ' semilla estable por mes
            Dim ingresos As Decimal = 120000 + r.Next(0, 80000)
            Dim costos As Decimal = ingresos * (0.58D + CDec(r.NextDouble()) * 0.12D)
            Dim cant As Integer = 400 + r.Next(0, 250)
            dt.Rows.Add(m.ToString("yyyy-MM"),
                        Math.Round(ingresos, 2),
                        Math.Round(costos, 2),
                        Math.Round(ingresos - costos, 2),
                        cant)
            m = m.AddMonths(1)
        End While

        DGVGerencia.DataSource = dt
        FormatearMonedas(DGVGerencia, {"Ingresos", "Costos", "Utilidad"})
        Autoajustar(DGVGerencia)
    End Sub

    ' ============== ADMIN: top productos por cantidad y monto ==============
    Private Sub CargarAdmin(desde As Date, hasta As Date)
        Dim dt As New DataTable()
        dt.Columns.Add("Producto", GetType(String))
        dt.Columns.Add("Cantidad", GetType(Integer))
        dt.Columns.Add("Monto", GetType(Decimal))

        Dim r As New Random(desde.Year * 10000 + hasta.Year * 10 + desde.Month)

        For Each p In ListaProductos
            Dim cant As Integer = r.Next(80, 650)
            Dim precio As Decimal = 120 + r.Next(0, 400)
            dt.Rows.Add(p, cant, Math.Round(cant * precio, 2))
        Next

        ' orden por cantidad desc; si querés por monto, cambiá el Sort
        Dim dv As New DataView(dt) With {.Sort = "Cantidad DESC"}
        TPAdmin.Text = "Administración (Top productos)"
        DGVAdmin.DataSource = dv.ToTable().AsEnumerable().Take(10).CopyToDataTable()

        FormatearMonedas(DGVAdmin, {"Monto"})
        Autoajustar(DGVAdmin)
    End Sub

    ' ============== VENDEDOR: ventas diarias del usuario logueado =========
    Private Sub CargarVendedor(desde As Date, hasta As Date, usuario As String)
        Dim dt As New DataTable()
        dt.Columns.Add("Día", GetType(Date))
        dt.Columns.Add("Ventas", GetType(Integer))
        dt.Columns.Add("Monto", GetType(Decimal))

        Dim r As New Random((desde - Date.MinValue).Days Xor usuario.GetHashCode())

        Dim d As Date = desde
        While d <= hasta
            Dim ventas As Integer = r.Next(0, 12)          ' 0..11 ventas
            Dim monto As Decimal = 0
            For i = 1 To ventas
                monto += 800 + r.Next(0, 4200)             ' ticket simulado
            Next
            dt.Rows.Add(d, ventas, Math.Round(monto, 2))
            d = d.AddDays(1)
        End While

        TPVendedor.Text = $"Vendedor ({usuario})"
        DGVVendedor.DataSource = dt

        FormatearMonedas(DGVVendedor, {"Monto"})
        DGVVendedor.Columns("Día").DefaultCellStyle.Format = "dd/MM/yyyy"
        Autoajustar(DGVVendedor)
    End Sub

    ' ---------------- helpers de presentación ----------------
    Private Sub FormatearMonedas(dgv As DataGridView, cols() As String)
        For Each c In cols
            If dgv.Columns.Contains(c) Then
                dgv.Columns(c).DefaultCellStyle.Format = "C2"
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

End Class

