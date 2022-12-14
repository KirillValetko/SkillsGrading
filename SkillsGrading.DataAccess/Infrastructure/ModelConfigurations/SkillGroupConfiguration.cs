using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class SkillGroupConfiguration : IEntityTypeConfiguration<SkillGroup>
    {
        public void Configure(EntityTypeBuilder<SkillGroup> builder)
        {
            builder.Property(sg => sg.GroupName).IsRequired();
            builder.Property(sg => sg.IsUsed).IsRequired();
            builder.Property(sg => sg.IsActive).IsRequired();
            builder.HasMany(sg => sg.Skills)
                .WithOne(s => s.SkillGroup)
                .HasForeignKey(s => s.GroupId);
            builder.HasMany(sg => sg.SkillLevels)
                .WithOne(sl => sl.SkillGroup)
                .HasForeignKey(sl => sl.GroupId);
        }
    }
}
