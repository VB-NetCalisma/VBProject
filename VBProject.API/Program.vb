Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports VBProject.Business
Imports VBProject.DataAccess

Module Program
    Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)

        ' Add services to the container.
        builder.Services.AddControllers()

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
        ' Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer()
        builder.Services.AddSwaggerGen()

        builder.Services.AddHttpContextAccessor()
        builder.Services.AddDbContext(Of VBContext)()
        builder.Services.AddScoped(Of IUnitOfWork, UnitOfWork)()
        builder.Services.AddScoped(Of IProductService, ProductManager)()



        Dim app = builder.Build()

        ' Configure the HTTP request pipeline.
        If app.Environment.IsDevelopment() Then
            app.UseSwagger()
            app.UseSwaggerUI()
        End If

        app.UseHttpsRedirection()
        app.UseAuthorization()
        app.MapControllers()

        app.Run()
    End Sub
End Module

