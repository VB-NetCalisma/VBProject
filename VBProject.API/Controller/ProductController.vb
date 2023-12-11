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

    <HttpGet("/GetProducts")>
    Public Async Function GetProducts() As Task(Of IActionResult)
        Dim products = Await _productService.GetAllAsync(Function(x) True, {"Category"})

        Dim productDTOResponseList As New List(Of ProductDTOResponse)()
        For Each product In products
            productDTOResponseList.Add(_mapper.Map(Of ProductDTOResponse)(product))
        Next

        Return Ok(productDTOResponseList)
    End Function

    <HttpPost("/AddProduct")>
    Public Async Function AddProduct(productDTORequest As ProductDTORequest) As Task(Of IActionResult)
        Dim product As Product = _mapper.Map(Of Product)(productDTORequest)

        Await _productService.AddAsync(product)

        Dim productDTOResponse As ProductDTOResponse = _mapper.Map(Of ProductDTOResponse)(product)

        Return Ok(productDTOResponse)
    End Function

    <HttpPost("/RemoveProduct/{productId}")>
    Public Async Function RemoveProduct(productId As Int64) As Task(Of IActionResult)
        Dim product As Product = Await _productService.GetAsync(Function(x) x.Id = productId, {"Category"})
        Dim productDTOResponse As ProductDTOResponse = _mapper.Map(Of ProductDTOResponse)(product)
        Await _productService.RemoveAsync(product)



        Return Ok(productDTOResponse)
    End Function

End Class
