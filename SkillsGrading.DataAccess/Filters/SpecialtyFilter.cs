namespace SkillsGrading.DataAccess.Filters
{
    public class SpecialtyFilter : BaseFilter
    {
        public string SpecialtyName { get; set; }
        public bool? HasGradeLevelGroup { get; set; }
        public bool? IsUsed { get; set; }
    }
}
