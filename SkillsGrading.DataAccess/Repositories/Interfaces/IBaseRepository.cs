using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Repositories.Interfaces
{
    public interface IBaseRepository<TDbModel, TDataModel, TFilter>
        where TDbModel : BaseDbModel
        where TDataModel : BaseDataModel
        where TFilter : BaseFilter
    {
        void Create(TDataModel item);
        void CreateMany(List<TDataModel> items);
        Task UpdateAsync(TDataModel item);
        Task UpdateManyAsync(List<TDataModel> items);
        Task SoftDeleteAsync(Guid id);
        Task SoftDeleteManyAsync(List<Guid> ids);
        Task HardDeleteAsync(Guid id);
        Task HardDeleteManyAsync(List<Guid> ids);
        Task<TDataModel> GetByFilterAsync(TFilter filter);
        Task<List<TDataModel>> GetAllByFilter(TFilter filter);
        Task<PaginationResponse<TDataModel>> GetPaginated(PaginationRequest<TFilter> request);
    }
}
