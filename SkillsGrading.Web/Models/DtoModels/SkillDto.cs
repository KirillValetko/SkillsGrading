using SkillsGrading.Web.Models.ViewModels;

namespace SkillsGrading.Web.Models.DtoModels
{
    public class SkillDto : BaseDto
    {
        public string SkillName { get; set; }

        public SkillGroupViewModel SkillGroup { get; set; }
    }
}
