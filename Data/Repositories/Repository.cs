using Data.Interfaces.DataConnector;
using Data.Interfaces.Redis;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly IDbConnector _dbConnector;
        private readonly IRedisIntegrator _cache;

        public Repository(IDbConnector dbConnector, IRedisIntegrator cache)
        {
            _dbConnector = dbConnector;
            _cache = cache;
        }

        public Task<T> CreateAsync(T data)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(int id, T data)
        {
            throw new NotImplementedException();
        }
    }
}
