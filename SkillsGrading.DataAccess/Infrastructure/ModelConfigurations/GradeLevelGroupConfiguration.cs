using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class GradeLevelGroupConfiguration : IEntityTypeConfiguration<GradeLevelGroup>
    {
        public void Configure(EntityTypeBuilder<GradeLevelGroup> builder)
        {
            builder.HasKey(glg => glg.Id);
            builder.Property(glg => glg.GroupName).IsRequired();
            builder.Property(glg => glg.IsActive).IsRequired();
            builder.Property(glg => glg.SpecialtyId).IsRequired();
            builder.HasMany(glg => glg.GradeLevels)
                .WithOne(gl => gl.GradeLevelGroup)
                .HasForeignKey(gl => gl.GroupId);
        }
    }
}
