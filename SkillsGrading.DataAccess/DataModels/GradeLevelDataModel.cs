namespace SkillsGrading.DataAccess.DataModels
{
    public class GradeLevelDataModel : BaseDataLevel
    {
        public int GradeRevisionInMonths { get; set; }
        
        public List<GradeDataModel> Grades { get; set; }
        public List<GradedSkillSetDataModel> GradedSkillSets { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var otherGradeLevel = (GradeLevelDataModel)obj;
            return Id == otherGradeLevel.Id
                   && LevelName == otherGradeLevel.LevelName
                   && LevelValue == otherGradeLevel.LevelValue
                   && GradeRevisionInMonths == otherGradeLevel.GradeRevisionInMonths;
        }
    }
}
