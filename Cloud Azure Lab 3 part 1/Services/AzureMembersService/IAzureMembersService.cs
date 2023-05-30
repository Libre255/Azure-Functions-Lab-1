using Cloud_Azure_Lab_3_part_1.Models;

namespace Cloud_Azure_Lab_3_part_1.Services.AzureMembersService
{
    public interface IAzureMembersService
    {
        public List<AzureMember> GetAllAzureMembers();
        public AzureMember? GetAzureMemberById(string Id);
        public AzureMember AddAzureMember(CreateAzureMember MemberInfo);
        public List<AzureMember> UpdateAzureMember(string Id, CreateAzureMember MemberInfo);
        public bool DeletAzureMember(string Id);
    }
}
