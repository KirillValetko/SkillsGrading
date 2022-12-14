namespace SkillsGrading.DataAccess.Models
{
    public class SkillLevel : BaseDbLevel
    {
        public string Description { get; set; }
        public Guid GroupId { get; set; }

        public SkillGroup SkillGroup { get; set; }
        public List<GradedSkillSet> GradedSkillSets { get; set; }
    }
}
