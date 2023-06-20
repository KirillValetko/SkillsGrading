namespace SkillsGrading.DataAccess.Filters
{
    public class GradeTemplateFilter : BaseFilter
    {
        public string TemplateName { get; set; }
        public bool? IncludeGradedSkillSets { get; set; }
        public Guid? SpecialtyId { get; set; }
    }
}
