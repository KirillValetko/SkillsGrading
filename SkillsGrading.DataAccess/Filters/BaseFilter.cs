namespace SkillsGrading.DataAccess.Filters
{
    public class BaseFilter
    {
        public Guid? Id { get; set; }
        public List<Guid> Ids { get; set; }
        public bool? IsTracking { get; set; }
        public bool? OnlyActive { get; set; }
    }
}
