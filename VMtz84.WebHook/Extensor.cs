using Microsoft.Extensions.DependencyInjection;

namespace VMtz84.WebHook
{
    public static class Extensor
    {
        public static void AgregarWebHookService(this IServiceCollection services)
        {
            services.AddScoped<WebHookService>();
        }
    }
}