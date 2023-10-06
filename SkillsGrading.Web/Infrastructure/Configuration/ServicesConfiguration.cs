using SkillsGrading.BusinessLogic.Services;
using SkillsGrading.BusinessLogic.Services.Interfaces;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class ServicesConfiguration
    {
        public static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddHttpClient<IEmployeeService, EmployeeService>();
            services.AddScoped<IGradeTemplateService, GradeTemplateService>();
            services.AddScoped<IGradeLevelService, GradeLevelService>();
            services.AddScoped<ISkillGroupService, SkillGroupService>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddSingleton<ITokenService, TokenService>();
        }
    }
}
