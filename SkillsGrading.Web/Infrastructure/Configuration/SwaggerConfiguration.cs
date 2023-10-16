using Microsoft.OpenApi.Models;
using SkillsGrading.Common.Constants;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void InitSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc(SwaggerOptionsConstants.Version, new OpenApiInfo
                {
                    Version = SwaggerOptionsConstants.Version,
                    Title = SwaggerOptionsConstants.Title,
                    Description = SwaggerOptionsConstants.Description
                });
                opt.AddSecurityDefinition(SwaggerOptionsConstants.SecurityScheme, new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = SwaggerOptionsConstants.DefinitionName,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = SwaggerOptionsConstants.SecurityScheme
                });
            });
        }
    }
}
