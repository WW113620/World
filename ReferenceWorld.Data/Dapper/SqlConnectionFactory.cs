using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ReferenceWorld.Data.Dapper
{
    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly IConnectionStringManager _connectionStringManager;

        public SqlConnectionFactory(IConnectionStringManager connectionStringManager)
        {
            _connectionStringManager = connectionStringManager;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionStringManager.ConnectionString);
        }
    }
}
