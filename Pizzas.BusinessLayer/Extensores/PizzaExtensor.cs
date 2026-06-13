using Microsoft.Extensions.DependencyInjection;
using VMtz84.Pizzas.Services;

namespace VMtz84.Pizzas.Extensores
{
    public static class PizzaExtensor
    {
        public static void AgregarPizzasService(this IServiceCollection services)
        {
            services.AddScoped<PizzaService>();
            services.AddScoped<OrdenService>();
            services.AddScoped<MenuService>();
            services.AddScoped<ClienteService>();
        }
    }
}
