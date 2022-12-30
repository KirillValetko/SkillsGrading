using AutoMapper;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Infrastructure;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.DataAccess.Repositories
{
    public class SkillLevelRepository :
        BaseRepository<SkillLevel, SkillLevelDataModel, SkillLevelFilter>,
        ISkillLevelRepository
    {
        public SkillLevelRepository(GradingContext gradingContext,
            IPaginationHelper<SkillLevel> paginationHelper,
            IMapper mapper) : base(gradingContext, paginationHelper, mapper)
        {
        }

        protected override IQueryable<SkillLevel> AddFilterConditions(IQueryable<SkillLevel> items,
            SkillLevelFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.LevelName))
            {
                items = items.Where(i => i.LevelName.Contains(filter.LevelName));
            }

            if (filter.LevelValue.HasValue)
            {
                items = items.Where(i => i.LevelValue > filter.LevelValue.Value);
            }

            if (!string.IsNullOrEmpty(filter.Description))
            {
                items = items.Where(i => i.Description.Contains(filter.Description));
            }

            return items;
        }
    }
}
