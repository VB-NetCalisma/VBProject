Public Class FieldValidationException
    Inherits Exception
    Public Sub New(ByVal messageList As List(Of String))
        MyBase.New()
        Me.Data("FieldValidationMessage") = messageList
    End Sub
End Class
