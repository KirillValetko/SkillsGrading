using FluentValidation;
using SkillsGrading.Web.Models.DtoModels;

namespace SkillsGrading.Web.Validators
{
    public class GradeTemplateDtoValidator : AbstractValidator<GradeTemplateDto>
    {
        public GradeTemplateDtoValidator()
        {
            RuleFor(gradeTemplate => gradeTemplate.TemplateName).NotEmpty();
            RuleFor(gradeTemplate => gradeTemplate.GradedSkillSets).NotEmpty();
            RuleForEach(gradeTemplate => gradeTemplate.GradedSkillSets).SetValidator(new GradedSkillSetDtoValidator());
        }
    }
}
