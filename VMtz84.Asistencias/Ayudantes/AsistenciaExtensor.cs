using Microsoft.Extensions.DependencyInjection;
using VMtz84.Asistencias.ReglasDeNegocio;
using VMtz84.Asistencias.Repositorios;
using VMtz84.Asistencias.Servicios;

namespace VMtz84.Asistencias.Ayudantes
{
    public static class AsistenciaExtensor
    {
        public static void AgregarAsistencia(this IServiceCollection services)
        {
            services.AddScoped<ContadorRepositorio>();
            services.AddScoped<UsuarioRepositorio>();
            services.AddScoped<AsistenciaRepositorio>();
            services.AddScoped<UsuarioReglaDeNegocio>();
            services.AddScoped<AlmancenDeArchivosServicio>();
            services.AddScoped<AsistenciaReglasDeNegocio>();
        }
    }
}