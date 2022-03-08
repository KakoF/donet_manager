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
    public class UsuarioRepository : Repository<Usuario>
    {
        private readonly IDbConnector _dbConnector;
        private readonly IRedisIntegrator _cache;

        public UsuarioRepository(IDbConnector dbConnector, IRedisIntegrator cache)
        {
            _dbConnector = dbConnector;
            _cache = cache;
        }

        /*public async Task<bool> DeleteAsync(int id)
        {
            _cache.Remove("Usuarios");
            string sql = " Delete FROM [dbo].[Usuario] Where Id = @Id";
            var delete = await _dbConnector.dbConnection.ExecuteAsync(sql, new { Id = id }, _dbConnector.dbTransaction);
            return Convert.ToBoolean(delete);
        }

        public async Task<Usuario> ReadAsync(int id)
        {
            string sql = "SELECT Id,Nome,Email,DataCriacao,DataAtualizacao FROM [dbo].[Usuario] Where Id = @Id";
            var usuario = await _dbConnector.dbConnection.QueryFirstOrDefaultAsync<Usuario>(sql, new {Id = id}, _dbConnector.dbTransaction);
            return usuario;
        }

        public async Task<IEnumerable<Usuario>> ReadAsync()
        {
            IEnumerable<Usuario> usuariosCache = await _cache.GetListAsync<Usuario>("Usuarios");
            if (usuariosCache == null)
            {
                string sql = "SELECT Id,Nome,Email,DataCriacao,DataAtualizacao FROM [dbo].[Usuario]";
                var usuarios = await _dbConnector.dbConnection.QueryAsync<Usuario>(sql, _dbConnector.dbTransaction);
                _cache.SetList("Usuarios", usuarios);
                return usuarios.ToList();
            }
            return usuariosCache;
           
        }

        public async Task<Usuario> CreateAsync(Usuario data)
        {
            _cache.Remove("Usuarios");
            string sql = @"INSERT INTO [dbo].[Usuario]
                                 ([Nome]
                                 ,[Email]
                                 ,[DataCriacao]) OUTPUT Inserted.Id
                           VALUES
                                 (@Nome
                                 ,@Email
                                 ,@DataCriacao)";

            data.SetId(await _dbConnector.dbConnection.QuerySingleAsync<int>(sql, new
            {
                Nome = data.Nome,
                Email = data.Email,
                DataCriacao = DateTime.Now,
            }, _dbConnector.dbTransaction));

            return data;
        }

        public async Task<Usuario> UpdateAsync(int id, Usuario data)
        {
            _cache.Remove("Usuarios");
            string sql = @"Update [dbo].[Usuario] Set
                                 Nome = @Nome
                                 ,Email = @Email
                                 ,DataAtualizacao = @DataAtualizacao Where Id = @Id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = data.Id,
                Nome = data.Nome,
                Email = data.Email,
                DataAtualizacao = DateTime.Now,
            }, _dbConnector.dbTransaction);
            return data;
        }*/
        

    }
}
