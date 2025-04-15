using CodigosPostales.ReglasDeNegocio;
using Microsoft.OpenApi.Models;
using Peliculas.Bl;
using System.Reflection;
using Utilidades.Servicios.Helpers;
using Notas.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AgregarServicios();
builder.Services.AgregarCodigosPostales();
builder.Services.AgregarPeliculas();
builder.Services.AgregarNotas();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(gen =>
{
    //gen.SwaggerDoc("v2", new OpenApiInfo
    //{
    //    Title = "Server API (internal)",
    //    Version = "2.0",
    //    Description = "Internal"
    //});
    gen.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Server API",
        Version = "1.0",
        Description = "This API features all public available endpoints showing different API features."
    });

    //gen.DocInclusionPredicate((docName, apiDesc) =>
    //{
    //    if (docName.Contains("Peliculas"))
    //    {
    //        return apiDesc.RelativePath.Contains("Peliculas/");
    //    }
    //    else
    //    {
    //        return !apiDesc.RelativePath.Contains("Peliculas/");
    //    }
    //});

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    gen.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //c.SwaggerEndpoint("v2/swagger.json", "Server API v1 (administracion)");
    c.RoutePrefix = string.Empty;
});

app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
