using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure.ModelConfigurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<Specialty>
    {
        public void Configure(EntityTypeBuilder<Specialty> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.SpecialtyName).IsRequired();
            builder.Property(d => d.IsUsed).IsRequired();
            builder.Property(d => d.IsActive).IsRequired();
            builder.HasMany(d => d.Employees)
                .WithOne(e => e.Specialty)
                .HasForeignKey(e => e.SpecialtyId);
            builder.HasMany(d => d.GradeTemplates)
                .WithOne(gt => gt.Specialty)
                .HasForeignKey(gt => gt.SpecialtyId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(d => d.GradeLevelGroups)
                .WithOne(glg => glg.Specialty)
                .HasForeignKey(glg => glg.SpecialtyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
