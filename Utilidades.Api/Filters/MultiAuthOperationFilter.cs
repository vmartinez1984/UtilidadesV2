using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Utilidades.Api.Filters
{
    public class BasicAuthAttribute : Attribute { }

    public class BearerAuthAttribute : Attribute { }

    public class MultiAuthOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasBasic = context.MethodInfo.GetCustomAttributes(true)
                .OfType<BasicAuthAttribute>()
                .Any();

            var hasBearer = context.MethodInfo.GetCustomAttributes(true)
                .OfType<BearerAuthAttribute>()
                .Any();

            if (!hasBasic && !hasBearer)
                return;

            operation.Security = new List<OpenApiSecurityRequirement>();

            if (hasBasic)
            {
                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        new string[] {}
                    }
                });
            }

            if (hasBearer)
            {
                operation.Security.Add(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearer"
                        }
                    },
                    new string[] {}
                }
            });
            }
        }
    }
}
