namespace SkillsGrading.BusinessLogic.Models
{
    public class GradedSkillSetModel : BaseModel
    {
        public int GradeLevelPosition { get; set; }
        public int SkillPosition { get; set; }
        public Guid GradeTemplateId { get; set; }
        public Guid GradeLevelId { get; set; }
        public Guid SkillId { get; set; }
        public Guid SkillLevelId { get; set; }

        public GradeLevelModel GradeLevel { get; set; }
        public SkillModel Skill { get; set; }
        public SkillLevelModel SkillLevel { get; set; }
    }
}
