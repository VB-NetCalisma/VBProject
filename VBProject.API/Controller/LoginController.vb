Imports System
Imports System.IdentityModel.Tokens.Jwt
Imports System.Security.Claims
Imports System.Text
Imports AutoMapper
Imports DevExpress.Xpo
Imports Microsoft.AspNetCore.Mvc
Imports Microsoft.Extensions.Configuration
Imports Microsoft.IdentityModel.Tokens
Imports VBProject.Business
Imports VBProject.Core
Imports VBProject.Entity

<ApiController>
<Route("[action]")>
Public Class LoginController
    Inherits Controller

    Private ReadOnly _userService As IUserService
    Private ReadOnly _configuration As IConfiguration
    Private ReadOnly _mapper As IMapper

    Public Sub New(userService As IUserService, mapper As IMapper, configuration As IConfiguration)
        _userService = userService
        _mapper = mapper
        _configuration = configuration
    End Sub

    <HttpPost("/Login")>
    Public Async Function Login(loginRequestDTO As LoginDTORequest) As Task(Of IActionResult)
        Dim user = Await _userService.GetAsync(Function(x) x.Email = loginRequestDTO.Email)
        If user Is Nothing Then
            Return NotFound(Result(Of LoginDTOResponse).ExistingError)
        End If
        If HashWithBCrypt.VerifyPassword(user.Password, loginRequestDTO.Password) Then
            Dim claims As New List(Of Claim)() From
            {
                New Claim(ClaimTypes.Name, user.FirstName),
                New Claim(ClaimTypes.Surname, user.LastName),
                New Claim(ClaimTypes.Email, user.Email)
            }

            Dim secretKey = _configuration("JWT:Token")
            Dim issuer = _configuration("JWT:Issuer")
            Dim audiance = _configuration("JWT:Audiance")

            Dim tokenHandler As New JwtSecurityTokenHandler()
            Dim key = Encoding.UTF8.GetBytes(secretKey)
            Dim tokenDescriptor As New SecurityTokenDescriptor With
            {
                .Audience = audiance,
                .Issuer = issuer,
                .Subject = New ClaimsIdentity(claims),
                .Expires = DateTime.Now.AddDays(20), ' Token süresi (örn: 20 gün)
                .SigningCredentials = New SigningCredentials(New SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            }

            Dim token = tokenHandler.CreateToken(tokenDescriptor)

            Dim loginDTOResponse As New LoginDTOResponse() With
            {
                .FirstName = user.FirstName,
                .LastName = user.LastName,
                .Email = user.Email,
                .Phone = user.Phone,
                .Token = tokenHandler.WriteToken(token)
            }

            Return Ok(Result(Of LoginDTOResponse).SuccessWithData(loginDTOResponse))
        End If
        Return Ok(Result(Of LoginDTOResponse).SuccessNoDataFound())
    End Function
End Class