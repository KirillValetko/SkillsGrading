namespace SkillsGrading.BusinessLogic.Models
{
    public class BaseModelLevel : BaseModel
    {
        public string LevelName { get; set; }
        public int LevelValue { get; set; }
        public bool IsUsed { get; set; }
    }
}
