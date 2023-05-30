using System.Net;
using Cloud_Azure_Lab_3_part_1.Models;
using Cloud_Azure_Lab_3_part_1.Services.AzureMembersService;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

namespace Cloud_Azure_Lab_3_part_1
{
    public class AzureClanAPI
    {
        public IAzureMembersService AzureClanDB;
        public AzureClanAPI(IAzureMembersService _AzureClanDB)
        {
            AzureClanDB = _AzureClanDB;
        }
        [Function("GetAllAzureMembers")]
        public HttpResponseData GetAllAzureMembers([HttpTrigger(AuthorizationLevel.Function, "get", Route = "AzureClanAPI")] HttpRequestData req)
        {
            List<AzureMember> AzureMembers = AzureClanDB.GetAllAzureMembers();
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.WriteAsJsonAsync(AzureMembers);
            return response;
        }
        
        [Function("GetAzureMemberById")]
        public HttpResponseData GetAzureMemberById
        ([HttpTrigger(AuthorizationLevel.Function, "get", Route = "AzureClanAPI/{Id}")] HttpRequestData req, string Id)
        {
            var SelectedMember = AzureClanDB.GetAzureMemberById(Id);
            var response = req.CreateResponse();
            if (SelectedMember == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.WriteAsJsonAsync(new { message = $"Member with Id ({Id}) was not found." });
                return response;
            }
            response.WriteAsJsonAsync(SelectedMember);
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        [Function("AddAzureMember")]
        public HttpResponseData AddAzureMember([HttpTrigger(AuthorizationLevel.Function, "post", Route = "AzureClanAPI")] HttpRequestData req)
        {
            var response = req.CreateResponse();
            var reqData = new StreamReader(req.Body).ReadToEnd();
            var reqJsonData = JsonConvert.DeserializeObject<CreateAzureMember>(reqData);

            bool ModelIsValid = CreateAzureMember_Validation(reqJsonData);

            if (!ModelIsValid)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.WriteAsJsonAsync(new { message = "Insert all required properties" });
                return response;
            }

            var AzureMemberAdded = AzureClanDB.AddAzureMember(reqJsonData);
            response.WriteAsJsonAsync(AzureMemberAdded);
            response.StatusCode = HttpStatusCode.Created;
            return response;
        }

        [Function("UpdateAzureMember")]
        public HttpResponseData UpdateAzureMember([HttpTrigger(AuthorizationLevel.Function, "put", Route = "AzureClanAPI/{Id}")] HttpRequestData req, string Id)
        {
            var response = req.CreateResponse();
            var reqData = new StreamReader(req.Body).ReadToEnd();
            var reqJsonData = JsonConvert.DeserializeObject<CreateAzureMember>(reqData);
            if(reqJsonData == null)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.WriteAsJsonAsync(new { message = "Insert all required properties" });
                return response;
            }
            
            var SelectedMember = AzureClanDB.GetAzureMemberById(Id);
            if (SelectedMember == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.WriteAsJsonAsync(new { messeage = $"Member Id ({Id}) not found" });
                return response;
            }

            List<AzureMember> UpdatedAzureClanDB = AzureClanDB.UpdateAzureMember(Id, reqJsonData);
            response.WriteAsJsonAsync(UpdatedAzureClanDB);
            response.StatusCode = HttpStatusCode.Created;
            
            return response;
        }

        [Function("Delet a AzureMember")]
        public HttpResponseData DeletAzureMember(
         [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "AzureClanAPI/{Id}") ] HttpRequestData req, string Id
        )
        {
            var response = req.CreateResponse();
            var DeletedMember = AzureClanDB.DeletAzureMember(Id);

            if (DeletedMember == false)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.WriteAsJsonAsync(new {message = $"Id ({Id}) not found" });
                return response;
            }

            response.WriteAsJsonAsync(AzureClanDB.GetAllAzureMembers());
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        internal bool CreateAzureMember_Validation(CreateAzureMember? AzureMemberModel)
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
