namespace SkillsGrading.DataAccess.Models
{
    public class BaseDbLevel : BaseDbModel
    {
        public string LevelName { get; set; }
        public int LevelValue { get; set; }
        public bool IsUsed { get; set; }
    }
}
