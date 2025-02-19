using HRM_Management.Dal.Entities.Enums;

namespace HRM_Management.Dal.Entities
{
    public class PartnerEntity
    {
        public int Id { get; set; }

        public required string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }

        public PersonEntity? MainPartner { get; set; }
    }
}
