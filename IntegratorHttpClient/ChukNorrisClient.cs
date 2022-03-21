using Domain.Models.Clients;
using IntegratorHttpClient.factory;
using IntegratorHttpClient.interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegratorHttpClient
{
    public class ChukNorrisClient : ClientFactory<ChuckNorrisModel>, IChukNorrisClient
    {
        public ChukNorrisClient(HttpClient httpClient) : base(httpClient)
        {
        }
    }
}
