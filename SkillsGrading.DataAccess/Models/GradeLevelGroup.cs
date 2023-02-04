namespace SkillsGrading.DataAccess.Models
{
    public class GradeLevelGroup : BaseDbModel
    {
        public string GroupName { get; set; }
        public int GroupValue { get; set; }
        public bool IsUsed { get; set; }
        public Guid SpecialtyId { get; set; }

        public Specialty Specialty { get; set; }
        public List<GradeLevel> GradeLevels { get; set; }
    }
}
