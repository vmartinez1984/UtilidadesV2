using CodigosPostales.Repositorios;
using Microsoft.Extensions.DependencyInjection;

namespace CodigosPostales.ReglasDeNegocio
{
    public static class Extensor
    {
        /// <summary>
        /// Inyecta los servicios para codigos postales
        /// </summary>
        /// <param name="services"></param>
        public static void AgregarCodigosPostales(this IServiceCollection services)
        {
            services.AddSingleton<IRepositorio, Repositorio>();
            services.AddSingleton<ICodigoPostalRdn, CodigoPostalRdn>();
        }
    }
}
