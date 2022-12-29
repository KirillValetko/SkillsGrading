namespace SkillsGrading.DataAccess.DataModels
{
    public class EmployeeDataModel : BaseDataModel
    {
        public string AccountName { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public Guid GraderId { get; set; }

        public EmployeeDataModel Grader { get; set; }
        public List<EmployeeDataModel> Gradees { get; set; }
        public List<GradeDataModel> Grades { get; set; }
    }
}
