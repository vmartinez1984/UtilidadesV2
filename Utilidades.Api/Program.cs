using CodigosPostales.ReglasDeNegocio;
using Contador.BusinessLayer;
using Notas.Helpers;
using Peliculas.Bl;
using ProductoBusinessLayer;
using Utilidades.Api.Extensores;
using Utilidades.Servicios.Helpers;
using VMtz84.Pizzas.Extensores;
using JwtToken.Services.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AgregarServicios();
builder.Services.AgregarCodigosPostales();
builder.Services.AgregarPeliculas();
builder.Services.AgregarNotas();
builder.Services.AgregarProductos();
builder.Services.AgregarAutenticacionJwt(builder.Configuration);
builder.Services.AgregarContadores();
builder.Services.AgregarPizzasService();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AgregarConfiguracionDeSawgger();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Yo merengues", policy =>
    {
        policy.RequireClaim("Role", "Yo merengues");
    });
});

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UsarSwagger();

app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
