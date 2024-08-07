using Microsoft.OpenApi.Models;
using Renapo;
using System.Reflection;
using utilidadesv2.Entidades;
using utilidadesv2.Repositorio;
using utilidadesv2.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<Curp>();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<RepositorioDeNombresYApellidos>();
builder.Services.AddScoped<RepositorioDeNombresYApellidosMx>();
builder.Services.AddScoped<ServicioDePersona>();
builder.Services.AddControllers();
builder.Services.Configure<DataSettingsMongoDb>(
    builder.Configuration.GetSection("DataSettingMongoDb")
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1.3",
        Title = "Utilidades",
        Description = @"servicio de códigos postales de México de SEPOMEX",
        Contact = new OpenApiContact
        {
            Name = "Víctor Martínez",
            Url = new Uri("mailto:ahal_tocob@hotmail.com")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowWebApp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
