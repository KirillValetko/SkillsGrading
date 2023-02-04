namespace SkillsGrading.DataAccess.DataModels
{
    public class GradeLevelGroupDataModel : BaseDataModel
    {
        public string GroupName { get; set; }
        public int GroupValue { get; set; }
        public bool IsUsed { get; set; }
        public Guid SpecialtyId { get; set; }

        public SpecialtyDataModel Specialty { get; set; }
        public List<GradeLevelDataModel> GradeLevels { get; set; }
    }
}
