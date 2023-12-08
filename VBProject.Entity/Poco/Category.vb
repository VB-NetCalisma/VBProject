Public Class Category
    Inherits BaseEntity

    Sub New()
        Products = New List(Of Product)
    End Sub

    Public Property Name As String
    Public Property Products As IEnumerable(Of Product)

End Class
