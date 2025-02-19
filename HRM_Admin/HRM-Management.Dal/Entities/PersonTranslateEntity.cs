namespace HRM_Management.Dal.Entities
{
    public class PersonTranslateEntity
    {
        public required string FNameUk { get; set; }
        public required string LNameUk { get; set; }
        public required string MNameUk { get; set; }

        public int PersonId { get; set; }

        public PersonEntity? Person { get; set; }
    }
}
