namespace SkillsGrading.DataAccess.Models
{
    public class Employee : BaseDbModel
    {
        public string AccountName { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public Guid GraderId { get; set; }

        public Employee Grader { get; set; }
        public List<Employee> Gradees { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
