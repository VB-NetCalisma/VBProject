Public Class HashWithBCrypt
    Public Shared Function HashPassword(password As String)
        password = BCrypt.Net.BCrypt.HashPassword(password)
        Return password
    End Function
    Public Shared Function VerifyPassword(userPassword As String, requestPassword As String)

        Dim bool = BCrypt.Net.BCrypt.Verify(requestPassword, userPassword)
        If bool Then
            Return 1
        Else
            Return 0
        End If
    End Function
End Class