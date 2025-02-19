namespace HRM_Management.Dal.Entities
{
    public class BlogEntity
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? ContentLink { get; set; }

        public string S3Key { get; set; }
    }
}
