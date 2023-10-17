namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class AwsLambdaConfiguration
    {
        public static void InitAwsLambda(this IServiceCollection services, IWebHostEnvironment environment)
        {
            if (!environment.IsDevelopment())
            {
                services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
            }
        }
    }
}
