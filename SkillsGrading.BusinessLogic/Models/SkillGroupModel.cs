namespace SkillsGrading.BusinessLogic.Models
{
    public class SkillGroupModel : BaseModel
    {
        public string GroupName { get; set; }
        public bool IsUsed { get; set; }
        
        public List<SkillLevelModel> SkillLevels { get; set; }
    }
}
