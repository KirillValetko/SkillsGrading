namespace SkillsGrading.Web.Models.DtoModels
{
    public class GradeLevelGroupDto : BaseDto
    {
        public string GroupName { get; set; }
        public Guid SpecialtyId { get; set; }

        public List<GradeLevelDto> GradeLevels { get; set; }
    }
}
