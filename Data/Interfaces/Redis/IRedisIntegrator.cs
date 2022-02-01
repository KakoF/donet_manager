using System.Collections.Generic;
using System.Threading.Tasks;

namespace Data.Interfaces.Redis
{
    //Não faz sentido o Domínio saber que há dados em Redis, Sql ou qualquer tecnologia que seja.
    public interface IRedisIntegrator
    {
        Task<T> GetAsync<T>(string key);
        Task<IEnumerable<T>> GetListAsync<T>(string key);
        void Set(string key, object value, int expirationMinutes = 1);
        void SetList(string key, IEnumerable<object> value, int expirationMinutes = 1);
        void Remove(string key);
    }
}
