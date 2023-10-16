namespace SkillsGrading.Web.Models.DtoModels
{
    public class GradingDto
    {
        public Guid EmployeeId { get; set; }
        public Guid GradeTemplateId { get; set; }
        public List<SkillSetDto> SkillSets { get; set; }
    }
}
