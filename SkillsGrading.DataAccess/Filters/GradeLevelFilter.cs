namespace SkillsGrading.DataAccess.Filters
{
    public class GradeLevelFilter : BaseLevelFilter
    {
        public int? Salary { get; set; }
        public int? GradeRevisionInMonths { get; set; }
        public Guid? SpecialtyId { get; set; }
    }
}
