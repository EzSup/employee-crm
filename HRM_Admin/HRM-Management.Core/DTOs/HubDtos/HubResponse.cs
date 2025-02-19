namespace HRM_Management.Core.DTOs.HubDtos
{
    public class HubResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DirectorId { get; set; }
        public int? LeaderId { get; set; }
        public int? DeputyLeaderId { get; set; }
        public string? DirectorName { get; set; }
        public string? LeaderName { get; set; }
        public string? DeputyLeaderName { get; set; }
        public List<HubMember> Employees { get; set; } = new List<HubMember>();
    }
}
