namespace SkillsGrading.DataAccess.Models
{
    public class Employee : BaseDbModel
    {
        public Guid? GraderId { get; set; }
        
        public Employee Grader { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
