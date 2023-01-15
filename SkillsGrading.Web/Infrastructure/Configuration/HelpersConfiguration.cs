using SkillsGrading.Common.Helpers;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class HelpersConfiguration
    {
        public static void InitHelpers(this IServiceCollection services)
        {
            services.AddScoped<IPaginationHelper<SkillGroup>, PaginationHelper<SkillGroup>>();
            services.AddScoped<IPaginationHelper<SkillLevel>, PaginationHelper<SkillLevel>>();
        }
    }
}
