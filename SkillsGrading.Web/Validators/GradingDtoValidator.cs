using FluentValidation;
using SkillsGrading.Web.Models.DtoModels;

namespace SkillsGrading.Web.Validators
{
    public class GradingDtoValidator : AbstractValidator<GradingDto>
    {
        public GradingDtoValidator()
        {
            RuleFor(g => g.EmployeeId).NotEmpty();
            RuleFor(g => g.GradeTemplateId).NotEmpty();
            RuleFor(g => g.SkillSets).NotEmpty();
            RuleForEach(g => g.SkillSets).SetValidator(new SkillSetDtoValidator());
        }
    }
}
