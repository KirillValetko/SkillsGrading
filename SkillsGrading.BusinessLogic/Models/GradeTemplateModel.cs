namespace SkillsGrading.BusinessLogic.Models
{
    public class GradeTemplateModel : BaseModel
    {
        public string TemplateName { get; set; }
        public Guid SpecialtyId { get; set; }

        public List<GradedSkillSetModel> GradedSkillSets { get; set; }
    }
}
