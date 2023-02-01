using DRD.Catalogo.API.Data;
using DRD.Catalogo.API.Data.Repository;
using DRD.Catalogo.API.Models;
using DRD.Core.Mediator;

namespace DRD.Catalogo.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<CatalogoContext>();

            return services;
        }
    }
}
