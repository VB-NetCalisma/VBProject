Public Class HashWithBCrypt
    Public Shared Function HashPassword(password As String)
        BCrypt.Net.BCrypt.HashPassword(password)
        Return password
    End Function
    Public Shared Function VerifyPassword(userPassword As String, requestPassword As String)
        Dim bool = BCrypt.Net.BCrypt.EnhancedVerify(userPassword, BCrypt.Net.BCrypt.HashPassword(requestPassword))
        If bool Then
            Return 1
        Else
            Return 0
        End If
    End Function
End Class