using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class GradeLevelConfiguration : IEntityTypeConfiguration<GradeLevel>
    {
        public void Configure(EntityTypeBuilder<GradeLevel> builder)
        {
            builder.Property(gl => gl.LevelName).IsRequired();
            builder.Property(gl => gl.Salary).IsRequired();
            builder.Property(gl => gl.GradeRevisionInMonths).IsRequired();
            builder.Property(gl => gl.LevelValue).IsRequired();
            builder.Property(gl => gl.IsUsed).IsRequired();
            builder.Property(gl => gl.IsActive).IsRequired();
            builder.Property(gl => gl.GroupId).IsRequired();
        }
    }
}
