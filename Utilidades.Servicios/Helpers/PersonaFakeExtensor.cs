using Microsoft.Extensions.DependencyInjection;
using Utilidades.Repositorios.Helpers;

namespace Utilidades.Servicios.Helpers
{
    public static class PersonaFakeExtensor
    {
        public static void AgregarServicios(this IServiceCollection services)
        {
            services.AgregarRepositorios();

            services.AddScoped<PersonaFakeServicio>();
            services.AddScoped<DireccionServicio>();
        }
    }
}
