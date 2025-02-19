using HRM_Management.Dal.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HRM_Management.Dal.Configurations
{
    internal class ExEmployeeConfiguration : IEntityTypeConfiguration<ExEmployeeEntity>
    {
        public void Configure(EntityTypeBuilder<ExEmployeeEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(ex => ex.LeavingDate)
                .IsRequired();

            builder.Navigation(ex => ex.Employee)
                .AutoInclude();

            builder.HasOne(ex => ex.Employee)
                .WithOne(emp => emp.ExEmployee)
                .HasForeignKey<ExEmployeeEntity>(ex => ex.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
