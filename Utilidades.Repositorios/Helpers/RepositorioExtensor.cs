using Microsoft.Extensions.DependencyInjection;

namespace Utilidades.Repositorios.Helpers
{
    public static class RepositorioExtensor
    {
        public static void AgregarRepositorios(this IServiceCollection services)
        {
            services.AddScoped<NombreRepositorio>();
            services.AddScoped<DireccionRepositorio>();
            services.AddScoped<RepositorioDeCodigoPostal>();
        }
    }
}
