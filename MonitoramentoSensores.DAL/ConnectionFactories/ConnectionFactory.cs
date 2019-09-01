using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoramentoSensores.DAL.ConnectionFactories
{
    class ConnectionFactory
    {
        public static IDbConnection GetConnection(string database)
        {
            //var a = ConfigurationManager.ConnectionStrings[database].ConnectionString;

            //string server = WebConfigurationManager.AppSettings["MyServer"];
            //var connectionString = Environment.GetEnvironmentVariable("MyDB");

            var connectionString = string.Format(@"Data Source=LAPTOP-1REIAA4J;Initial Catalog={0};Integrated Security=True;", database);

            return new SqlConnection(connectionString);
        }
    }
}
