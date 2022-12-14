using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SkillsGrading.Common.Constants;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class JwtConfiguration
    {
        public static void InitJwt(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.RequireHttpsMetadata = true;
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = JwtOptionsConstants.IssuerOpt,

                        ValidateAudience = true,
                        ValidAudience = JwtOptionsConstants.AudienceOpt,

                        ValidateLifetime = true,

                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptionsConstants.Key)),
                        ValidateIssuerSigningKey = true
                    };
                });
        }
    }
}
