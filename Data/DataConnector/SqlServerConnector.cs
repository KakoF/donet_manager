using Domain.Interfaces.Repositories.DataConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataConnector
{
    public class SqlServerConnector : IDbConnector
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
        }
    }
}
