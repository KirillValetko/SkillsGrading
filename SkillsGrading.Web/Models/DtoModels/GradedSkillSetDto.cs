namespace SkillsGrading.Web.Models.DtoModels
{
    public class GradedSkillSetDto : BaseDto
    {
        public int GradeLevelPosition { get; set; }
        public int SkillPosition { get; set; }
        public Guid GradeLevelId { get; set; }
        public Guid SkillId { get; set; }
        public Guid SkillLevelId { get; set; }
    }
}
