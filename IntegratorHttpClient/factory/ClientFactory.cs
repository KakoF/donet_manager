using IntegratorHttpClient.interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegratorHttpClient.factory
{
    public abstract class ClientFactory<T> : IHttpClient<T>
    {
        protected readonly HttpClient _httpClient;
        //protected abstract string _remoteServiceBaseUrl { get; }

        public ClientFactory(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri(_remoteServiceBaseUrl);
        }
        public async Task<T> Get(string path)
        {
            var responseString = await _httpClient.GetStringAsync(path);
            var response = JsonConvert.DeserializeObject<T>(responseString);
            return response;
        }
    }
}
