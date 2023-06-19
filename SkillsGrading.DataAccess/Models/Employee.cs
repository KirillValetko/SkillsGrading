namespace SkillsGrading.DataAccess.Models
{
    public class Employee : BaseDbModel
    {
        public string AccountName { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
        public string Password { get; set; }
        public Guid GraderId { get; set; }
        public Guid SpecialtyId { get; set; }

        public Specialty Specialty { get; set; }
        public Employee Grader { get; set; }
        public List<Employee> Gradees { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
