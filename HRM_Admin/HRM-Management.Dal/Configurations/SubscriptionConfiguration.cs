using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM_Management.Dal.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<SubscriptionEntity>
    {
        public void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
        {
            builder.HasKey(sub => sub.Id);

            builder.Property(sub => sub.TriggerKey)
                .IsRequired();
            builder.Property(sub => sub.JobKey)
                .IsRequired();

            builder.HasOne(sub => sub.Person)
                .WithMany(per => per.Notifications)
                .HasForeignKey(sub => sub.PersonId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
