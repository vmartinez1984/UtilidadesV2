using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Utilidades.Api.Filters
{
    public class AuthRequirementOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Detecta atributos en el endpoint (Minimal APIs y Controllers)
            var hasAnonymous = context.ApiDescription.CustomAttributes().OfType<AllowAnonymousAttribute>().Any();
            var hasAuthorize = context.ApiDescription.CustomAttributes().OfType<AuthorizeAttribute>().Any();

            if (hasAnonymous || !hasAuthorize) return;

            operation.Security ??= new List<OpenApiSecurityRequirement>();
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        }
    }
}
