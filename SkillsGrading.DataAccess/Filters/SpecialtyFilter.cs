namespace SkillsGrading.DataAccess.Filters
{
    public class SpecialtyFilter : BaseFilter
    {
        public string SpecialtyName { get; set; }
        public bool? IsFull { get; set; }
    }
}
