using FluentValidation;
using SkillsGrading.Web.Models.DtoModels;

namespace SkillsGrading.Web.Validators
{
    public class SkillSetDtoValidator : AbstractValidator<SkillSetDto>
    {
        public SkillSetDtoValidator()
        {
            RuleFor(ss => ss.SkillId).NotEmpty();
            RuleFor(ss => ss.SkillLevelId).NotEmpty();
        }
    }
}
