﻿using Microsoft.EntityFrameworkCore;
using SkillsGrading.DataAccess.Infrastructure;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class GradingContextConfiguration
    {
        private const string ConnectionString = "GradingDb";
        public static void InitDbContext(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                services.AddDbContext<GradingContext>(opt =>
                    opt.UseSqlServer(configuration.GetConnectionString(ConnectionString)));
            }
            else
            {
                services.AddDbContext<GradingContext>(opt =>
                    opt.UseNpgsql(configuration.GetConnectionString(ConnectionString)));
            }
        }
    }
}
