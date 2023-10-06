namespace SkillsGrading.DataAccess.Models
{
    public class GradeLevel : BaseDbLevel
    {
        public int GradeRevisionInMonths { get; set; }
        
        public List<Grade> Grades { get; set; }
        public List<GradedSkillSet> GradedSkillSets { get; set; }
    }
}
