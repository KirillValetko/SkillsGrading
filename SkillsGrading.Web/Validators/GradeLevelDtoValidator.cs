using FluentValidation;
using SkillsGrading.Web.Models.DtoModels;

namespace SkillsGrading.Web.Validators
{
    public class GradeLevelDtoValidator : AbstractValidator<GradeLevelDto>
    {
        public GradeLevelDtoValidator()
        {
            RuleFor(gl => gl.LevelName).NotEmpty();
            RuleFor(gl => gl.LevelValue).GreaterThan(0).NotEmpty();
            RuleFor(gl => gl.Salary).GreaterThan(0).NotEmpty();
            RuleFor(gl => gl.GradeRevisionInMonths).GreaterThan(0).NotEmpty();
        }
    }
}
