namespace SkillsGrading.DataAccess.DataModels
{
    public class EmployeeDataModel : BaseDataModel
    {
        public Guid? GraderId { get; set; }
        
        public EmployeeDataModel Grader { get; set; }
        public List<GradeDataModel> Grades { get; set; }
    }
}
