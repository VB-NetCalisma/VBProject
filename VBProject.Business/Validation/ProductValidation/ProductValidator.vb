Imports FluentValidation
Imports VBProject.Entity

Public Class ProductValidator
    Inherits AbstractValidator(Of ProductDTORequest)
    Public Sub New()
        RuleFor(Function(x) x.Name).NotEmpty().MinimumLength(2).WithMessage("Ürün ismi boş olamaz.")
        RuleFor(Function(x) x.Price).NotEmpty.WithMessage("Ürün fiyatı boş olamaz.")
    End Sub
End Class
