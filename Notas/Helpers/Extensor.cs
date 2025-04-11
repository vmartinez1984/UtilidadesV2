using Microsoft.Extensions.DependencyInjection;
using Notas.BussinesLayer;
using Notas.Persistence;

namespace Notas.Helpers
{
    public static class Extensor
    {
        /// <summary>
        /// Inyecta los servicios para Notas
        /// </summary>
        /// <param name="services"></param>
        public static void AgregarNotas(this IServiceCollection services)
        {
            services.AddSingleton<EstadoRepositorio>();
            services.AddSingleton<NotaRepositorio>();
            services.AddSingleton<NotaBl>();
        }
    }
}
