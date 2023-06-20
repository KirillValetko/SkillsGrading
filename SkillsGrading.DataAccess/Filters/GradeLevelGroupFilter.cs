namespace SkillsGrading.DataAccess.Filters
{
    public class GradeLevelGroupFilter : BaseFilter
    {
        public string GroupName { get; set; }
        public int? GroupValue { get; set; }
        public Guid? SpecialtyId { get; set; }
    }
}
