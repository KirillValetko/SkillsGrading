namespace SkillsGrading.DataAccess.DataModels
{
    public class BaseDataLevel : BaseDataModel
    {
        public string LevelName { get; set; }
        public int LevelValue { get; set; }
        public bool IsUsed { get; set; }
    }
}
