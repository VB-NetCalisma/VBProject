Imports AutoMapper
Imports VBProject.Entity

Public Class UserDTOResponesMapper
    Inherits Profile

    Public Sub New()
        CreateMap(Of User, UserDTOResponse)()
        CreateMap(Of UserDTOResponse, User)()
    End Sub
End Class
