using Domain.Models.Clients;
using IntegratorHttpClient.factory;
using IntegratorHttpClient.interfaces;
using System.Net.Http;

namespace IntegratorHttpClient
{
    public class AdviceClient : ClientFactory<AdviceModel>, IAdviceClient
    {
        public AdviceClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
