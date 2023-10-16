using Microsoft.EntityFrameworkCore;
using SkillsGrading.DataAccess.Infrastructure;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class GradingContextConfiguration
    {
        private const string ConnectionString = "ConnectionStrings:GradingDB";
        public static void InitDbContext(this IServiceCollection services, IConfiguration configuration)
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
