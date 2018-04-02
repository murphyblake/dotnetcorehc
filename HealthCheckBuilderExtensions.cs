using Microsoft.Extensions.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnetcorehc
{
    public static class HealthCheckBuilderExtensions
    {
        public static HealthCheckBuilder AddMySQLCheck(this HealthCheckBuilder builder, string name, string connectionstring)
        {
            builder.AddCheck(name, new MySQLHealthCheck(connectionstring));
            return builder;
        }

        public static HealthCheckBuilder AddMySQLCheck(this HealthCheckBuilder builder, string name ,string connectionstring, TimeSpan cacheDuration)
        {
            builder.AddCheck(name, new MySQLHealthCheck(connectionstring), cacheDuration);
            return builder;
        }
    }
}
