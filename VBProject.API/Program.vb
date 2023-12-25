Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.Extensions.DependencyInjection
Imports Microsoft.Extensions.Hosting
Imports VBProject.Business
Imports VBProject.DataAccess
Imports FluentValidation.AspNetCore
Imports Microsoft.AspNetCore.Authentication
Imports Microsoft.AspNetCore.Authentication.JwtBearer
Imports Microsoft.IdentityModel.Tokens
Imports System.Text

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
        builder.Services.AddScoped(Of IUserService, UserManager)()
        builder.Services.AddFluentValidationAutoValidation()
        builder.Services.AddAuthentication(Sub(opt)
                                               opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme
                                               opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme
                                           End Sub).AddJwtBearer(Sub(options)
                                                                     options.IncludeErrorDetails = True
                                                                     options.TokenValidationParameters = New TokenValidationParameters() With
                                                          {
                                                              .ValidateIssuer = True,
                                                              .ValidateAudience = True,
                                                              .ValidateLifetime = True,
                                                              .ValidateIssuerSigningKey = True,
                                                              .ValidIssuer = builder.Configuration("JWT:Issuer"), ' Tokený oluþturan tarafýn adresi
                                                              .ValidAudience = builder.Configuration("JWT:Audiance"), ' Tokenýn kullanýlacaðý hedef adres
                                                              .IssuerSigningKey = New SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration("JWT:Token"))) ' Gizli anahtar
                                                          }
                                                                 End Sub)

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

