namespace SkillsGrading.DataAccess.DataModels
{
    public class GradeLevelDataModel : BaseDataLevel
    {
        public int Salary { get; set; }
        public int GradeRevisionInMonths { get; set; }

        public List<GradeDataModel> Grades { get; set; }
        public List<GradedSkillSetDataModel> GradedSkillSets { get; set; }
    }
}
