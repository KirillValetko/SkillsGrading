namespace SkillsGrading.BusinessLogic.Models
{
    public class GradingModel
    {
        public Guid EmployeeId { get; set; }
        public Guid GradeTemplateId { get; set; }
        public List<SkillSetModel> SkillSets { get; set; }
    }
}
