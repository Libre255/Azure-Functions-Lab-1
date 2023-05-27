using System.Net;
using Cloud_Azure_Lab_3_part_1.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Cloud_Azure_Lab_3_part_1
{
    public static class AzureClanAPI
    {
        static private List<AzureMember> AzureMembers = new()
        {
            new AzureMember("Goku", 90, 9000),
            new AzureMember("Vegeta", 99, 7000),
            new AzureMember("Pikachu", 20, 600),
            new AzureMember("Superman", 30, 5000),
            new AzureMember("Spiderman", 25, 3000),
        };


        [Function("GetAllAzureMembers")]
        public static async Task<List<AzureMember>> Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "AzureClanAPI")] HttpRequestData req)
        {

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            response.WriteString("Welcome to Azure Functions!");

            return null;
        }
        [Function("AddAzureMember")]
        public static HttpResponseData Awa([HttpTrigger(AuthorizationLevel.Function, "post", Route = "AzureClanAPI")] HttpRequestData req)
        {

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
