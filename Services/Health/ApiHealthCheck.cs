using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace logTesting.Services.Health
{
    public class ApiHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, System.Threading.CancellationToken cancellationToken = default)
        {
            var url = "https://api.chucknorris.io/jokes/random";

            var client = new RestClient();
            var request = new RestRequest(url, Method.Get);

            var response =  client.Execute(request);        
            if(response.IsSuccessful)
            {
                return Task.FromResult(HealthCheckResult.Healthy("API is up and running"));
            }
            else
            {
                return Task.FromResult(HealthCheckResult.Unhealthy("API is down"));
            }
        }
    }
}