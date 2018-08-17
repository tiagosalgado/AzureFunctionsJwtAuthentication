using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using JwtAuthetnicationV1.Authentication;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace JwtAuthetnicationV1
{
    public static class TestJwtFx
    {
        [FunctionName("test-jwt-v1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                if (
                    req.Headers.Authorization == null)
                {
                    return req.CreateResponse(HttpStatusCode.Unauthorized);
                }

                var cp = await JwtHelper.ValidateToken(req.Headers.Authorization);
                if (cp == null)
                {
                    return req.CreateResponse(HttpStatusCode.Unauthorized);
                }

                log.Info(cp.Identity.Name);
                return await Process(req, log);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        static async Task<HttpResponseMessage> Process(HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            return req.CreateResponse(HttpStatusCode.OK, "Hi!");
        }
    }
}
