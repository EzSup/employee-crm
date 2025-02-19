using HRM_Management.Dal.Entities.Enums;

namespace HRM_Management.Dal.Entities
{
    public class ChildEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        public int ParentId { get; set; }

        public PersonEntity? Parent { get; set; }
    }
}
