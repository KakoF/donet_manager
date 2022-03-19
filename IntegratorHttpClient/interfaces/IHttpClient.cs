using System.Threading.Tasks;

namespace IntegratorHttpClient.interfaces
{
    public interface IHttpClient<T>
    {
        Task<T> Get(string path);
    }
}
