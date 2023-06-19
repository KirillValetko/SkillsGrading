namespace SkillsGrading.Web.Models.ViewModels
{
    public class GradeTemplateViewModel : BaseViewModel
    {
        public string TemplateName { get; set; }
        public Guid SpecialtyId { get; set; }

        public List<GradedSkillSetViewModel> GradedSkillSets { get; set; }
    }
}
