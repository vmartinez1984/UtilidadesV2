using Microsoft.Extensions.DependencyInjection;

namespace Contador.BusinessLayer
{
    public static class ContadorExtensor
    {
        public static void AgregarContadores(this IServiceCollection services)
        {
            services.AddScoped<ContadorService>();
        }
    }
}
