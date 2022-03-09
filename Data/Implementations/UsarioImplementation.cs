using Data.Interfaces.DataConnector;
using Data.Interfaces.Redis;
using Data.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class UsarioImplementation : Repository<Usuario>
    {

        protected override string InsertQuery => $"INSERT INTO [{nameof(Usuario)}] VALUES (@{nameof(Usuario.Nome)})";
        protected override string InsertQueryReturnInserted => $"INSERT INTO [{nameof(Usuario)}] OUTPUT INSERTED.* VALUES (@{nameof(Usuario.Nome)})";
        protected override string UpdateByIdQuery => $"UPDATE [{nameof(Usuario)}] SET {nameof(Usuario.Nome)} = @{nameof(Usuario.Nome)} WHERE {nameof(Usuario.Id)} = @{nameof(Usuario.Id)}";
        protected override string DeleteByIdQuery => $"DELETE FROM [{nameof(Usuario)}] WHERE {nameof(Usuario.Id)} = @{nameof(Usuario.Id)}";
        protected override string SelectAllQuery => $"SELECT * FROM [{nameof(Usuario)}]";
        protected override string SelectByIdQuery => $"SELECT * FROM [{nameof(Usuario)}] WHERE {nameof(Usuario.Id)} = @{nameof(Usuario.Id)}";


        public UsarioImplementation(IDbConnector dbConnector, IRedisIntegrator cache) : base(dbConnector, cache) { }
    }
}