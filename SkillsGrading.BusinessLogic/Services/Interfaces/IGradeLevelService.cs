using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.BusinessLogic.Services.Interfaces
{
    public interface IGradeLevelService : IBaseService<GradeLevel, GradeLevelDataModel, GradeLevelModel, GradeLevelFilter>
    {
    }
}
