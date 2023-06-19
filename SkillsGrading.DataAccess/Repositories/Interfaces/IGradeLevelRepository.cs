using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Repositories.Interfaces
{
    public interface IGradeLevelRepository : IBaseRepository<GradeLevel, GradeLevelDataModel, GradeLevelFilter>
    {
        void SetGradeLevelsUsed(List<GradeLevelDataModel> gradeLevels);
    }
}
