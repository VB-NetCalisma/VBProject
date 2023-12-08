Imports System.Threading.Tasks

Public Interface IUnitOfWork
        ReadOnly Property ProductRepository As IProductRepository
        ReadOnly Property CategoryRepository As ICategoryRepository
        Function SaveChangeAsync() As Task(Of Integer)
    End Interface

