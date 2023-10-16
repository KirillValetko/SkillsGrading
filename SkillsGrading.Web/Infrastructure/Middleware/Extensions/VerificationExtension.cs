namespace SkillsGrading.Web.Infrastructure.Middleware.Extensions
{
    public static class VerificationExtension
    {
        public static IApplicationBuilder UseVerification(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VerificationMiddleware>();
        }
    }
}
