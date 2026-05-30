using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Microsoft.Data.SqlClient;
using System.Data;
using VentasLimpieza.Core.Enum;
using VentasLimpieza.Core.Interfaces;

namespace VentasLimpieza.Infrastructure.Repositories
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IConfiguration _config;
        private readonly string _sqlConn;
        private readonly string _mySqlConn;
        public DataBaseProvider Provider { get; }

        public DbConnectionFactory(IConfiguration config)
        {
            _config = config;
            _sqlConn = _config.GetConnectionString("ConnectionSqlServer")
                ?? string.Empty;
            _mySqlConn = _config.GetConnectionString("ConnectionMySql")
                ?? string.Empty;

            var providerStr = _config.GetSection("DatabaseProvider").Value
                ?? "MySql";

            Provider = providerStr.Equals("MySql", StringComparison.OrdinalIgnoreCase)
                ? DataBaseProvider.MySql
                : DataBaseProvider.SqlServer;
        }


        public IDbConnection CreateConnection()
        {
            return Provider switch
            {
                DataBaseProvider.MySql => new MySqlConnection(_mySqlConn),
                _ => new SqlConnection(_sqlConn)
            };
        }
    }
}
