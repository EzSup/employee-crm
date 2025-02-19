using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM_Management.Dal.Configurations
{
    class BlogConfiguration : IEntityTypeConfiguration<BlogEntity>
    {
        void IEntityTypeConfiguration<BlogEntity>.Configure(EntityTypeBuilder<BlogEntity> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.ContentLink);
        }
    }
}
