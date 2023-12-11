
Imports Microsoft.AspNetCore.Http
Imports Microsoft.EntityFrameworkCore
Imports System.Threading.Tasks
Imports VBProject.Entity


Public Class UnitOfWork
    Implements IUnitOfWork
    Private ReadOnly _context As VBContext
    Private ReadOnly _contextAccessor As IHttpContextAccessor

    Public Sub New(ByVal contextAccessor As IHttpContextAccessor, ByVal context As VBContext)
        _contextAccessor = contextAccessor
        _context = context

        CategoryRepository = New CategoryRepository(_context)
        ProductRepository = New ProductRepository(_context)

    End Sub

    Public ReadOnly Property ProductRepository As IProductRepository Implements IUnitOfWork.ProductRepository

    Public ReadOnly Property CategoryRepository As ICategoryRepository Implements IUnitOfWork.CategoryRepository

    Public Function SaveChangeAsync() As Task(Of Integer) Implements IUnitOfWork.SaveChangeAsync
        For Each item In _context.ChangeTracker.Entries(Of BaseEntity)()
            If item.State = EntityState.Added Then
                item.Entity.AddedTime = Date.Now
                item.Entity.UpdatedTime = Date.Now
                item.Entity.AddedUser = item.Entity.AddedUser
                item.Entity.UpdatedUser = item.Entity.UpdatedUser
                item.Entity.AddedIPV4Address = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()
                item.Entity.UpdatedIPV4Address = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()

                If item.Entity.IsActive Is Nothing Then
                    item.Entity.IsActive = True
                End If

            ElseIf item.State = EntityState.Modified Then
                item.Entity.UpdatedTime = Date.Now
                item.Entity.UpdatedUser = item.Entity.UpdatedUser
                item.Entity.UpdatedIPV4Address = _contextAccessor.HttpContext.Connection.RemoteIpAddress.ToString()

            ElseIf item.State = EntityState.Deleted Then
                item.Entity.IsActive = False
                item.State = EntityState.Modified

            End If

        Next


        Return _context.SaveChangesAsync()
    End Function
End Class

