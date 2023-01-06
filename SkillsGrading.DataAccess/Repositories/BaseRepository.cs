using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillsGrading.Common.Constants;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Infrastructure;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.DataAccess.Repositories
{
    public abstract class BaseRepository<TDbModel, TDataModel, TFilter> : 
        IBaseRepository<TDbModel, TDataModel, TFilter>
        where TDbModel : BaseDbModel
        where TDataModel : BaseDataModel
        where TFilter : BaseFilter
    {
        protected readonly GradingContext _gradingContext;
        protected readonly IPaginationHelper<TDbModel> _paginationHelper;
        protected readonly IMapper _mapper;

        protected BaseRepository(GradingContext gradingContext,
            IPaginationHelper<TDbModel> paginationHelper,
            IMapper mapper)
        {
            _gradingContext = gradingContext;
            _paginationHelper = paginationHelper;
            _mapper = mapper;
        }

        public virtual void Create(TDataModel item)
        {
            var mappedItem = _mapper.Map<TDbModel>(item);
            PrepareForCreation(mappedItem);
            _gradingContext.Set<TDbModel>().Add(mappedItem);
        }

        public virtual void CreateMany(List<TDataModel> items)
        {
            var mappedItems = _mapper.Map<List<TDbModel>>(items);
            mappedItems.ForEach(PrepareForCreation);
            _gradingContext.Set<TDbModel>().AddRange(mappedItems);
        }

        public virtual async Task UpdateAsync(TDataModel item)
        {
            var dbItem = await _gradingContext.Set<TDbModel>().FirstOrDefaultAsync(i => i.Id.Equals(item.Id));

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var mappedItem = _mapper.Map<TDbModel>(item);
            SaveImportantInfo(dbItem, mappedItem);
            _mapper.Map(mappedItem, dbItem);
        }

        public virtual async Task UpdateManyAsync(List<TDataModel> items)
        {
            var itemIds = items.Select(i => i.Id).ToList();
            var dbItems = await _gradingContext.Set<TDbModel>().Where(i => itemIds.Contains(i.Id)).ToListAsync();

            if (itemIds.Count != dbItems.Count)
            {
                throw new Exception(ExceptionMessageConstants.EntitiesAreNotFound);
            }

            var mappedItems = _mapper.Map<List<TDbModel>>(items);
            
            foreach (var item in mappedItems)
            {
                var dbItem = dbItems.FirstOrDefault(i => i.Id.Equals(item.Id));
                SaveImportantInfo(dbItem, item);
                _mapper.Map(item, dbItem);
            }
        }

        public virtual async Task SoftDeleteAsync(Guid id)
        {
            var dbItem = await _gradingContext.Set<TDbModel>().FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            dbItem.IsActive = false;
        }

        public virtual async Task SoftDeleteManyAsync(List<Guid> ids)
        {
            var dbItems = await _gradingContext.Set<TDbModel>().Where(i => ids.Contains(i.Id)).ToListAsync();

            if (ids.Count != dbItems.Count)
            {
                throw new Exception(ExceptionMessageConstants.EntitiesAreNotFound);
            }

            dbItems.ForEach(i => i.IsActive = false);
        }

        public virtual async Task HardDeleteAsync(Guid id)
        {
            var dbItem = await _gradingContext.Set<TDbModel>().FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            _gradingContext.Set<TDbModel>().Remove(dbItem);
        }

        public virtual async Task HardDeleteManyAsync(List<Guid> ids)
        {
            var dbItems = await _gradingContext.Set<TDbModel>().Where(i => ids.Contains(i.Id)).ToListAsync();

            if (ids.Count != dbItems.Count)
            {
                throw new Exception(ExceptionMessageConstants.EntitiesAreNotFound);
            }

            _gradingContext.Set<TDbModel>().RemoveRange(dbItems);
        }

        public async Task<TDataModel> GetByFilterAsync(TFilter filter)
        {
            var source = ConstructFilter(filter);

            var item = await source.FirstOrDefaultAsync();
            var mappedItem = _mapper.Map<TDataModel>(item);

            return mappedItem;
        }

        public async Task<List<TDataModel>> GetAllByFilterAsync(TFilter filter)
        {
            var source = ConstructFilter(filter);

            var items = await source.ToListAsync();
            var mappedItems = _mapper.Map<List<TDataModel>>(items);

            return mappedItems;
        }

        public async Task<PaginationResponse<TDataModel>> GetPaginatedAsync(PaginationRequest<TFilter> request)
        {
            var source = ConstructFilter(request.Filter);

            var response = await _paginationHelper.PaginateAsync(source, request.PageNumber, request.Limit);
            var mappedResponse = _mapper.Map<PaginationResponse<TDataModel>>(response);

            return mappedResponse;
        }

        protected virtual void PrepareForCreation(TDbModel item)
        {
            item.Id = Guid.NewGuid();
            item.IsActive = true;
        }

        protected virtual void SaveImportantInfo(TDbModel beforeSave, TDbModel forSave)
        {
            forSave.Id = beforeSave.Id;
            forSave.IsActive = beforeSave.IsActive;
        }

        private IQueryable<TDbModel> ConstructFilter(TFilter filter)
        {
            var items = _gradingContext.Set<TDbModel>().AsNoTracking().Where(i => i.IsActive);

            if (filter == null)
            {
                return items;
            }

            if (filter.Id.HasValue)
            {
                items = items.Where(i => i.Id.Equals(filter.Id.Value));
            }

            if (filter.Ids != null && filter.Ids.Any())
            {
                items = items.Where(i => filter.Ids.Contains(i.Id));
            }

            items = AddFilterConditions(items, filter);

            return items;
        } 

        protected abstract IQueryable<TDbModel> AddFilterConditions(IQueryable<TDbModel> items, TFilter filter);
    }
}
