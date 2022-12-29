namespace SkillsGrading.DataAccess.DataModels
{
    public class GradedSkillSetDataModel : BaseDataModel
    {
        public int GradeLevelPosition { get; set; }
        public int SkillPosition { get; set; }
        public Guid GradeTemplateId { get; set; }
        public Guid GradeLevelId { get; set; }
        public Guid SkillId { get; set; }
        public Guid SkillLevelId { get; set; }

        public GradeTemplateDataModel GradeTemplate { get; set; }
        public GradeLevelDataModel GradeLevel { get; set; }
        public SkillDataModel Skill { get; set; }
        public SkillLevelDataModel SkillLevel { get; set; }
    }
}
