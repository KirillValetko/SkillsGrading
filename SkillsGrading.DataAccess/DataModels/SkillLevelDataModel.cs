namespace SkillsGrading.DataAccess.DataModels
{
    public class SkillLevelDataModel : BaseDataLevel
    {
        public string Description { get; set; }
        public Guid GroupId { get; set; }

        public SkillGroupDataModel SkillGroup { get; set; }
        public List<GradedSkillSetDataModel> GradedSkillSets { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherSkillLevel = (SkillLevelDataModel)obj;
            return Id == otherSkillLevel.Id
                   && LevelName == otherSkillLevel.LevelName
                   && LevelValue == otherSkillLevel.LevelValue
                   && Description == otherSkillLevel.Description;
        }
    }
}
