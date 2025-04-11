using Microsoft.OpenApi.Models;
using Peliculas.Bl;
using System.Reflection;
using Utilidades.Repositorios.Entidades;
using Utilidades.Servicios.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AgregarServicios();

builder.Services.AgregarPeliculas();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Utilidades",
        Description = @"Servicios de utilidades para tests",
        Contact = new OpenApiContact
        {
            Name = "Víctor Martínez",
            Url = new Uri("mailto:ahal_tocob@hotmail.com")
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});

builder.Services.Configure<DataSettingsMongoDb>(builder.Configuration.GetSection("NombresYApellidos"));
builder.Services.Configure<DataSettingsCodigosPostalesMongoDb>(builder.Configuration.GetSection("CodigosPostales"));

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("./swagger/v1/swagger.json", "Utilidades");
    x.RoutePrefix = string.Empty;
});

app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
