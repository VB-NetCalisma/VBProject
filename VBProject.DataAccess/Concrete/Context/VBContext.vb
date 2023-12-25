
Imports Microsoft.EntityFrameworkCore
Imports VBProject.Entity
Partial Public Class VBContext
    Inherits DbContext
    Public Sub New()
    End Sub

    Public Sub New(ByVal options As DbContextOptions(Of VBContext))
        MyBase.New(options)
    End Sub

    Public Overridable Property Products As DbSet(Of Product)

    Public Overridable Property Categories As DbSet(Of Category)
    Public Overridable Property Users As DbSet(Of User)



    Protected Overrides Sub OnConfiguring(ByVal optionsBuilder As DbContextOptionsBuilder)
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-R04PVQ3; Initial Catalog=VisualBasicDB; Integrated Security=true; TrustServerCertificate=True")
    End Sub
End Class

