using AutoMapper;
using DI.Mappings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace API.Integration.Test.Integration
{
    public class BaseIntegration : IDisposable
    {
        public HttpClient client{ get; private set; }
        public IMapper mapper { get; set; }
        public string hostApi { get; set; }

        public HttpResponseMessage response { get; set; }

        public BaseIntegration()
        {
            hostApi = "https://localhost:44372/api/";
            var builder = new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();
            var server = new TestServer(builder);
            mapper = new AutoMapperFixture().GetMapper();
            client = server.CreateClient();

        }

        public static async Task<HttpResponseMessage> PostJsonAsync(object dataClass, string url, HttpClient client)
        {
            return await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(dataClass), System.Text.Encoding.UTF8, "application/json"));
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }

    public class AutoMapperFixture: IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
                cfg.AddProfile(new DtoToModelProfile());
            });

            return config.CreateMapper();
        }
    }
}
