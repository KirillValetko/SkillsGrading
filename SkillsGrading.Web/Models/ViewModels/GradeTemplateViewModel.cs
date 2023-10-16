namespace SkillsGrading.Web.Models.ViewModels
{
    public class GradeTemplateViewModel : BaseViewModel
    {
        public string TemplateName { get; set; }

        public List<GradedSkillSetViewModel> GradedSkillSets { get; set; }
    }
}
