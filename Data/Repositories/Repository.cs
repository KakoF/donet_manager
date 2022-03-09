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
    public class Repository<T> : IRepository<T> where T : Base
    {
        private readonly IDbConnector _dbConnector;
        private readonly IRedisIntegrator _cache;

        public virtual string InsertQuery => "";
        public virtual string InsertQueryReturnInserted => "";
        public virtual string UpdateByIdQuery => "";
        public virtual string DeleteByIdQuery => "";
        public virtual string SelectAllQuery => "";
        public virtual string SelectByIdQuery => "";

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
            throw new NotImplementedException();
        }
    }
}
