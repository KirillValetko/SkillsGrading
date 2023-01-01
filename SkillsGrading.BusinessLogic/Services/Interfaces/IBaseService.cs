using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.BusinessLogic.Services.Interfaces
{
    public interface IBaseService<TDbModel, TDataModel, TModel, TFilter>
        where TDbModel : BaseDbModel
        where TDataModel : BaseDataModel
        where TModel : BaseModel
        where TFilter : BaseFilter
    {
        Task CreateAsync(TModel item);
        Task UpdateAsync(TModel item);
        Task DeleteAsync(Guid id);
        Task<TModel> GetByFilterAsync(TFilter filter);
        Task<List<TModel>> GetAllByFilterAsync(TFilter filter);
        Task<PaginationResponse<TModel>> GetPaginatedAsync(PaginationRequest<TFilter> request);
    }
}
