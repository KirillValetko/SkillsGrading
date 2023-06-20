namespace SkillsGrading.BusinessLogic.Models
{
    public class GradeLevelModel : BaseModelLevel
    {
        public int Salary { get; set; }
        public int GradeRevisionInMonths { get; set; }
        public Guid GroupId { get; set; }
    }
}
