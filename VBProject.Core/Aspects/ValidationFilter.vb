Imports Microsoft.AspNetCore.Mvc.Filters

Public Class ValidationFilter
    Inherits Attribute
    Implements IAsyncActionFilter

    Private ReadOnly _validationType As Type

    Public Sub New(ByVal validationType As Type)
        _validationType = validationType
    End Sub

    Public Async Function OnActionExecutionAsync(ByVal context As ActionExecutingContext, ByVal [next] As ActionExecutionDelegate) As Task Implements IAsyncActionFilter.OnActionExecutionAsync
        ValidationHelper.Validate(_validationType, context.ActionArguments.Values.ToArray())
        Await [next]()
    End Function
End Class
