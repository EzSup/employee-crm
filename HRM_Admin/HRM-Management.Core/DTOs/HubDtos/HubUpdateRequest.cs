namespace HRM_Management.Core.DTOs.HubDtos
{
    public record HubUpdateRequest(
        int Id = 0,
        string? Name = null,
        int? DirectorId = null,
        int? LeaderId = null,
        int? DeputyLeaderId = null,
        int[] MemberIds = null!)
    {
        public HubUpdateRequest() : this(0, null, null, null, null, Array.Empty<int>()) { }
    }
}
