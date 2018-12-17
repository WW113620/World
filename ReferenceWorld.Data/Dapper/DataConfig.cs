using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace ReferenceWorld.Data.Dapper
{
    public class DataConfig
    {
        public static string GetPrimaryConnectionStringKey()
        {
            return "ConnectionString";
        }

        public static string GetPrimaryConnectionString(string connectionKey="")
        {
            string connectionStringKey = string.Empty;
            if (string.IsNullOrEmpty(connectionKey))
            {
                connectionStringKey = GetPrimaryConnectionStringKey();
            }
            else {
                connectionStringKey = connectionKey;
            }           

            if (ConfigurationManager.ConnectionStrings[connectionStringKey] == null)
                throw new ConfigurationErrorsException(
                    "Missing connectionstring for key '" + connectionStringKey + "', add one to the web.config");
            string connectionString = ConfigurationManager.ConnectionStrings[connectionStringKey].ConnectionString;

            return connectionString;
        }

        private static bool IsLocalDataBase(string connectionString)
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
            return sqlConnectionStringBuilder.DataSource.StartsWith("(LocalDb)");
        }

        private static string GetMasterConnectionString(string connectionString)
        {
            var builder = new SqlConnectionStringBuilder(connectionString) { InitialCatalog = "master" };
            return builder.ConnectionString;
        }

    }
}
