Imports System.Net

Public Class Result(Of T)
    Public Property Data As T
    Public Property Mesaj As String
    Public Property StatusCode As Integer
    Public Property ErrorInfo As ErrorInfo

    Public Sub New(_data As T, _mesaj As String, _statusCode As Integer, _ErrorInfo As ErrorInfo)
        Data = _data
        Mesaj = _mesaj
        StatusCode = _statusCode
        ErrorInfo = _ErrorInfo
    End Sub

    Public Sub New(_mesaj As String, _statusCode As Integer, _ErrorInfo As ErrorInfo)
        Mesaj = _mesaj
        StatusCode = _statusCode
        ErrorInfo = _ErrorInfo
    End Sub

    Public Sub New(_statusCode As Integer, _ErrorInfo As ErrorInfo)
        StatusCode = _statusCode
        ErrorInfo = _ErrorInfo
    End Sub

    Public Shared Function [Error](Optional message As String = "Hata Oluştu", Optional statusCode As Integer = CInt(HttpStatusCode.InternalServerError)) As Result(Of T)
        Return New Result(Of T)(message, statusCode, ErrorInfo.Mistake)
    End Function

    Public Shared Function SuccessWithData(data As T, Optional message As String = "İşlem Başarılı", Optional statusCode As Integer = CInt(HttpStatusCode.OK)) As Result(Of T)
        Return New Result(Of T)(data, message, statusCode, Nothing)
    End Function

    Public Shared Function SuccessWithoutData(Optional message As String = "İşlem Başarılı", Optional statusCode As Integer = CInt(HttpStatusCode.OK)) As Result(Of T)
        Return New Result(Of T)(message, statusCode, Nothing)
    End Function

    Public Shared Function SuccessNoDataFound(Optional message As String = "Sonuç Bulunamadı", Optional statusCode As Integer = CInt(HttpStatusCode.NotFound)) As Result(Of T)
        Return New Result(Of T)(message, statusCode, ErrorInfo.NotFound())
    End Function

    Public Shared Function FieldValidationError(ErrorInfo As ErrorInfo, Optional statusCode As Integer = CInt(HttpStatusCode.BadRequest)) As Result(Of T)
        Return New Result(Of T)("Hata Oluştu", statusCode, ErrorInfo)
    End Function

    Public Shared Function TokenNotFound() As Result(Of T)
        Return New Result(Of T)("Hata Oluştu", CInt(HttpStatusCode.Unauthorized), ErrorInfo.TokenNotFoundError())
    End Function

    Public Shared Function ExistingError(Optional message As String = "Hata Oluştu", Optional statusCode As Integer = CInt(HttpStatusCode.BadRequest)) As Result(Of T)
        Return New Result(Of T)(message, statusCode, ErrorInfo.Mistake)
    End Function

    Public Shared Function AlreadyExistError(Optional message As String = "Aynı yetkiden birden fazla eklenemez.", Optional statusCode As Integer = CInt(HttpStatusCode.BadRequest)) As Result(Of T)
        Return New Result(Of T)(message, statusCode, ErrorInfo.Mistake)
    End Function
End Class
