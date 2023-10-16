namespace SkillsGrading.DataAccess.Models
{
    public class GradeTemplate : BaseDbModel
    {
        public string TemplateName { get; set; }
        public bool IsUsed { get; set; }
        
        public List<Grade> Grades { get; set; }
        public List<GradedSkillSet> GradedSkillSets { get; set; }
    }
}
