Imports System.Linq.Expressions
Imports Microsoft.EntityFrameworkCore
Imports Microsoft.EntityFrameworkCore.ChangeTracking
Imports VBProject.Entity

Public Class Repository(Of T As BaseEntity)
    Implements IRepository(Of T)
    Private ReadOnly _context As DbContext
    Private ReadOnly _dbSet As DbSet(Of T)

    Public Sub New(ByVal context As DbContext) ' Constructor Injection 
        _context = context
        _dbSet = _context.[Set](Of T)()
    End Sub

    Public Async Function AddAsync(ByVal Entity As T) As Task(Of EntityEntry(Of T)) Implements IRepository(Of T).AddAsync
        Return Await _dbSet.AddAsync(Entity)
    End Function

    Public Async Function GetAllAsync(ByVal Optional Filter As Expression(Of Func(Of T, Boolean)) = Nothing, Optional IncludeProperties As String() = Nothing) As Task(Of IEnumerable(Of T)) Implements IRepository(Of T).GetAllAsync
        Dim query As IQueryable(Of T) = _dbSet ' select * from user 

        ' _dbset.Where(q=>q.id>5) 
        If Filter IsNot Nothing Then
            query = query.Where(Filter) ' select * from User Where id >5 
        End If
        If IncludeProperties.Length > 0 Then
            For Each includeProperty In IncludeProperties ' "User.Orders.OrderDetails.Product.Category"
                query = query.Include(includeProperty)
            Next
        End If
        Return Await Task.Run(Function() query)

    End Function

    Public Async Function GetAsync(ByVal Filter As Expression(Of Func(Of T, Boolean)), ParamArray IncludeProperties As String()) As Task(Of T) Implements IRepository(Of T).GetAsync
        Dim query As IQueryable(Of T) = _dbSet

        If IncludeProperties.Length > 0 Then
            For Each includeProperty In IncludeProperties ' "User.Orders.OrderDetails.Product.Category"
                query = query.Include(includeProperty)
            Next
        End If

        ' select * from User where TCNO=12345678901 

        Return Await query.SingleOrDefaultAsync(Filter)
    End Function

    Public Async Function RemoveAsync(ByVal Entity As T) As Task Implements IRepository(Of T).RemoveAsync
        Await Task.Run(Function() _dbSet.Remove(Entity))
    End Function

    Public Async Function UpdateAsync(ByVal Entity As T) As Task Implements IRepository(Of T).UpdateAsync
        Await Task.Run(Function() _dbSet.Update(Entity))
    End Function
End Class

