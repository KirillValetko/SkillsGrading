using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.AccountName).IsRequired();
            builder.Property(e => e.FullName).IsRequired();
            builder.Property(e => e.Department).IsRequired();
            builder.Property(e => e.IsActive).IsRequired();
            builder.Property(e => e.GraderId).IsRequired();
            builder.Property(e => e.SpecialtyId).IsRequired();
            builder.HasOne(e => e.Grader)
                .WithMany(e => e.Gradees)
                .HasForeignKey(e => e.GraderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
