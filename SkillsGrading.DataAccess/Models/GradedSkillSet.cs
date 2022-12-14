namespace SkillsGrading.DataAccess.Models
{
    public class GradedSkillSet : BaseDbModel
    {
        public int GradeLevelPosition { get; set; }
        public int SkillPosition { get; set; }
        public Guid GradeTemplateId { get; set; }
        public Guid GradeLevelId { get; set; }
        public Guid SkillId { get; set; }
        public Guid SkillLevelId { get; set; }

        public GradeTemplate GradeTemplate { get; set; }
        public GradeLevel GradeLevel { get; set; }
        public Skill Skill { get; set; }
        public SkillLevel SkillLevel { get; set; }
    }
}
