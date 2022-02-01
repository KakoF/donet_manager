using Data.Interfaces.Redis;
using Data.Redis;
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
            serviceCollection.AddScoped<IRedisIntegrator, RedisIntegrator>();
        }
    }
}