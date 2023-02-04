namespace SkillsGrading.BusinessLogic.Models
{
    public class GradeLevelGroupModel : BaseModel
    {
        public string GroupName { get; set; }
        public int GroupValue { get; set; }
        public Guid SpecialtyId { get; set; }

        public List<GradeLevelModel> GradeLevels { get; set; }
    }
}
