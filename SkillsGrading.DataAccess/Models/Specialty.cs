namespace SkillsGrading.DataAccess.Models
{
    public class Specialty : BaseDbModel
    {
        public string SpecialtyName { get; set; }
        public bool IsUsed { get; set; }

        public List<Employee> Employees { get; set; }
        public List<GradeLevelGroup> GradeLevelGroups { get; set; }
        public List<GradeTemplate> GradeTemplates { get; set; }
    }
}
