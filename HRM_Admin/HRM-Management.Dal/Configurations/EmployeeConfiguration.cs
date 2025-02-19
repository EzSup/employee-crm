using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM_Management.Dal.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<EmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<EmployeeEntity> builder)
        {
            builder.HasKey(hub => hub.Id);

            builder.Navigation(emp => emp.ExEmployee)
                .IsRequired(false);
            builder.Navigation(emp => emp.Hirer)
                .IsRequired(false);

            builder.HasOne(emp => emp.Hirer)
                .WithMany(user => user.HiredEmployees)
                .HasForeignKey(emp => emp.HirerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(emp => emp.Person)
                .WithOne(per => per.Employee)
                .HasForeignKey<EmployeeEntity>(emp => emp.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
