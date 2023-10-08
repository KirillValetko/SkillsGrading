namespace SkillsGrading.Web.Models.ViewModels
{
    public class GradeViewModel : BaseViewModel
    {

        public DateTime GradeDate { get; set; }

        public GradeTemplateViewModel GradeTemplate { get; set; }
        public GradeLevelViewModel GradeLevel { get; set; }
    }
}
