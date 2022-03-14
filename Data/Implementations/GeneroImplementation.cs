using Data.Interfaces.DataConnector;
using Data.Interfaces.Redis;
using Data.Repositories;
using Domain.Entities;
using Domain.Interfaces.Implementations;

namespace Data.Implementations
{
    public class GeneroImplementation : Repository<Genero>, IGeneroImplementation
    {
        protected readonly IRedisIntegrator _cache;
        protected override string InsertQuery => "";
        protected override string InsertQueryReturnInserted => "";
        protected override string UpdateByIdQuery => "";
        protected override string DeleteByIdQuery => "";
        protected override string SelectAllQuery => $"SELECT * FROM [{nameof(Genero)}]";
        protected override string SelectByIdQuery => "";

        public GeneroImplementation(IDbConnector dbConnector, IRedisIntegrator cache) : base(dbConnector) {
         _cache = cache;
        }
    }
}