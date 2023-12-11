Public Class ErrorInfo
    Public Property Fail As List(Of String)
    Public Property ErrorMessage As String

    Public Shared Function NotFound(Optional ErrorMessage As String = "Sonuç Bulunamadı", Optional Fail As List(Of String) = Nothing) As ErrorInfo
        Return New ErrorInfo() With {.ErrorMessage = ErrorMessage, .Fail = Fail}
    End Function

    Public Shared Function Mistake(Optional Fail As List(Of String) = Nothing, Optional ErrorMessage As String = "Hata Oluştu") As ErrorInfo
        Return New ErrorInfo() With {.ErrorMessage = ErrorMessage, .Fail = Fail}
    End Function

    Public Shared Function FieldValidationError(Optional Fail As List(Of String) = Nothing, Optional ErrorMessage As String = "Zorunlu Alanlar Eksik") As ErrorInfo
        Return New ErrorInfo With {.Fail = Fail, .ErrorMessage = ErrorMessage}
    End Function

    Public Shared Function TokenNotFoundError(Optional Fail As List(Of String) = Nothing, Optional ErrorMessage As String = "Token Bilgisi Gelmedi") As ErrorInfo
        Return New ErrorInfo With {.Fail = Fail, .ErrorMessage = ErrorMessage}
    End Function

    Public Shared Function ForbiddenError(Optional Fail As List(Of String) = Nothing, Optional ErrorMessage As String = "Yetkisiz Giriş!") As ErrorInfo
        Return New ErrorInfo With {.Fail = Fail, .ErrorMessage = ErrorMessage}
    End Function
End Class
