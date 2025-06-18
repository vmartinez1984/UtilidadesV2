using Microsoft.Extensions.DependencyInjection;

namespace ProductoBusinessLayer
{
    public static class ProductoExtensor
    {
        public static void AgregarProductos(this IServiceCollection services)
        {
            services.AddScoped<ProductoBl>();
            services.AddScoped<ProductoRepositorio>();
        }
    }
}
