using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Infrastructure.HealthCheck
{
    public class SqlConnectionHealthCheck:IHealthCheck
    {
        private string ConnectionString { get;  }
        private DBInformation DBInformationInfo { get; set; }
        public SqlConnectionHealthCheck()
        {
            ConnectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION");
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            var info = new Dictionary<string, object>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                    var command = connection.CreateCommand();
                    command.CommandText = "select 1";
                    await command.ExecuteNonQueryAsync(cancellationToken);

                }
                catch (DbException ex)
                {
                    return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
                }
            }
            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync(cancellationToken);
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT DB_NAME(dbid) as DBName, COUNT(dbid) as NumberOfConnections, loginame as LoginName FROM sys.sysprocesses WHERE dbid > 0 GROUP BY dbid, loginame";
                    var reader = await command.ExecuteReaderAsync(cancellationToken);
                    while (reader.Read())
                    {
                        
                        DBInformationInfo = new DBInformation
                        {
                            Name = reader[0].ToString(),
                            CountConection = reader[1].ToString(),
                            LoginName = reader[2].ToString()
                        };

                        info.Add("Info", DBInformationInfo);
                    }
                }
                catch (DbException ex)
                {
                    return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
                }
            }

            return HealthCheckResult.Healthy("La conexion a la base de datos esta correcta", info);
        }
    }

    public class DBInformation { 
        public string Name { get; set; }
        public string CountConection { get; set; }
        public string LoginName { get; set; }
    }
}
