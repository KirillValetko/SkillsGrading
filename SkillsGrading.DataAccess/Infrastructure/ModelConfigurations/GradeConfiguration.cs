using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.Property(g => g.GradeDate).IsRequired();
            builder.Property(g => g.IsActive).IsRequired();
            builder.Property(g => g.GradeTemplateId).IsRequired();
            builder.Property(g => g.NewGradeLevelId).IsRequired();
            builder.Property(g => g.EmployeeId).IsRequired();
            builder.HasOne(g => g.GradeTemplate)
                .WithMany(gt => gt.Grades)
                .HasForeignKey(g => g.GradeTemplateId);
            builder.HasOne(g => g.GradeLevel)
                .WithMany(gl => gl.Grades)
                .HasForeignKey(g => g.NewGradeLevelId);
            builder.HasOne(g => g.Employee)
                .WithMany(e => e.Grades)
                .HasForeignKey(g => g.EmployeeId);
        }
    }
}
