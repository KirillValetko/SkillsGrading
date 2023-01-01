using AutoMapper;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.BusinessLogic.Services.Interfaces;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.BusinessLogic.Services
{
    public abstract class BaseService<TDbModel, TDataModel, TModel, TFilter> : 
        IBaseService<TDbModel, TDataModel, TModel, TFilter>
        where TDbModel : BaseDbModel
        where TDataModel : BaseDataModel
        where TModel : BaseModel
        where TFilter : BaseFilter
    {
        protected readonly IBaseRepository<TDbModel, TDataModel, TFilter> _repository;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        protected BaseService(IBaseRepository<TDbModel, TDataModel, TFilter> repository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public virtual async Task CreateAsync(TModel item)
        {
            var mappedItem = _mapper.Map<TDataModel>(item);
            _repository.Create(mappedItem);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task UpdateAsync(TModel item)
        {
            var mappedItem = _mapper.Map<TDataModel>(item);
            await _repository.UpdateAsync(mappedItem);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _repository.SoftDeleteAsync(id);
            await _unitOfWork.SaveAsync();
        }

        public virtual async Task<TModel> GetByFilterAsync(TFilter filter)
        {
            var item = await _repository.GetByFilterAsync(filter);
            var mappedItem = _mapper.Map<TModel>(item);

            return mappedItem;
        }

        public virtual async Task<List<TModel>> GetAllByFilterAsync(TFilter filter)
        {
            var items = await _repository.GetAllByFilterAsync(filter);
            var mappedItems = _mapper.Map<List<TModel>>(items);

            return mappedItems;
        }

        public virtual async Task<PaginationResponse<TModel>> GetPaginatedAsync(PaginationRequest<TFilter> request)
        {
            var response = await _repository.GetPaginatedAsync(request);
            var mappedResponse = _mapper.Map<PaginationResponse<TModel>>(response);

            return mappedResponse;
        }
    }
}
