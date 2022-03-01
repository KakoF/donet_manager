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
        public string hostApi { get; set; }

        public HttpResponseMessage response { get; set; }

        public BaseIntegration()
        {
            hostApi = "https://localhost:44372/api/";
            var builder = new WebHostBuilder().UseEnvironment("Testing").UseStartup<Startup>();
            var server = new TestServer(builder);
            AutoMapperFixture autmapper = new AutoMapperFixture();
            client = server.CreateClient();

        }

        public static async Task<HttpResponseMessage> PostJsonAsync(object dataClass, string url, HttpClient client)
        {
            return await client.PostAsync(url, new StringContent(JsonConvert.SerializeObject(dataClass), System.Text.Encoding.UTF8, "application/json"));
        }

        public static async Task<HttpResponseMessage> PutJsonAsync(object dataClass, string url, HttpClient client)
        {
            return await client.PutAsync(url, new StringContent(JsonConvert.SerializeObject(dataClass), System.Text.Encoding.UTF8, "application/json"));
        }

        public static async Task<HttpResponseMessage> GetAsync(string url, HttpClient client)
        {
            return await client.GetAsync(url);
        }

        public static async Task<HttpResponseMessage> DeleteAsync(string url, HttpClient client)
        {
            return await client.DeleteAsync(url);
        }


        public void Dispose()
        {
            client.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class AutoMapperFixture: IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public static IMapper GetMapper()
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
