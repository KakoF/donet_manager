using Data.Implementations;
using Domain.Interfaces.Implementations;
using Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;

namespace DI.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IUsuarioService, UsuarioService>();
            serviceCollection.AddTransient<IGeneroService, GeneroService>();
            serviceCollection.AddTransient<IUsarioImplementation, UsarioImplementation>();
            serviceCollection.AddTransient<IGeneroImplementation, GeneroImplementation>();

        }
    }
}