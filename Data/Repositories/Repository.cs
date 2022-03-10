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
        protected readonly IRedisIntegrator _cache;

        protected abstract string InsertQuery { get; }
        protected abstract string InsertQueryReturnInserted { get; }
        protected abstract string UpdateByIdQuery { get; }
        protected abstract string DeleteByIdQuery { get; }
        protected abstract string SelectByIdQuery { get; }
        protected abstract string SelectAllQuery { get; }
        protected abstract bool CreateCache { get; }
        protected abstract bool CreateListCache { get; }
        protected abstract bool ReadCache { get; }
        protected abstract bool ReadListCache { get; }


        public Repository(IDbConnector dbConnector, IRedisIntegrator cache)
        {
            _dbConnector = dbConnector;
            _cache = cache;
        }


        public async Task<T> CreateAsync(T data)
        {
            T entity = await _dbConnector.dbConnection.QuerySingleAsync<T>(InsertQueryReturnInserted, data);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return  await _dbConnector.dbConnection.ExecuteAsync(DeleteByIdQuery, new { Id = id });
        }

        public async Task<T> ReadAsync(int id)
        {
            var entity = await _dbConnector.dbConnection.QueryAsync<T>(SelectByIdQuery, new { Id = id });
            return entity.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> ReadAsync()
        {
            //IEnumerable<T> usuariosCache = await _cache.GetListAsync<T>("Usuarios");
            //if (usuariosCache == null)
            //{
            //string sql = "SELECT Id,Nome,Email,DataCriacao,DataAtualizacao FROM [dbo].[Usuario]";
            var data = await _dbConnector.dbConnection.QueryAsync<T>(SelectAllQuery, _dbConnector.dbTransaction);
            //_cache.SetList("Usuarios", usuarios);
            return data.ToList();
            //}
            //return usuariosCache;
        }

        public Task<T> UpdateAsync(int id, T data)
        {
            return await dbConn.ExecuteAsync(UpdateByIdQuery, obj, transaction: dbTransaction);
        }
    }
}
