namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class AwsSecretsManagerConfiguration
    {
        public static void ConfigureAwsSecretsManager(this WebApplicationBuilder builder)
        {
            if (!builder.Environment.IsDevelopment())
            {
                var envName = builder.Environment.EnvironmentName;
                var appName = builder.Environment.ApplicationName;
                builder.Configuration.AddSecretsManager(
                    configurator: options =>
                    {
                        options.SecretFilter = entry => entry.Name.StartsWith(
                            $"{envName}_{appName}_");
                        options.KeyGenerator = (_, s) => s
                            .Replace($"{envName}_{appName}_", string.Empty)
                            .Replace("__", ":");
                        options.PollingInterval = TimeSpan.FromSeconds(15);
                    });
            }
        }
    }
}
