using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<T> ReadAsync(int id);
        Task<IEnumerable<T>> ReadAsync();
        Task<T> CreateAsync(T data);
        Task<T> UpdateAsync(int id, T data);
        Task<bool> DeleteAsync(int id);
    }
}
