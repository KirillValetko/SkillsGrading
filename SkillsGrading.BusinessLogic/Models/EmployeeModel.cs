using System.Text.Json.Serialization;

namespace SkillsGrading.BusinessLogic.Models
{
    public class EmployeeModel : BaseModel
    {
        public Guid? GraderId { get; set; }
        
        public EmployeeModel Grader { get; set; }
        public List<GradeModel> Grades { get; set; }
    }
}
