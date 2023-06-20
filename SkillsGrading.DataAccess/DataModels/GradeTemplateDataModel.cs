namespace SkillsGrading.DataAccess.DataModels
{
    public class GradeTemplateDataModel : BaseDataModel
    {
        public string TemplateName { get; set; }
        public bool IsUsed { get; set; }
        public Guid SpecialtyId { get; set; }

        public SpecialtyDataModel Specialty { get; set; }
        public List<GradeDataModel> Grades { get; set; }
        public List<GradedSkillSetDataModel> GradedSkillSets { get; set; }
    }
}
