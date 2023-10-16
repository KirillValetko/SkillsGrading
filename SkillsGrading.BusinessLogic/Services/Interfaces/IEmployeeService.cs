using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService : IBaseService<Employee, EmployeeDataModel, EmployeeModel, EmployeeFilter>
    {
        Task GradeAsync(GradingModel item);
    }
}
