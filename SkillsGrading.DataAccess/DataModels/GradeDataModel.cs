namespace SkillsGrading.DataAccess.DataModels
{
    public class GradeDataModel : BaseDataModel
    {
        public DateTime GradeDate { get; set; }
        public Guid GradeTemplateId { get; set; }
        public Guid NewGradeLevelId { get; set; }
        public Guid EmployeeId { get; set; }

        public GradeTemplateDataModel GradeTemplate { get; set; }
        public GradeLevelDataModel GradeLevel { get; set; }
        public EmployeeDataModel Employee { get; set; }
    }
}
