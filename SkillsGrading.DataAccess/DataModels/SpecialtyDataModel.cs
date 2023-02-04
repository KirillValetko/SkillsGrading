namespace SkillsGrading.DataAccess.DataModels
{
    public class SpecialtyDataModel : BaseDataModel
    {
        public string SpecialtyName { get; set; }
        public bool IsUsed { get; set; }

        public List<EmployeeDataModel> Employees { get; set; }
        public List<GradeLevelGroupDataModel> GradeLevelGroups { get; set; }
        public List<GradeTemplateDataModel> GradeTemplates { get; set; }
    }
}
