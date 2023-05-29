using System.Net;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Azure;
using Cloud_Azure_Lab_3_part_1.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

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
        public static HttpResponseData GetAllAzureMembers([HttpTrigger(AuthorizationLevel.Function, "get", Route = "AzureClanAPI")] HttpRequestData req)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(AzureMembers);
            return response;
        }
        
        [Function("GetAzureMemberById")]
        public static HttpResponseData GetAzureMemberById
        ([HttpTrigger(AuthorizationLevel.Function, "get", Route = "AzureClanAPI/{Id}")] HttpRequestData req, string Id)
        {
            AzureMember? SelectedAzureMember = AzureMembers.FirstOrDefault(member => member.Id == Id);
            if(SelectedAzureMember == null)
            {
                var response = req.CreateResponse(HttpStatusCode.NotFound);
                response.WriteString($"Member with the ID ({Id}) was not found");
                return response;
            }
            else
            {
                var response = req.CreateResponse(HttpStatusCode.OK);
                response.WriteAsJsonAsync(SelectedAzureMember);
                return response;
            }
        }

        [Function("AddAzureMember")]
        public static HttpResponseData AddAzureMember([HttpTrigger(AuthorizationLevel.Function, "post", Route = "AzureClanAPI")] HttpRequestData req)
        {
            var response = req.CreateResponse();
            try
            {
                var reqData = new StreamReader(req.Body).ReadToEnd();
                var reqJsonData = JsonConvert.DeserializeObject<CreateAzureMember>(reqData);

                bool ModelIsValid = CreateAzureMember_Validation(reqJsonData);
                if (!ModelIsValid)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    throw new Exception("Insert required properties");
                }

                AzureMember NewMember = new(reqJsonData.Name, reqJsonData.Age, reqJsonData.Power);
                AzureMembers.Add(NewMember);

                response.WriteAsJsonAsync(NewMember);
                response.StatusCode = HttpStatusCode.Created;
                
                return response;
            }catch(Exception ex)
            {
                response.WriteString(ex.Message.ToString());
                return response;
            }
        }

        [Function("UpdateAzureMember")]
        public static HttpResponseData UpdateAzureMember([HttpTrigger(AuthorizationLevel.Function, "put", Route = "AzureClanAPI/{Id}")] HttpRequestData req, string Id)
        {
            var response = req.CreateResponse();
            AzureMember? SelectedMember = AzureMembers.FirstOrDefault(member => member.Id == Id);

            if(SelectedMember == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.WriteAsJsonAsync(new { Message = $"Couldn't find the Id ({Id})"});
                return response;
            }

            var reqData = new StreamReader(req.Body).ReadToEnd();
            var reqDataJson = JsonConvert.DeserializeObject<CreateAzureMember>(reqData);

            bool ModelIsValid = CreateAzureMember_Validation(reqDataJson);
            if (ModelIsValid)
            {
                SelectedMember.Name = reqDataJson.Name;
                SelectedMember.Age = reqDataJson.Age;
                SelectedMember.Power = reqDataJson.Power;

                response.WriteAsJsonAsync(AzureMembers);
                response.StatusCode = HttpStatusCode.Created;
                return response;
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.WriteString("Not worked :ccc:c");
                return response;
            }

        }

        [Function("Delet a AzureMember")]
        public static HttpResponseData DeletAzureMember(
         [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "AzureClanAPI/{Id}") ] HttpRequestData req, string Id
        )
        {
            var response = req.CreateResponse();
            var SelectedMember = AzureMembers.FirstOrDefault(member => member.Id == Id);
            if(SelectedMember == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }

            AzureMembers.Remove(SelectedMember);
            response.WriteAsJsonAsync(AzureMembers);
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        internal static bool CreateAzureMember_Validation(CreateAzureMember? AzureMemberModel)
        {
            if (AzureMemberModel == null || AzureMemberModel.Name == null || AzureMemberModel.Age == 0 || AzureMemberModel.Power == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
