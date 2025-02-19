using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM_Management.Dal.Configurations
{
    public class PersonsTranslateConfiguration : IEntityTypeConfiguration<PersonTranslateEntity>
    {
        public void Configure(EntityTypeBuilder<PersonTranslateEntity> builder)
        {
            builder.HasKey(tra => tra.PersonId);

            builder.Property(tra => tra.FNameUk)
                .IsRequired();
            builder.Property(tra => tra.LNameUk)
                .IsRequired();
            builder.Property(tra => tra.MNameUk)
                .IsRequired();

            builder.HasOne(tra => tra.Person)
                .WithOne(per => per.Translate)
                .HasForeignKey<PersonTranslateEntity>(tra => tra.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
