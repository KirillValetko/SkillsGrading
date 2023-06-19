using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Repositories.Interfaces
{
    public interface ISkillLevelRepository : IBaseRepository<SkillLevel, SkillLevelDataModel, SkillLevelFilter>
    {
        void SetSkillLevelsUsed(List<SkillLevelDataModel> skillLevels);
    }
}
