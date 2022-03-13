using Dapper;
using Data.Interfaces.DataConnector;
using Data.Interfaces.Redis;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class UsarioImplementation : Repository<Usuario>, IUsarioImplementation
    {
        protected readonly IRedisIntegrator _cache;
        protected override string InsertQuery => $"INSERT INTO [{nameof(Usuario)}] ([{nameof(Usuario.Nome)}] ,[{nameof(Usuario.Email)}] ,[{nameof(Usuario.DataCriacao)}]) VALUES (@{nameof(Usuario.Nome)}, @{nameof(Usuario.Email)}, @{nameof(Usuario.DataCriacao)})";
        protected override string InsertQueryReturnInserted => $"INSERT INTO [{nameof(Usuario)}] ([{nameof(Usuario.Nome)}] ,[{nameof(Usuario.Email)}] ,[{nameof(Usuario.GeneroId)}], [{nameof(Usuario.DataCriacao)}]) OUTPUT Inserted.* VALUES (@{nameof(Usuario.Nome)}, @{nameof(Usuario.Email)}, @{nameof(Usuario.GeneroId)}, @{nameof(Usuario.DataCriacao)})";
        protected override string UpdateByIdQuery => $"UPDATE [{nameof(Usuario)}] SET {nameof(Usuario.Nome)} = @{nameof(Usuario.Nome)}, {nameof(Usuario.Email)} = @{nameof(Usuario.Email)}, {nameof(Usuario.DataAtualizacao)} = @{nameof(Usuario.DataAtualizacao)} WHERE {nameof(Usuario.Id)} = @{nameof(Usuario.Id)}";
        protected override string DeleteByIdQuery => $"DELETE FROM [{nameof(Usuario)}] WHERE {nameof(Usuario.Id)} = @{nameof(Usuario.Id)}";
        protected override string SelectAllQuery => $"SELECT * FROM [{nameof(Usuario)}]";
        protected override string SelectByIdQuery => $"SELECT * FROM [{nameof(Usuario)}] WHERE {nameof(Usuario.Id)} = @{nameof(Usuario.Id)}";

        
        public UsarioImplementation(IDbConnector dbConnector, IRedisIntegrator cache) : base(dbConnector) {
            _cache = cache;
        }

        public new async Task<Usuario> CreateAsync(Usuario data)
        {
            _cache.Remove($"List_{nameof(Usuario)}");
            return await base.CreateAsync(data);
        }

        public new async Task<bool> DeleteAsync(int id)
        {
            _cache.Remove($"{nameof(Usuario)}_{id}");
            return await base.DeleteAsync(id);
        }

        public new async Task<Usuario> ReadAsync(int id)
        {
            var dataCache = await _cache.GetAsync<Usuario>($"{nameof(Usuario)}_{id}");
            if (dataCache != null)
                return dataCache;
            return await base.ReadAsync(id);
        }

        public new async Task<IEnumerable<Usuario>> ReadAsync()
        {
            var dataCache = await _cache.GetListAsync<Usuario>($"List_{nameof(Usuario)}");
            if (dataCache != null)
                return dataCache;
            return await base.ReadAsync();
        }

        public new async Task<Usuario> UpdateAsync(int id, Usuario data)
        {
            _cache.Remove($"List_{nameof(Usuario)}");
            _cache.Remove($"{nameof(Usuario)}_{id}");
            return await base.UpdateAsync(id, data);
        }














        public async Task<IEnumerable<Usuario>> ReadUsuarioGeneroAsync()
        {
            string query = $"SELECT * FROM [{nameof(Usuario)}] u INNER JOIN [{nameof(Genero)}] g ON u.{nameof(Usuario.GeneroId)} = g.{nameof(Genero.Id)}";
            var data = await _dbConnector.dbConnection.QueryAsync<Usuario, Genero, Usuario> (query,  map: (usuario, genero) => FuncMapUsuarioGenero(usuario, genero), _dbConnector.dbTransaction);
            return data;
        }

        private readonly Func<Usuario, Genero, Usuario> FuncMapUsuarioGenero = (usuario, genero) =>
        {
            usuario.InitGenero(genero);
            return usuario;
        };
    }
}