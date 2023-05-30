using Cloud_Azure_Lab_3_part_1.Models;

namespace Cloud_Azure_Lab_3_part_1.Services.AzureMembersService
{
    public class AzureMembersService : IAzureMembersService
    {
        public static List<AzureMember> AzureMembers = new()
        {
            new AzureMember("Goku", 90, 9000),
            new AzureMember("Vegeta", 99, 7000),
            new AzureMember("Pikachu", 20, 600),
            new AzureMember("Superman", 30, 5000),
            new AzureMember("Spiderman", 25, 3000),
        };
        public List<AzureMember> GetAllAzureMembers() => AzureMembers;
        public AzureMember? GetAzureMemberById(string Id)
        {
            var SelectedMember = AzureMembers.FirstOrDefault(member => member.Id == Id);
            if (SelectedMember == null) return null;
            return SelectedMember;
        }
        public AzureMember AddAzureMember(CreateAzureMember MemberInfo)
        {
            AzureMember NewMember = new(MemberInfo.Name, MemberInfo.Age, MemberInfo.Power);
            AzureMembers.Add(NewMember);
            return NewMember;
        }

        public List<AzureMember> UpdateAzureMember(string Id, CreateAzureMember MemberInfo)
        {
            AzureMember SelectedMember = AzureMembers.First(member => member.Id == Id);

            SelectedMember.Name = MemberInfo.Name == null ? SelectedMember.Name : MemberInfo.Name;
            SelectedMember.Age = MemberInfo.Age == 0 ? SelectedMember.Age : MemberInfo.Age;
            SelectedMember.Power = MemberInfo.Power == 0 ? SelectedMember.Power : MemberInfo.Power;

            return AzureMembers;
        }
        public bool DeletAzureMember(string Id)
        {
            AzureMember? SelectedMember = AzureMembers.FirstOrDefault(member => member.Id == Id);
            if (SelectedMember == null) return false;
            AzureMembers.Remove(SelectedMember);
            return true;
        }
    }
}
