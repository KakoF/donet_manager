﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Redis
{
    public interface IRedisIntegrator
    {
        Task<T> GetAsync<T>(string key);
        Task<IEnumerable<T>> GetListAsync<T>(string key);
        void Set(string key, object value, int expirationMinutes = 1);
        void SetList(string key, IEnumerable<object> value, int expirationMinutes = 1);
        void Remove(string key);
    }
}
