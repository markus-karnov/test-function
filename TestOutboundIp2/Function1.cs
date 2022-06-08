using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;

namespace TestOutboundIp
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var httpClient = new HttpClient();
            var httpRes = await httpClient.GetAsync("https://ifconfig.me");
            var httpBody = await httpRes.Content.ReadAsStringAsync();

            log.LogInformation($"found {httpBody}");
            
            return new OkObjectResult(httpBody);
        }
    }
}
