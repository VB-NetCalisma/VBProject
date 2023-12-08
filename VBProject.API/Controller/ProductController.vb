Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class ProductController
    Inherits Controller
    Private ReadOnly _productService As IProductService
    Private ReadOnly _mapper As IMapper

    Public Sub New(mapper As IMapper, productService As IProductService)
        _mapper = mapper
        _productService = productService
    End Sub

    '<HttpGet, Route("GetAllProducts")>
    'Public Async Function GetAllProducts() As Task(Of IActionResult)

    '    Return Ok("Deneme GetAll")
    'End Function

    <HttpGet("/GetProducts")>
    Public Async Function GetProducts() As Task(Of IActionResult)
        Dim products = Await _productService.GetAllAsync(Function(x) True, {"Category"})

        Dim productDTOResponseList As New List(Of ProductDTOResponse)()
        For Each product In products
            productDTOResponseList.Add(_mapper.Map(Of ProductDTOResponse)(product))
        Next

        Return Ok(productDTOResponseList)
    End Function

End Class
