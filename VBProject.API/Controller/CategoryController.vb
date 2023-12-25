Imports System
Imports AutoMapper
Imports Microsoft.AspNetCore.Mvc
Imports VBProject.Business
Imports VBProject.Core
Imports VBProject.Entity

<ApiController>
<Route("[Action]")>
Public Class CategoryController
    Inherits Controller

    Private ReadOnly _categoryService As ICategoryService
    Private ReadOnly _mapper As IMapper
    Public Sub New(categoryService As ICategoryService, mapper As IMapper)
        _categoryService = categoryService
        _mapper = mapper
    End Sub

    <HttpPost("/AddCategory")>
    Public Async Function AddCategory(categoryDTORequest As CategoryDTORequest) As Task(Of IActionResult)
        Dim category As Category = _mapper.Map(Of Category)(categoryDTORequest)
        Await _categoryService.AddAsync(category)

        Dim categoryDTOResponse As CategoryDTOResponse = _mapper.Map(Of CategoryDTOResponse)(category)
        Return Ok(Result(Of CategoryDTOResponse).SuccessWithData(categoryDTOResponse))
    End Function

    <HttpPost("/UpdateCategory")>
    Public Async Function UpdateCategory(categoryDTORequest As CategoryDTORequest) As Task(Of IActionResult)
        Dim category = Await _categoryService.GetAsync(Function(x) x.Id = categoryDTORequest.Id)
        category = _mapper.Map(categoryDTORequest, category)
        Await _categoryService.UpdateAsync(category)

        Dim categoryDTOResponse As CategoryDTOResponse = _mapper.Map(Of CategoryDTOResponse)(category)
        Return Ok(Result(Of CategoryDTOResponse).SuccessWithData(categoryDTOResponse))
    End Function

    <HttpPost("/DeleteCategory/{categoryId}")>
    Public Async Function DeleteCategory(categoryId As Int64) As Task(Of IActionResult)
        Dim category = Await _categoryService.GetAsync(Function(x) x.Id = categoryId)
        Await _categoryService.RemoveAsync(category)

        Return Ok(Result(Of CategoryDTOResponse).SuccessWithoutData())
    End Function

    <HttpGet("/GetCategories")>
    Public Async Function GetCategories() As Task(Of IActionResult)
        Dim categories = Await _categoryService.GetAllAsync()

        Dim categoryResponseDTOList As List(Of CategoryDTOResponse)
        For Each category In categories
            categoryResponseDTOList.Add(_mapper.Map(Of CategoryDTOResponse)(category))
        Next

        Return Ok(Result(Of List(Of CategoryDTOResponse)).SuccessWithData(categoryResponseDTOList))

    End Function

    <HttpGet("/GetCategory/{categoryId}")>
    Public Async Function GetCategory(categoryId As Int64) As Task(Of IActionResult)
        Dim category = Await _categoryService.GetAsync(Function(x) x.Id = categoryId)
        Dim categoryResponseDTO = _mapper.Map(Of CategoryDTOResponse)(category)

        Return Ok(Result(Of CategoryDTOResponse).SuccessWithData(categoryResponseDTO))
    End Function

End Class