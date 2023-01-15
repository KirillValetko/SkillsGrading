namespace SkillsGrading.DataAccess.Filters
{
    public class BaseLevelFilter : BaseFilter
    {
        public string LevelName { get; set; }
        public int? LevelValue { get; set; }
    }
}
