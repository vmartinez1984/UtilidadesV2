using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Runtime.CompilerServices;
using Utilidades.Api.Filters;

namespace Utilidades.Api.Extensores
{
    /// <summary>
    /// Configuraçión de swagger
    /// </summary>
    public static class SwaggerExtensor
    {
        /// <summary>
        /// Configuración de swagger documents
        /// </summary>
        /// <param name="services"></param>
        public static void AgregarConfiguracionDeSawgger(this IServiceCollection services)
        {
            services.AddSwaggerGen(gen =>
            {
                gen.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Server API",
                    Version = "1.0",
                    Description = "This API features all public available endpoints showing different API features."
                });

                gen.SwaggerDoc("cruds", new OpenApiInfo
                {
                    Title = "Server API",
                    Version = "1.0",
                    Description = "CRUDS para pruebas."
                });

                gen.SwaggerDoc("Notas", new OpenApiInfo
                {
                    Title = "Notas",
                    Version = "Notas",
                    Description = "Claves, referencias, etc"
                });

                gen.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Introduce sólo el token. El prefijo 'Bearer' se agrega automáticamente."
                });

                gen.OperationFilter<AuthRequirementOperationFilter>();

                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                gen.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        /// <summary>
        /// Use swagger local, se agregan los elementos de combobox
        /// </summary>
        /// <param name="app"></param>
        public static void UsarSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.SwaggerEndpoint("/swagger/Notas/swagger.json", "Notas");
                c.SwaggerEndpoint("/swagger/cruds/swagger.json", "CRUDS");

                c.RoutePrefix = string.Empty;
            });
        }
    }
}
