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
    // Nesta classe vc manipula cache e SQL. Num cenário de simplicidade, OK!
    // Ainda assim, o melhor indicado é vc separá-los, usando uma abstração pra cada um. Por exemplo: IUsuarioSqlRepository e IUsuarioRedisRepository (ambas herdando de IUsuarioRepository).
    // Na implementação de IUsuarioRedisRepository, vc tem a referência do IUsuarioSqlRepository. 
    // O exemplo abaixo, é um design pattern chamado decorator:

    // Obs1: vc não precisa das propriedades da classe base:
    //protected abstract string InsertQuery { get; }
    //protected abstract string InsertQueryReturnInserted { get; }
    //protected abstract string UpdateByIdQuery { get; }
    //protected abstract string DeleteByIdQuery { get; }
    //protected abstract string SelectByIdQuery { get; }
    //protected abstract string SelectAllQuery { get; }

    // Obs2: Na configuração da injeção de dependencia, vc deve manter algo assim: services.AddSingleton<IUsarioImplementation, UsuarioRedisRepository>
    // Aqui vc está configurando que a sua implementação correta (primária) é a do Redis de tal forma que no domínio vc não deve saber se vai gravar os dados em cache, sql ou qualquer outra opção.


    public interface IUsuarioRedisRepository : IUsarioImplementation { }
    public interface IUsuarioSqlRepository : IUsarioImplementation { }
    public class UsuarioRedisRepository : IUsuarioRedisRepository
    {
        private readonly IUsuarioSqlRepository _sql;
        protected readonly IRedisIntegrator _cache;

        public UsuarioRedisRepository(IUsuarioSqlRepository sql, IRedisIntegrator cache)
        {
            _sql = sql;
            _cache = cache;
        }

        public async Task<Usuario> CreateAsync(Usuario data)
        {

            _cache.Remove($"List_{nameof(Usuario)}");
            return await _sql.CreateAsync(data);            
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> ReadAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> ReadUsuarioGeneroAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> UpdateAsync(int id, Usuario data)
        {
            throw new NotImplementedException();
        }
    }


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
            _cache.Remove($"List_{nameof(Usuario)}");
            _cache.Remove($"{nameof(Usuario)}_{id}");
            return await base.DeleteAsync(id);
        }

        public new async Task<Usuario> ReadAsync(int id)
        {
            var dataCache = await _cache.GetAsync<Usuario>($"{nameof(Usuario)}_{id}");
            if (dataCache != null)
                return dataCache;
            
            var data = await base.ReadAsync(id);
            _cache.Set($"{nameof(Usuario)}_{id}", data);
            return data;
        }

        public new async Task<IEnumerable<Usuario>> ReadAsync()
        {
            var dataCache = await _cache.GetListAsync<Usuario>($"List_{nameof(Usuario)}");
            if (dataCache != null)
                return dataCache;

            var data = await base.ReadAsync();
            _cache.SetList($"List_{nameof(Usuario)}", data);
            return data;
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