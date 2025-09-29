Public Module SessionUser
    Public Property Usuario As String
    Public Property Nombre As String
    Public Property Rol As String   ' gerente / administrador / vendedor (en minúsculas desde BD)
    Public Property Estado As String ' 'A' o 'I' - útil para validaciones adicionales
End Module

