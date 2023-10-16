namespace SkillsGrading.BusinessLogic.Models
{
    public class GradeModel : BaseModel
    {
        public DateTime GradeDate { get; set; }
        public Guid GradeTemplateId { get; set; }
        public Guid NewGradeLevelId { get; set; }
        public Guid EmployeeId { get; set; }
        
        public GradeTemplateModel GradeTemplate { get; set; }
        public GradeLevelModel GradeLevel { get; set; }
    }
}
