using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM_Management.Dal.Configurations
{
    public class HubConfiguration : IEntityTypeConfiguration<HubEntity>
    {
        public void Configure(EntityTypeBuilder<HubEntity> builder)
        {
            builder.HasKey(hub => hub.Id);

            builder.Property(hub => hub.Name)
                .IsRequired();
            builder.HasOne(hub => hub.Director)
                .WithOne(emp => emp.DirectedHub)
                .HasForeignKey<HubEntity>(hub => hub.DirectorId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(hub => hub.Leader)
                .WithOne(emp => emp.LeadedHub)
                .HasForeignKey<HubEntity>(hub => hub.LeaderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(hub => hub.DeputyLeader)
                .WithOne(emp => emp.DeputyLeadedHub)
                .HasForeignKey<HubEntity>(hub => hub.DeputyLeaderId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(hub => hub.Employees)
                .WithOne(emp => emp.Hub)
                .HasForeignKey(emp => emp.HubId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
