using Data.Interfaces.DataConnector;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Data.DataConnector
{
    public class SqlServerConnector : IDbConnector, IDisposable
    {
        public SqlServerConnector(string connectionString)
        {
            dbConnection = SqlClientFactory.Instance.CreateConnection();
            dbConnection.ConnectionString = connectionString;
        }

        public IDbConnection dbConnection { get; }

        public IDbTransaction dbTransaction { get; set; }

        public IDbTransaction BeginTransaction(IsolationLevel isolation)
        {
            if (dbTransaction != null)
            {
                return dbTransaction;
            }

            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }

            return (dbTransaction = dbConnection.BeginTransaction(isolation));
        }

        public void Dispose()
        {
            dbConnection?.Dispose();
            dbTransaction?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
