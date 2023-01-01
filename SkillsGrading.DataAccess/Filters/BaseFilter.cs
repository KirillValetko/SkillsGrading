namespace SkillsGrading.DataAccess.Filters
{
    public class BaseFilter
    {
        public Guid? Id { get; set; }
        public Guid[] Ids { get; set; }
    }
}
