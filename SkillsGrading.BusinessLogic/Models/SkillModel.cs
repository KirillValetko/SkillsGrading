namespace SkillsGrading.BusinessLogic.Models
{
    public class SkillModel : BaseModel
    {
        public string SkillName { get; set; }
        public bool IsUsed { get; set; }
        public Guid GroupId { get; set; }

        public SkillGroupModel SkillGroup { get; set; }
    }
}
