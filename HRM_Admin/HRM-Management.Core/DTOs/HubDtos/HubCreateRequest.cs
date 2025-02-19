namespace HRM_Management.Core.DTOs.HubDtos
{
    public record HubCreateRequest(
        string Name,
        int[] MemberIds,
        int LeaderId,
        int DeputyLeaderId,
        int DirectorId);
}
