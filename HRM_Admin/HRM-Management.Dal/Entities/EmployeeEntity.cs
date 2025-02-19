using HRM_Management.Dal.Entities.Enums;

namespace HRM_Management.Dal.Entities
{
    public class EmployeeEntity
    {
        public int Id { get; set; }

        public TechLevel TechLevel { get; set; }
        public DateTime DateOfComing { get; set; } = DateTime.UtcNow;
        public List<string>? Documents { get; set; }
        public string?  Mail { get; set; }
        public string? MentorName { get; set; }

        public int PersonId { get; set; }
        public int? HirerId { get; set; }
        public int? HubId { get; set; }

        public PersonEntity? Person { get; set; }
        public UserEntity? Hirer { get; set; }
        public HubEntity? Hub { get; set; }
        public HubEntity? DirectedHub { get; set; }
        public HubEntity? LeadedHub { get; set; }
        public HubEntity? DeputyLeadedHub { get; set; }
        public ExEmployeeEntity? ExEmployee { get; set; }
    }
}
