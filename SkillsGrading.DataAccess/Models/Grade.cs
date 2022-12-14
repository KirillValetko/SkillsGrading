using System.ComponentModel.DataAnnotations;

namespace SkillsGrading.DataAccess.Models
{
    public class Grade : BaseDbModel
    {
        [DataType(DataType.Date)]
        public DateTime GradeDate { get; set; } 
        public Guid GradeTemplateId { get; set; }
        public Guid NewGradeLevelId { get; set; }
        public Guid EmployeeId { get; set; }

        public GradeTemplate GradeTemplate { get; set; }
        public GradeLevel GradeLevel { get; set; }
        public Employee Employee { get; set; }
    }
}
