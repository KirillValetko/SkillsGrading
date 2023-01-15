namespace SkillsGrading.Web.Models.DtoModels
{
    public class SkillGroupDto : BaseDto
    {
        public string GroupName { get; set; }

        public List<SkillLevelDto> SkillLevels { get; set; }
    }
}
