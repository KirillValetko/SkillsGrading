﻿using SkillsGrading.BusinessLogic.Services;
using SkillsGrading.BusinessLogic.Services.Interfaces;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class ServicesConfiguration
    {
        public static void InitServices(this IServiceCollection services)
        {
            services.AddScoped<ISpecialtyService, SpecialtyService>();
            services.AddScoped<IGradeLevelGroupService, GradeLevelGroupService>();
            services.AddScoped<ISkillGroupService, SkillGroupService>();
            services.AddScoped<ISkillService, SkillService>();
        }
    }
}
