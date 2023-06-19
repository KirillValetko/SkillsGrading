namespace SkillsGrading.DataAccess.DataModels
{
    public class EmployeeDataModel : BaseDataModel
    {
        public string AccountName { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
        public string Password { get; set; }
        public Guid GraderId { get; set; }
        public Guid SpecialtyId { get; set; }

        public SpecialtyDataModel Specialty { get; set; }
        public EmployeeDataModel Grader { get; set; }
        public List<EmployeeDataModel> Gradees { get; set; }
        public List<GradeDataModel> Grades { get; set; }
    }
}
