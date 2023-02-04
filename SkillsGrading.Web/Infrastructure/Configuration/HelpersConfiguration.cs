﻿using SkillsGrading.Common.Helpers;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.Web.Enums;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class HelpersConfiguration
    {
        public static void InitHelpers(this IServiceCollection services)
        {
            services.AddScoped<IPaginationHelper<Specialty>, PaginationHelper<Specialty>>();
            services.AddScoped<IPaginationHelper<GradeLevelGroup>, PaginationHelper<GradeLevelGroup>>();
            services.AddScoped<IPaginationHelper<GradeLevel>, PaginationHelper<GradeLevel>>();
            services.AddScoped<IPaginationHelper<Skill>, PaginationHelper<Skill>>();
            services.AddScoped<IPaginationHelper<SkillGroup>, PaginationHelper<SkillGroup>>();
            services.AddScoped<IPaginationHelper<SkillLevel>, PaginationHelper<SkillLevel>>();
            services.AddScoped<IEnumHelper<GradeLevelGroupValues>, EnumHelper<GradeLevelGroupValues>>();
        }
    }
}
