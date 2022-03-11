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
    public class GeneroImplementation : Repository<Genero>, IGeneroImplementation
    {

        protected override string InsertQuery => "";
        protected override string InsertQueryReturnInserted => "";
        protected override string UpdateByIdQuery => "";
        protected override string DeleteByIdQuery => "";
        protected override string SelectAllQuery => $"SELECT * FROM [{nameof(Genero)}]";
        protected override string SelectByIdQuery => "";

        protected override bool CreateCache => false;
        protected override bool CreateListCache => false;
        protected override bool ReadCache => false;
        protected override bool ReadListCache => false;
        protected override int MinutesCacheTime => 0;
        protected override string NameDataCache => nameof(Genero);


        public GeneroImplementation(IDbConnector dbConnector, IRedisIntegrator cache) : base(dbConnector, cache) { }

        
    }
}