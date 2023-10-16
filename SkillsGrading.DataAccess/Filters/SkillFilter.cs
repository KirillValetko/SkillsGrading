namespace SkillsGrading.DataAccess.Filters
{
    public class SkillFilter : BaseFilter
    {
        public string SkillName { get; set; }
        public bool? IncludeSkillGroups { get; set; }
    }
}
