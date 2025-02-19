namespace HRM_Management.Dal.Entities
{
    public class HubEntity
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int? LeaderId { get; set; }
        public int? DeputyLeaderId { get; set; }
        public int? DirectorId { get; set; }

        public EmployeeEntity? Leader { get; set; }
        public EmployeeEntity? DeputyLeader { get; set; }
        public EmployeeEntity? Director { get; set; }

        public List<EmployeeEntity>? Employees { get; set; }
    }
}
