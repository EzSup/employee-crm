using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM_Management.Dal.Configurations
{
    internal class PartnerConfiguration : IEntityTypeConfiguration<PartnerEntity>
    {
        public void Configure(EntityTypeBuilder<PartnerEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(par => par.Name)
                .IsRequired();
            builder.Property(par => par.BirthDate)
                .IsRequired();
        }
    }
}
