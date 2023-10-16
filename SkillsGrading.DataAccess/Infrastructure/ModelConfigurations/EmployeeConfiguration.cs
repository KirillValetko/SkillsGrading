using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.Common.Constants;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.IsActive).IsRequired();
            builder.HasOne(e => e.Grader)
                .WithMany()
                .HasForeignKey(e => e.GraderId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
            builder.ToTable(et => et.HasTrigger(TriggerNameConstants.CreateTrigger));
            builder.ToTable(et => et.HasTrigger(TriggerNameConstants.UpdateTrigger));
        }
    }
}
