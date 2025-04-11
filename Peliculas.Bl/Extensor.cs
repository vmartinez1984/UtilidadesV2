using Microsoft.Extensions.DependencyInjection;

namespace Peliculas.Bl
{
    public static class Extensor
    {
        public static void AgregarPeliculas(this IServiceCollection services)
        {
            services.AddScoped<AlmacenDeArchivos>();
            services.AddScoped<PeliculaRepositorio>();
            services.AddScoped<PeliculaBl>();
        }
    }
}
