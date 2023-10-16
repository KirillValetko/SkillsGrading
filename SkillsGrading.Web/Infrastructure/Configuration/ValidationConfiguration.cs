using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using SkillsGrading.BusinessLogic.Responses;

namespace SkillsGrading.Web.Infrastructure.Configuration
{
    public static class ValidationConfiguration
    {
        private const string ErrorMessage = "Validation Error";

        public static void InitModelValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation(v =>
            {
                v.DisableDataAnnotationsValidation = true;
            });

            services.AddValidatorsFromAssemblyContaining<Program>();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var errors = c.ModelState.Keys
                        .Where(key => (c.ModelState[key]?.Errors?.Count ?? default) > default(int))
                        .ToDictionary(k => k, k => c.ModelState[k]!.Errors.Select(e => e.ErrorMessage).ToArray());

                    return new BadRequestObjectResult(new ApiResponse<ValidationError>(ErrorMessage, StatusCodes.Status400BadRequest)
                    {
                        Payload = new ValidationError
                        {
                            Errors = errors
                        }
                    });
                };
            });
        }
    }
}
