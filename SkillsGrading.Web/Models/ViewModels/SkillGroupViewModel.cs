namespace SkillsGrading.Web.Models.ViewModels
{
    public class SkillGroupViewModel : BaseViewModel
    {
        public string GroupName { get; set; }

        public List<SkillLevelViewModel> SkillLevels { get; set; }
    }
}
