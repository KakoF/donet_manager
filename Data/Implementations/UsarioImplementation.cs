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

        protected override string InsertQuery => $"INSERT INTO [{nameof(Usuario)}] ([{nameof(Usuario.Nome)}] ,[{nameof(Usuario.Email)}] ,[{nameof(Usuario.DataCriacao)}]) VALUES (@{nameof(Usuario.Nome)}, @{nameof(Usuario.Email)}, @{nameof(Usuario.DataCriacao)})";
        //protected override string InsertQuery => $"INSERT INTO [{nameof(Usuario)}] ([{nameof(Usuario.Nome)}] ,[{nameof(Usuario.Email)}] ,[{nameof(Usuario.DataCriacao)}]) VALUES (@{nameof(Usuario.Nome)}, @{nameof(Usuario.Email)}, @{nameof(Usuario.DataCriacao)})";
        protected override string InsertQueryReturnInserted => $"INSERT INTO [{nameof(Usuario)}] ([{nameof(Usuario.Nome)}] ,[{nameof(Usuario.Email)}] ,[{nameof(Usuario.GeneroId)}], [{nameof(Usuario.DataCriacao)}]) OUTPUT Inserted.* VALUES (@{nameof(Usuario.Nome)}, @{nameof(Usuario.Email)}, @{nameof(Usuario.GeneroId)}, @{nameof(Usuario.DataCriacao)})";
        //protected override string InsertQueryReturnInserted => $"INSERT INTO [{nameof(Usuario)}] ([{nameof(Usuario.Nome)}] ,[{nameof(Usuario.Email)}] ,[{nameof(Usuario.DataCriacao)}]) OUTPUT Inserted.* VALUES (@{nameof(Usuario.Nome)}, @{nameof(Usuario.Email)}, @{nameof(Usuario.DataCriacao)})";
        protected override string UpdateByIdQuery => $"UPDATE [{nameof(Usuario)}] SET {nameof(Usuario.Nome)} = @{nameof(Usuario.Nome)}, {nameof(Usuario.Email)} = @{nameof(Usuario.Email)}, {nameof(Usuario.DataAtualizacao)} = @{nameof(Usuario.DataAtualizacao)} WHERE {nameof(Usuario.Id)} = @{nameof(Usuario.Id)}";
        protected override string DeleteByIdQuery => $"DELETE FROM [{nameof(Usuario)}] WHERE {nameof(Usuario.Id)} = @{nameof(Usuario.Id)}";
        protected override string SelectAllQuery => $"SELECT * FROM [{nameof(Usuario)}]";
        protected override string SelectByIdQuery => $"SELECT * FROM [{nameof(Usuario)}] WHERE {nameof(Usuario.Id)} = @{nameof(Usuario.Id)}";

        protected override bool CreateCache => true;
        protected override bool CreateListCache => true;
        protected override bool ReadCache => true;
        protected override bool ReadListCache => true;
        protected override int MinutesCacheTime => 5;
        protected override string NameDataCache => nameof(Usuario);


        public UsarioImplementation(IDbConnector dbConnector, IRedisIntegrator cache) : base(dbConnector, cache) { }

        //Exemplo de implementação
        public async Task<IEnumerable<Usuario>> ReadUsuarioGeneroAsync()
        {
            string query = $"SELECT * FROM [{nameof(Usuario)}] u INNER JOIN [{nameof(Genero)}] g ON u.{nameof(Usuario.GeneroId)} = g.{nameof(Genero.Id)}";
            var data = await _dbConnector.dbConnection.QueryAsync<Usuario, Genero, Usuario> (query,  map: (usuario, genero) => FuncMapRelation(usuario, genero), _dbConnector.dbTransaction);
            return data;
        }

        private readonly Func<Usuario, Genero, Usuario> FuncMapRelation = (usuario, genero) =>
        {
            usuario.InitGenero(genero);
            return usuario;
        };
    }
}