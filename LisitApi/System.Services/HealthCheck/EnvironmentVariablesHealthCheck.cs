using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Infrastructure.HealthCheck
{
    public class EnvironmentVariablesHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                //Se verifica que las variables de entorno existan.
                var ev = Environment.GetEnvironmentVariable("PHOTO_EMAIL");
                if (ev == null)
                    return Task.FromResult(
                                new HealthCheckResult(
                                context.Registration.FailureStatus, "Variable de entorno [PHOTO_EMAIL] no esta configurada."));

                var ev2 = Environment.GetEnvironmentVariable("URL_FRONTEND");
                if (ev2 == null)
                    return Task.FromResult(
                                new HealthCheckResult(
                                context.Registration.FailureStatus, "Variable de entorno [URL_FRONTEND] no esta configurada."));
                return Task.FromResult(
                    HealthCheckResult.Healthy("Todas las variables de entornos estan cargadas.")); 

                var ev3 = Environment.GetEnvironmentVariable("SQL_CONNECTION");
                if (ev3 == null)
                    return Task.FromResult(
                                new HealthCheckResult(
                                context.Registration.FailureStatus, "Variable de entorno [SQL_CONNECTION] no esta configurada."));
                return Task.FromResult(
                    HealthCheckResult.Healthy("Todas las variables de entornos estan cargadas."));


            }
            catch
            {
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus, "An unhealthy result."));
            }
        }
    }
}
