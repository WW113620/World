using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReferenceWorld.Data.Dapper
{
    public class ConnectionStringManager : IConnectionStringManager
    {
        public ConnectionStringManager(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; private set; }
    }
}
