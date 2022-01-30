using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.DataConnector
{
    public interface IUnitOfWork
    {
        IDbConnector dbConnector { get; }

        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
