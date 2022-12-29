namespace SkillsGrading.DataAccess.DataModels
{
    public class SkillLevelDataModel : BaseDataLevel
    {
        public string Description { get; set; }
        public Guid GroupId { get; set; }

        public SkillGroupDataModel SkillGroup { get; set; }
        public List<GradedSkillSetDataModel> GradedSkillSets { get; set; }
    }
}
