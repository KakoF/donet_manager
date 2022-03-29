using Dapper;
using Data.Interfaces.DataConnector;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Implementations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class GeneroImplementation : Repository<Genero>, IGeneroImplementation
    {
        protected override string InsertQuery => "";
        protected override string InsertQueryReturnInserted => "";
        protected override string UpdateByIdQuery => "";
        protected override string DeleteByIdQuery => "";
        protected override string SelectAllQuery => $"SELECT * FROM [{nameof(Genero)}]";
        protected override string SelectByIdQuery => "";

        public GeneroImplementation(IDbConnector dbConnector) : base(dbConnector) {}

        public async Task<IEnumerable<Genero>> QueryGraphql()
        {
            var generoMap = new Dictionary<int, Genero>();
            string query = $"SELECT * FROM [{nameof(Genero)}] g LEFT JOIN [{nameof(Usuario)}] u ON g.{nameof(Genero.Id)} = u.{nameof(Usuario.GeneroId)}";
            var data = await _dbConnector.dbConnection.QueryAsync<Genero, Usuario, Genero>(query,
                 map: (genero, usuario) =>
                 {
                     if (usuario == null)
                         return genero;
                     usuario.SetGeneroId(genero.Id);
                     if (generoMap.TryGetValue(genero.Id, out Genero existingGenero))
                     {
                         genero = existingGenero;
                     }
                     else
                     {
                         genero.UsuariosListInit();
                         generoMap.Add(genero.Id, genero);

                     }

                     genero.Usuarios.Add(usuario);
                     return genero;
                 }, 
                 
                 $" u.{nameof(Usuario.Id)}");
            return data.ToList().Distinct();
        }
        public async Task<Genero> QueryGraphql(int id)
        {
            var generoMap = new Dictionary<int, Genero>();
            string query = $"SELECT * FROM [{nameof(Genero)}] g INNER JOIN [{nameof(Usuario)}] u ON g.{nameof(Genero.Id)} = u.{nameof(Usuario.GeneroId)} Where g.{nameof(Genero.Id)} = @Id";
            var data = await _dbConnector.dbConnection.QueryAsync<Genero, Usuario, Genero>(query,
                 map: (genero, usuario) =>
                 {
                     if (usuario == null)
                         return genero;
                     usuario.SetGeneroId(genero.Id);
                     if (generoMap.TryGetValue(genero.Id, out Genero existingGenero))
                     {
                         genero = existingGenero;
                     }
                     else
                     {
                         genero.UsuariosListInit();
                         generoMap.Add(genero.Id, genero);

                     }

                     genero.Usuarios.Add(usuario);
                     return genero;
                 },
                param: new {Id = id});
            return data.FirstOrDefault();
        }
    }
}