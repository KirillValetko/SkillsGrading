namespace SkillsGrading.Web.Models.ViewModels
{
    public class GradeLevelGroupViewModel : BaseViewModel
    {
        public string GroupName { get; set; }
        public Guid SpecialtyId { get; set; }

        public List<GradeLevelViewModel> GradeLevels { get; set; }
    }
}
