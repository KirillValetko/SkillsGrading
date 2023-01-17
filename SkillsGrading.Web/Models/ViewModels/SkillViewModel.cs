namespace SkillsGrading.Web.Models.ViewModels
{
    public class SkillViewModel : BaseViewModel
    {
        public string SkillName { get; set; }

        public SkillGroupViewModel SkillGroup { get; set; }
    }
}
