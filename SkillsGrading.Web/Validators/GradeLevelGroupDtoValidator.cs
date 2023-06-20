using FluentValidation;
using SkillsGrading.Web.Models.DtoModels;

namespace SkillsGrading.Web.Validators
{
    public class GradeLevelGroupDtoValidator : AbstractValidator<GradeLevelGroupDto>
    {
        public GradeLevelGroupDtoValidator()
        {
            RuleFor(glg => glg.GroupName).NotEmpty();
            RuleFor(glg => glg.SpecialtyId).NotEmpty();
            RuleFor(glg => glg.GradeLevels).NotEmpty();
        }
    }
}
