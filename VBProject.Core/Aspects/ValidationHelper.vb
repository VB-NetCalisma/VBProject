Imports System.ComponentModel.DataAnnotations
Imports FluentValidation
Imports FluentValidation.Results

Public Module ValidationHelper
    Public Sub Validate(ByVal type As Type, ByVal items As Object())
        If Not GetType(IValidator).IsAssignableFrom(type) Then
            Throw New Exception("Validator Geçerli Bir Tip Değildir.")
        End If

        Dim validator = DirectCast(Activator.CreateInstance(type), IValidator)

        For Each item In items
            If validator.CanValidateInstancesOfType(item.GetType()) Then
                Dim result = validator.Validate(New ValidationContext(Of Object)(item))

                Dim ValidatonMessages As New List(Of String)()

                For Each [error] In result.Errors
                    ValidatonMessages.Add([error].ErrorMessage)
                Next

                If Not result.IsValid Then
                    Throw New Exception("Hatalı Doldurma")
                End If
            End If
        Next
    End Sub
End Module
