using Dapper;
using Data.Interfaces.DataConnector;
using Data.Interfaces.Redis;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Base
    {
        protected readonly IDbConnector _dbConnector;
        //protected readonly IRedisIntegrator _cache;

        protected abstract string InsertQuery { get; }
        protected abstract string InsertQueryReturnInserted { get; }
        protected abstract string UpdateByIdQuery { get; }
        protected abstract string DeleteByIdQuery { get; }
        protected abstract string SelectByIdQuery { get; }
        protected abstract string SelectAllQuery { get; }
       
        public Repository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }


        public async Task<T> CreateAsync(T data)
        {
            var entity = await _dbConnector.dbConnection.QuerySingleAsync<T>(InsertQueryReturnInserted, data, _dbConnector.dbTransaction);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int delete = await _dbConnector.dbConnection.ExecuteAsync(DeleteByIdQuery, new { Id = id }, _dbConnector.dbTransaction);
            return Convert.ToBoolean(delete);
        }

        public async Task<T> ReadAsync(int id)
        {
            var entity = await _dbConnector.dbConnection.QueryAsync<T>(SelectByIdQuery, new { Id = id });
            return entity.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> ReadAsync()
        {
            var data = await _dbConnector.dbConnection.QueryAsync<T>(SelectAllQuery);
            return data.ToList();
        }

        public async Task<T> UpdateAsync(int id, T data)
        {
            await _dbConnector.dbConnection.ExecuteAsync(UpdateByIdQuery, data, _dbConnector.dbTransaction);
            return data;
        }
    }
}
