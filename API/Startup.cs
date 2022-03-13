using API.Helpers.Middleware;
using AutoMapper;
using Data.DataConnector;
using Data.Interfaces.DataConnector;
using DI.DependencyInjection;
using DI.Mappings;
using IntegratorRabbitMq.Interfaces.RabbitMqIntegrator;
using IntegratorRabbitMq.RabbitMqIntegrator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            if (_environment.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("sqlServer", "Server=127.0.0.1,1433;Database=master_integration_test;User Id=sa;Password=Manager010203!@#;TrustServerCertificate=true;");
                Environment.SetEnvironmentVariable("redis", "127.0.0.1:6379,password=Manager010203!@#");
                Environment.SetEnvironmentVariable("redisName", "managerRedis");
                Environment.SetEnvironmentVariable("rabbit", "amqp://guest:guest@localhost:5673");
            }

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = _environment.IsEnvironment("Testing") ? Environment.GetEnvironmentVariable("redis") : Configuration.GetConnectionString("redis");
                options.InstanceName = _environment.IsEnvironment("Testing") ? Environment.GetEnvironmentVariable("redisName") : Configuration["redisName"];

            });
            services.AddScoped<IDbConnector>(db => new SqlServerConnector(_environment.IsEnvironment("Testing") ? Environment.GetEnvironmentVariable("sqlServer") : Configuration.GetConnectionString("sqlServer")));
            services.AddScoped<IRabbitMqIntegrator>(db => new RabbitMqIntegrator(_environment.IsEnvironment("Testing") ? Environment.GetEnvironmentVariable("rabbit") : Configuration.GetConnectionString("rabbit")));
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);
            services.AddControllers(options => options.Filters.Add<ValidationMiddleware>())
               .AddNewtonsoftJson(options =>
                   options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
               .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new DtoToModelProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddSingleton(_ => Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }

            app.UseHttpsRedirection();

            app.UseMiddleware(typeof(ErrorResponseMiddleware));

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
