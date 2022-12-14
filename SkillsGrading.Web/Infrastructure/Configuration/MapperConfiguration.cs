using SkillsGrading.BusinessLogic.Infrastructure;
using SkillsGrading.DataAccess.Infrastructure;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class MapperConfiguration
    {
        public static void InitMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DalMapperProfile), typeof(BllMapperProfile), typeof(ApiMapperProfile));
        }
    }
}
