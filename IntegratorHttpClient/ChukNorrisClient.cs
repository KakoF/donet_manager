using Domain.Models.Clients;
using IntegratorHttpClient.factory;
using IntegratorHttpClient.interfaces;
using System.Net.Http;

namespace IntegratorHttpClient
{
    public class ChukNorrisClient : ClientFactory<ChuckNorrisModel>, IChukNorrisClient
    {
        public ChukNorrisClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
