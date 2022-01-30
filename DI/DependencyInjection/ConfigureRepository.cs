using Data.DataConnector;
using Data.Repositories;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Repositories.DataConnector;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUsuarioRepository, UsuarioRepository>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}