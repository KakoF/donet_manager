using System;
using System.Data;

namespace Data.Interfaces.DataConnector
{
    // essa interface deve ficar no projeto Data
    public interface IDbConnector : IDisposable
    {
        IDbConnection dbConnection { get; }
        IDbTransaction dbTransaction { get; set; }
        IDbTransaction BeginTransaction(IsolationLevel isolation);
    }
}
