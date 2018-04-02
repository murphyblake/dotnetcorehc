using Microsoft.Extensions.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace dotnetcorehc
{
    public class MySQLHealthCheck : IHealthCheck
    {
        readonly string _connectionString = String.Empty;

        public MySQLHealthCheck(string ConnectionString)
        {
            _connectionString = ConnectionString ?? throw new NullReferenceException("Connection string cannot be null");
        }

        public ValueTask<IHealthCheckResult> CheckAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            CheckStatus status = CheckStatus.Unknown;
            string description = "UNKNOWN";

            try
            {
                using (var checkConnection = new MySql.Data.MySqlClient.MySqlConnection(_connectionString))
                {
                    checkConnection.Open();
                    status = checkConnection.State == System.Data.ConnectionState.Open ? CheckStatus.Healthy : CheckStatus.Unhealthy;
                }
                return new ValueTask<IHealthCheckResult>(HealthCheckResult.FromStatus(status, description));
            }
            catch (Exception e)
            {
                return new ValueTask<IHealthCheckResult>(HealthCheckResult.FromStatus(CheckStatus.Unhealthy, $"Exception {e.Message}"));
            }
            

        }
    }
}
