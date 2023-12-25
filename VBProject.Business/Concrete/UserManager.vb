Imports System.Linq.Expressions
Imports VBProject.DataAccess
Imports VBProject.Entity

Public Class UserManager
    Implements IUserService
    Private ReadOnly _uow As IUnitOfWork

    Public Sub New(ByVal uow As IUnitOfWork)
        _uow = uow
    End Sub

    Public Async Function GetAsync(ByVal Filter As Expression(Of Func(Of User, Boolean)), ParamArray IncludeProperties() As String) As Task(Of User) Implements IGenericService(Of User).GetAsync
        Return Await _uow.UserRepository.GetAsync(Filter, IncludeProperties)
    End Function

    Public Async Function GetAllAsync(ByVal Optional Filter As Expression(Of Func(Of User, Boolean)) = Nothing, Optional IncludeProperties() As String = Nothing) As Task(Of IEnumerable(Of User)) Implements IGenericService(Of User).GetAllAsync
        Return Await _uow.UserRepository.GetAllAsync(Filter, IncludeProperties)
    End Function

    Public Async Function AddAsync(ByVal Entity As User) As Task(Of User) Implements IGenericService(Of User).AddAsync
        Await _uow.UserRepository.AddAsync(Entity)
        Await _uow.SaveChangeAsync()
        Return Entity
    End Function

    Public Async Function UpdateAsync(ByVal Entity As User) As Task Implements IGenericService(Of User).UpdateAsync
        Await _uow.UserRepository.UpdateAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function

    Public Async Function RemoveAsync(ByVal Entity As User) As Task Implements IGenericService(Of User).RemoveAsync
        Await _uow.UserRepository.RemoveAsync(Entity)
        Await _uow.SaveChangeAsync()
    End Function
End Class
