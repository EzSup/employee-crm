using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM_Management.Dal.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.HasKey(per => per.Id);

            builder.Property(per => per.FNameEn)
                .IsRequired();
            builder.Property(per => per.LNameEn)
                .IsRequired();
            builder.Property(per => per.MNameEn)
                .IsRequired();

            builder.Navigation(per => per.Translate)
                .AutoInclude();
            builder.Navigation(per => per.Partner)
                .IsRequired(false);

            builder.HasOne(per => per.Partner)
                .WithOne(par => par.MainPartner)
                .HasForeignKey<PersonEntity>(per => per.PartnerId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(per => per.Translate)
                .WithOne(trn => trn.Person)
                .HasForeignKey<PersonTranslateEntity>(trn => trn.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
