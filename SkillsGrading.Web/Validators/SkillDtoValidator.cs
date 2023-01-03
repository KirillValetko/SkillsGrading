using FluentValidation;
using SkillsGrading.Web.Models.DtoModels;

namespace SkillsGrading.Web.Validators
{
    public class SkillDtoValidator : AbstractValidator<SkillDto>
    {
        public SkillDtoValidator()
        {
            RuleFor(s => s.SkillName).NotEmpty();
            RuleFor(s => s.SkillGroup).NotEmpty();
        }
    }
}
