namespace SkillsGrading.DataAccess.Models
{
    public class GradeLevel : BaseDbLevel
    {
        public int Salary { get; set; }
        public int GradeRevisionInMonths { get; set; }

        public List<Grade> Grades { get; set; }
        public List<GradedSkillSet> GradedSkillSets { get; set; }
    }
}
