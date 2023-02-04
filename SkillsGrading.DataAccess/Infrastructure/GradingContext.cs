using Microsoft.EntityFrameworkCore;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Infrastructure
{
    public class GradingContext : DbContext
    {
        public GradingContext(DbContextOptions<GradingContext> options)
        : base(options)
        {
        }

        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<GradeTemplate> GradeTemplates { get; set; }
        public DbSet<GradeLevelGroup> GradeLevelGroups { get; set; }
        public DbSet<GradeLevel> GradeLevels { get; set; }
        public DbSet<GradedSkillSet> GradedSkillSets { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<SkillGroup> SkillGroups { get; set; }
        public DbSet<SkillLevel> SkillLevels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
