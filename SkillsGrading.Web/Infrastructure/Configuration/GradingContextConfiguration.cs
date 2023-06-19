using Microsoft.EntityFrameworkCore;
using SkillsGrading.DataAccess.Infrastructure;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class GradingContextConfiguration
    {
        private const string ConnectionString = "GradingDB";
        public static void InitDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<GradingContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString(ConnectionString)));
        }
    }
}
