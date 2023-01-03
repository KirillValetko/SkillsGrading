using SkillsGrading.DataAccess.Repositories;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class RepositoriesConfiguration
    {
        public static void InitRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISkillGroupRepository, SkillGroupRepository>();
            services.AddScoped<ISkillLevelRepository, SkillLevelRepository>();
        }
    }
}
