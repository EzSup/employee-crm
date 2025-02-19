namespace HRM_Management.Dal.Entities
{
    public class ExEmployeeEntity
    {
        public int Id { get; set; }

        public string? Comment { get; set; }
        public DateTime LeavingDate { get; set; }

        public int EmployeeId { get; set; }

        public required EmployeeEntity Employee { get; set; }
    }
}
