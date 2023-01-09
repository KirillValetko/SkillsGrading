using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
                items = items.Where(skillGroup => skillGroup.GroupName.Contains(filter.GroupName));
            }

            items = items.Include(group => 
                group.SkillLevels
                .Where(skillLevel => skillLevel.IsActive)
                .OrderBy(skillLevel => skillLevel.LevelValue));

            return items;
        }

        protected override void PrepareForCreation(SkillGroup item)
        {
            base.PrepareForCreation(item);
            item.IsUsed = false;
            
            foreach (var skillLevel in item.SkillLevels)
            {
                skillLevel.Id = Guid.NewGuid();
                skillLevel.IsActive = true;
                skillLevel.IsUsed = false;
            }
        }
    }
}
