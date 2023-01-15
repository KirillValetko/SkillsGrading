namespace SkillsGrading.BusinessLogic.Models
{
    public class SkillLevelModel : BaseModelLevel
    {
        public string Description { get; set; }
        public Guid GroupId { get; set; }
    }
}
