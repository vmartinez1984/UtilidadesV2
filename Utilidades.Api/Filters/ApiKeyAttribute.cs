using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProductoBusinessLayer;

namespace Utilidades.Api.Filters
{
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {      
        private const string HEADER_NAME = "apikey";     

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();

            if (!context.HttpContext.Request.Headers.TryGetValue(HEADER_NAME, out var apiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key no proporcionada");
                return;
            }

            //var apiKeyConfig = configuration["ApiKey"];

            //if (!apiKeyConfig.Equals(apiKey))
            //{
            //    context.Result = new UnauthorizedObjectResult("Api Key inválida");
            //    return;
            //}

            var service = context.HttpContext.RequestServices.GetService<ProductoBl>();
            var apiKeyConfig = await service.ExisteApikeyAsync(apiKey);
            if (!apiKeyConfig)
            {
                context.Result = new UnauthorizedObjectResult("Api Key inválida");
                return;
            }

            await next();
        }
    }
}
