Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Core
Imports VBProject.Entity

<ApiController>
<Route("[action]")>
Public Class UserController
    Inherits Controller

    Private ReadOnly _userService As IUserService
    Private ReadOnly _mapper As IMapper

    Public Sub New(userService As IUserService, mapper As IMapper)
        _userService = userService
        _mapper = mapper
    End Sub

    <HttpPost("/AddUser")>
    Public Async Function AddUser(userRequestDTO As UserDTORequest) As Task(Of IActionResult)
        Dim user As User = _mapper.Map(Of User)(userRequestDTO)
        user.Password = HashWithBCrypt.HashPassword(userRequestDTO.Password)
        Await _userService.AddAsync(user)

        Dim userResponseDTO As UserDTOResponse = _mapper.Map(Of UserDTOResponse)(user)
        Return Ok(Result(Of UserDTOResponse).SuccessWithData(userResponseDTO))
    End Function

    <HttpPost("/UpdateUser")>
    Public Async Function UpdateUser(userRequestDTO As UserDTORequest) As Task(Of IActionResult)
        Dim user = Await _userService.GetAsync(Function(x) x.Id = userRequestDTO.Id)

        user = _mapper.Map(userRequestDTO, user)
        Await _userService.UpdateAsync(user)

        Dim userResponseDTO As UserDTOResponse = _mapper.Map(Of UserDTOResponse)(user)
        Return Ok(Result(Of UserDTOResponse).SuccessWithData(userResponseDTO))
    End Function

    <HttpPost("/DeleteUser/{userId}")>
    Public Async Function DeleteUser(userId As Int64) As Task(Of IActionResult)
        Dim user = Await _userService.GetAsync(Function(x) x.Id = userId)
        Await _userService.RemoveAsync(user)
        Return Ok(Result(Of UserDTOResponse).SuccessWithoutData)
    End Function

    <HttpGet("/GetUser/{userId}")>
    Public Async Function GetUser(userId As Int64) As Task(Of IActionResult)
        Dim user = Await _userService.GetAsync(Function(x) x.Id = userId)
        Dim userDTOResponse = _mapper.Map(Of UserDTOResponse)(user)
        Return Ok(Result(Of UserDTOResponse).SuccessWithData(userDTOResponse))
    End Function

    <HttpGet("/GetUsers")>
    Public Async Function GetUsers() As Task(Of IActionResult)
        Dim users = Await _userService.GetAllAsync()
        Dim userDTOResponseList As New List(Of UserDTOResponse)
        For Each item In users
            userDTOResponseList.Add(_mapper.Map(Of UserDTOResponse)(item))
        Next
        Return Ok(Result(Of List(Of UserDTOResponse)).SuccessWithData(userDTOResponseList))
    End Function

End Class