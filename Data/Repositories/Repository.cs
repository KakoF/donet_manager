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
        
        
        protected abstract int MinutesCacheTime{ get; }
        protected abstract bool CreateCache { get; }
        protected abstract bool CreateListCache { get; }
        protected abstract bool ReadCache { get; }
        protected abstract bool ReadListCache { get; }
        protected abstract string NameDataCache { get; }

        public Repository(IDbConnector dbConnector, IRedisIntegrator cache)
        {
            _dbConnector = dbConnector;
            _cache = cache;
        }


        public async Task<T> CreateAsync(T data)
        {
            var entity = await _dbConnector.dbConnection.QuerySingleAsync<T>(InsertQueryReturnInserted, data, _dbConnector.dbTransaction);
            RemoveAllCacheData(entity.Id);
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            int delete = await _dbConnector.dbConnection.ExecuteAsync(DeleteByIdQuery, new { Id = id }, _dbConnector.dbTransaction);
            RemoveAllCacheData(id);
            return Convert.ToBoolean(delete);
        }

        public async Task<T> ReadAsync(int id)
        {
            if (ReadCache)
            {
                var dataCache = await GetDataCache(id);
                if (dataCache != null)
                    return dataCache;
            }
            var entity = await _dbConnector.dbConnection.QueryAsync<T>(SelectByIdQuery, new { Id = id });
            if(ReadCache && entity != null)
                SetDataCache(entity);
            return entity.FirstOrDefault();
        }

        public async Task<IEnumerable<T>> ReadAsync()
        {
            if (ReadListCache)
            {
                var dataCache = await GetDataCache();
                if (dataCache != null)
                    return dataCache;
            }
           
            var data = await _dbConnector.dbConnection.QueryAsync<T>(SelectAllQuery);
            if(CreateListCache)
                SetDataCache(data);

            return data.ToList();
        }

        public async Task<T> UpdateAsync(int id, T data)
        {
            await _dbConnector.dbConnection.ExecuteAsync(UpdateByIdQuery, data, _dbConnector.dbTransaction);
            RemoveAllCacheData(id);
            return data;
        }

        public void  RemoveDataCache()
        {
            _cache.Remove(NameDataCache);
        }

        public void RemoveDataCache(int id)
        {
            _cache.Remove($"{NameDataCache}_{id}");
        }

        public void RemoveAllCacheData(int id)
        {
            if (ReadListCache)
                _cache.Remove(NameDataCache);
            if (ReadCache)
                _cache.Remove($"{NameDataCache}_{id}");
        }

        public async Task<T> GetDataCache(int id)
        {
           return await _cache.GetAsync<T>($"{NameDataCache}_{id}");
        }

        public async Task<IEnumerable<T>> GetDataCache()
        {
            return await _cache.GetListAsync<T>(NameDataCache);
        }

        public void SetDataCache(T data)
        {
            _cache.Set($"{NameDataCache}_{data.Id}", data, MinutesCacheTime);
        }

        public void SetDataCache(IEnumerable<T> data)
        {
            _cache.SetList(NameDataCache, data, MinutesCacheTime);
        }
    }
}
