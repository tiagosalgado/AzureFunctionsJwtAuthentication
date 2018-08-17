
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using JwtAuthenticationFx.Authentication;
using System.Linq;
using System.Net.Http.Headers;

namespace JwtAuthenticationFx
{
    public static class TestJwtFx
    {
        [FunctionName("test-jwt")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger log)
        {
            try
            {
                if (
                    !req.Headers.TryGetValue("Authorization", out var value) || 
                    !AuthenticationHeaderValue.TryParse(value.First(), out var authHeader))
                {
                    return new UnauthorizedResult();
                }
                
                var cp = await JwtHelper.ValidateToken(authHeader);
                if (cp == null)
                {
                    return new UnauthorizedResult();
                }

                log.LogInformation(cp.Identity.Name);
                return await Process(req, log);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        static async Task<IActionResult> Process(HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult($"Hi!");
        }
    }
}
