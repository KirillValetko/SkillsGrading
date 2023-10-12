using FluentValidation;
using SkillsGrading.Web.Models.DtoModels;

namespace SkillsGrading.Web.Validators
{
    public class GradedSkillSetDtoValidator : AbstractValidator<GradedSkillSetDto>
    {
        public GradedSkillSetDtoValidator()
        {
            RuleFor(gradedSkillSet => gradedSkillSet.SkillPosition).GreaterThan(0).NotEmpty();
            RuleFor(gradedSkillSet => gradedSkillSet.GradeLevelPosition).GreaterThan(0).NotEmpty();
            RuleFor(gradedSkillSet => gradedSkillSet.SkillId).NotEmpty();
            RuleFor(gradedSkillSet => gradedSkillSet.GradeLevelId).NotEmpty();
            RuleFor(gradedSkillSet => gradedSkillSet.SkillLevelId).NotEmpty();
        }
    }
}
