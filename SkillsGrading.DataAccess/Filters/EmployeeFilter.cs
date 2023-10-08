namespace SkillsGrading.DataAccess.Filters
{
    public class EmployeeFilter : BaseFilter
    {
        public Guid? GraderId { get; set; }
    }
}
