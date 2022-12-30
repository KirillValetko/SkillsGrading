using AutoMapper;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Infrastructure;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.DataAccess.Repositories
{
    public class SkillGroupRepository :
        BaseRepository<SkillGroup, SkillGroupDataModel, SkillGroupFilter>,
        ISkillGroupRepository
    {
        public SkillGroupRepository(GradingContext gradingContext,
            IPaginationHelper<SkillGroup> paginationHelper,
            IMapper mapper) : base(gradingContext, paginationHelper, mapper)
        {
        }

        protected override IQueryable<SkillGroup> AddFilterConditions(IQueryable<SkillGroup> items,
            SkillGroupFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.GroupName))
            {
                items = items.Where(x => x.GroupName.Contains(filter.GroupName));
            }
            
            return items;
        }
    }
}
