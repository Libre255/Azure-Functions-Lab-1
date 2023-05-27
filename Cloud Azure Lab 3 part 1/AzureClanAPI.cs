using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Cloud_Azure_Lab_3_part_1
{
    public class AzureClanAPI
    {
        
        private readonly ILogger _logger;

        public AzureClanAPI(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AzureClanAPI>();
        }

        [Function("AzureClanAPI")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = "AzureClanAPI")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
