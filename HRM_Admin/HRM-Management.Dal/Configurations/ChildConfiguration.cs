using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM_Management.Dal.Configurations
{
    public class ChildConfiguration : IEntityTypeConfiguration<ChildEntity>
    {
        public void Configure(EntityTypeBuilder<ChildEntity> builder)
        {
            builder.HasKey(builder => builder.Id);

            builder.Property(child => child.Name)
                .IsRequired();
            builder.Property(child => child.BirthDate)
                .IsRequired();
            builder.Property(child => child.Gender)
                .IsRequired();

            builder.HasOne(child => child.Parent)
                .WithMany(par => par.Children)
                .HasForeignKey(child => child.ParentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
