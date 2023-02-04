using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class GradedSkillSetConfiguration : IEntityTypeConfiguration<GradedSkillSet>
    {
        public void Configure(EntityTypeBuilder<GradedSkillSet> builder)
        {
            builder.Property(gss => gss.GradeLevelPosition).IsRequired();
            builder.Property(gss => gss.SkillPosition).IsRequired();
            builder.Property(gss => gss.IsActive).IsRequired();
            builder.Property(gss => gss.GradeTemplateId).IsRequired();
            builder.Property(gss => gss.GradeLevelId).IsRequired();
            builder.Property(gss => gss.SkillId).IsRequired();
            builder.HasOne(gss => gss.GradeTemplate)
                .WithMany(gt => gt.GradedSkillSets)
                .HasForeignKey(gss => gss.GradeTemplateId);
            builder.HasOne(gss => gss.GradeLevel)
                .WithMany(gl => gl.GradedSkillSets)
                .HasForeignKey(gss => gss.GradeLevelId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(gss => gss.Skill)
                .WithMany(s => s.GradedSkillSets)
                .HasForeignKey(gss => gss.SkillId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(gss => gss.SkillLevel)
                .WithMany(sl => sl.GradedSkillSets)
                .HasForeignKey(gss => gss.GradeTemplateId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
