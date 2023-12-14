Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Core
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

    <HttpPost("/AddProduct")>
    Public Async Function AddProduct(productDTORequest As ProductDTORequest) As Task(Of IActionResult)
        Dim product As Product = _mapper.Map(Of Product)(productDTORequest)

        Await _productService.AddAsync(product)

        Dim productDTOResponse As ProductDTOResponse = _mapper.Map(Of ProductDTOResponse)(product)

        Return Ok(Result(Of ProductDTOResponse).SuccessWithData(productDTOResponse))
    End Function

    <HttpPost("/UpdateProduct")>
    Public Async Function UpdateProduct(productDTORequest As ProductDTORequest) As Task(Of IActionResult)
        Dim product = Await _productService.GetAsync(Function(x) x.Id = productDTORequest.Id)
        If product Is Nothing Then
            Return Ok(Result(Of ProductDTOResponse).SuccessNoDataFound())
        End If
        product = _mapper.Map(productDTORequest, product)
        Await _productService.UpdateAsync(product)
        Dim productDTOResponse = _mapper.Map(Of ProductDTOResponse)(product)
        Return Ok(Result(Of ProductDTOResponse).SuccessWithData(productDTOResponse))
    End Function

    <HttpPost("/RemoveProduct/{productId}")>
    Public Async Function RemoveProduct(productId As Int64) As Task(Of IActionResult)
        Dim product As Product = Await _productService.GetAsync(Function(x) x.Id = productId, {"Category"})
        Dim productDTOResponse As ProductDTOResponse = _mapper.Map(Of ProductDTOResponse)(product)
        Await _productService.RemoveAsync(product)

        Return Ok(Result(Of Product).SuccessWithoutData())
    End Function

    <HttpGet("/GetProducts")>
    Public Async Function GetProducts() As Task(Of List(Of ProductDTOResponse))
        Dim products = Await _productService.GetAllAsync(Function(x) True, {"Category"})

        Dim productDTOResponseList As New List(Of ProductDTOResponse)()
        For Each product In products
            productDTOResponseList.Add(_mapper.Map(Of ProductDTOResponse)(product))
        Next

        'Return Ok(Result(Of List(Of ProductDTOResponse)).SuccessWithData(productDTOResponseList))
        Return productDTOResponseList
    End Function

    <HttpGet("/GetProduct/{productId}")>
    Public Async Function GetProduct(productId As Int64) As Task(Of IActionResult)
        Dim product = Await _productService.GetAsync(Function(x) x.Id = productId, {"Category"})
        If product Is Nothing Then
            Return Ok(Result(Of ProductDTOResponse).SuccessNoDataFound())
        End If

        Dim productDTOResponse = _mapper.Map(Of ProductDTOResponse)(product)

        Return Ok(Result(Of ProductDTOResponse).SuccessWithData(productDTOResponse))

    End Function

    <HttpGet("/GetProductsByCategory/{categoryId}")>
    Public Async Function GetProductsByCategory(categoryId As Int64) As Task(Of IActionResult)
        Dim products = Await _productService.GetAllAsync(Function(x) x.CategoryId = categoryId, {"Category"})

        Dim productDTOResponseList As New List(Of ProductDTOResponse)()
        For Each product In products
            productDTOResponseList.Add(_mapper.Map(Of ProductDTOResponse)(product))
        Next

        Return Ok(Result(Of List(Of ProductDTOResponse)).SuccessWithData(productDTOResponseList))
    End Function
End Class
