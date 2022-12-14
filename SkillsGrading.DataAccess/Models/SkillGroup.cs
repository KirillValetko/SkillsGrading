namespace SkillsGrading.DataAccess.Models
{
    public class SkillGroup : BaseDbModel
    {
        public string GroupName { get; set; }
        public bool IsUsed { get; set; }

        public List<Skill> Skills { get; set; }
        public List<SkillLevel> SkillLevels { get; set; }
    }
}
