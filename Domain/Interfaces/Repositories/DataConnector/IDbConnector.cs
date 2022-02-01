using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories.DataConnector
{
    // essa interface deve ficar no projeto Data
    public interface IDbConnector : IDisposable
    {
        IDbConnection dbConnection { get; }
        IDbTransaction dbTransaction { get; set; }
        IDbTransaction BeginTransaction(IsolationLevel isolation);
    }
}
