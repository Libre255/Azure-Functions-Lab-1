using Cloud_Azure_Lab_3_part_1.Models;
using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud_Azure_Lab_3_part_1.Services.AzureMembersService
{
    internal interface IAzureMembersService
    {
        public HttpResponseData GetAzureMemberById();
        public HttpResponseData AddAzureMember();
        public HttpResponseData UpdateAzureMember();
        public HttpResponseData DeletAzureMember();
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
