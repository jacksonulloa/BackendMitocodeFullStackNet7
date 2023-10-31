using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net.NetworkInformation;

namespace GamerSellStore.Api.HealthCheck.HealthChecks
{
    public class PingHealthCheck : IHealthCheck
    {
        public string host;
        public PingHealthCheck(string _host) { host = _host; }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            var ping = new Ping();
            HealthCheckResult healthCheckResult = new HealthCheckResult();
            try
            {
                var reply = await ping.SendPingAsync(host, 1000);
                switch(reply.Status)
                {
                    case IPStatus.Success:
                        healthCheckResult = HealthCheckResult.Healthy($"Respuesta del host {host} dentro del tiempo esperado"); break;
                    case IPStatus.BadDestination:
                        healthCheckResult = HealthCheckResult.Unhealthy($"El host {host} no es accesible"); break;
                    case IPStatus.TimedOut:
                    case IPStatus.TimeExceeded:
                        healthCheckResult = HealthCheckResult.Unhealthy($"El host {host} no responde rapidamente"); break;
                    default:
                        healthCheckResult = HealthCheckResult.Unhealthy($"El host {host} no es accesible de ninguna forma"); break;
                }
            }
            catch (Exception ex)
            {
                healthCheckResult = HealthCheckResult.Unhealthy(ex.Message);
            }
            return healthCheckResult;
        }
    }
}
