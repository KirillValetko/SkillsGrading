namespace SkillsGrading.Web.Models.DtoModels
{
    public class GradeTemplateDto : BaseDto
    {
        public string TemplateName { get; set; }

        public List<GradedSkillSetDto> GradedSkillSets { get; set; }
    }
}
