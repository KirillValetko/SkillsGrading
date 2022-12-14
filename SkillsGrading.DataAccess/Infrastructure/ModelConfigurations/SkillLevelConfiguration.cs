using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class SkillLevelConfiguration : IEntityTypeConfiguration<SkillLevel>
    {
        public void Configure(EntityTypeBuilder<SkillLevel> builder)
        {
            builder.Property(sl => sl.LevelName).IsRequired();
            builder.Property(sl => sl.Description).IsRequired();
            builder.Property(sl => sl.LevelValue).IsRequired();
            builder.Property(sl => sl.IsUsed).IsRequired();
            builder.Property(sl => sl.IsActive).IsRequired();
            builder.Property(sl => sl.GroupId).IsRequired();
        }
    }
}
