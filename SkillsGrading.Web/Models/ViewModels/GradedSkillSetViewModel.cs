namespace SkillsGrading.Web.Models.ViewModels
{
    public class GradedSkillSetViewModel : BaseViewModel
    {
        public int GradeLevelPosition { get; set; }
        public int SkillPosition { get; set; }
        public Guid GradeTemplateId { get; set; }
        public Guid GradeLevelId { get; set; }
        public Guid SkillId { get; set; }
        public Guid SkillLevelId { get; set; }

        public GradeLevelViewModel GradeLevel { get; set; }
        public SkillViewModel Skill { get; set; }
        public SkillLevelViewModel SkillLevel { get; set; }
    }
}
