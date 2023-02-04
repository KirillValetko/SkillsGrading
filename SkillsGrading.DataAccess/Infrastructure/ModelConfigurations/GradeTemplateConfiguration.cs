using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class GradeTemplateConfiguration : IEntityTypeConfiguration<GradeTemplate>
    {
        public void Configure(EntityTypeBuilder<GradeTemplate> builder)
        {
            builder.Property(gt => gt.TemplateName).IsRequired();
            builder.Property(gt => gt.IsUsed).IsRequired();
            builder.Property(gt => gt.IsActive).IsRequired();
            builder.Property(gt => gt.SpecialtyId).IsRequired();
        }
    }
}
