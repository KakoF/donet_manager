using Data.DataConnector;
using Data.Implementations;
using Data.Interfaces.DataConnector;
using Data.Interfaces.Redis;
using Data.Redis;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DI.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            //serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
            //serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            //serviceCollection.AddScoped<IRepository<Base>>();
            serviceCollection.AddScoped<Repository<Base>>((serviceProvider) => (Repository<Base>)serviceProvider.GetRequiredService<IRepository<Base>>());
            serviceCollection.AddScoped<IRedisIntegrator, RedisIntegrator>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}