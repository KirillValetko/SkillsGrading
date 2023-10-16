namespace SkillsGrading.Web.Models.ViewModels
{
    public class EmployeeViewModel : BaseViewModel
    {
        public EmployeeViewModel Grader { get; set; }
        public List<GradeViewModel> Grades { get; set; }
    }
}
