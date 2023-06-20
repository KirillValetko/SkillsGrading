using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SkillsGrading.Common.Constants;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Infrastructure;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.DataAccess.Repositories
{
    public class GradeLevelGroupRepository :
        BaseRepository<GradeLevelGroup, GradeLevelGroupDataModel, GradeLevelGroupFilter>,
        IGradeLevelGroupRepository
    {
        public GradeLevelGroupRepository(GradingContext gradingContext,
            IPaginationHelper<GradeLevelGroup> paginationHelper,
            IMapper mapper) : base(gradingContext, paginationHelper, mapper)
        {
        }

        public override async Task SoftDeleteAsync(Guid id)
        {
            var dbItem = await _gradingContext.GradeLevelGroups
                .Include(gradeLevelGroup => gradeLevelGroup.GradeLevels
                    .Where(gradeLevel => gradeLevel.IsActive))
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            dbItem.IsActive = false;
            dbItem.GradeLevels.ForEach(gradeLevel => gradeLevel.IsActive = false);
        }

        protected override IQueryable<GradeLevelGroup> AddFilterConditions(IQueryable<GradeLevelGroup> items, GradeLevelGroupFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.GroupName))
            {
                items = items.Where(gradeLevelGroup => gradeLevelGroup.GroupName.Contains(filter.GroupName));
            }

            if (filter.SpecialtyId.HasValue)
            {
                items = items.Where(gradeLevelGroup => gradeLevelGroup.SpecialtyId.Equals(filter.SpecialtyId.Value));
            }

            items = items
                .Include(gradeLevelGroup => gradeLevelGroup.GradeLevels
                    .Where(gradeLevel => gradeLevel.IsActive)
                    .OrderBy(gradeLevel => gradeLevel.LevelValue))
                .OrderBy(gradeLevelGroup => gradeLevelGroup.SpecialtyId);

            return items;
        }

        protected override void PrepareForCreation(GradeLevelGroup item)
        {
            base.PrepareForCreation(item);

            foreach (var gradeLevel in item.GradeLevels)
            {
                gradeLevel.Id = Guid.NewGuid();
                gradeLevel.IsActive = true;
                gradeLevel.IsUsed = false;
            }
        }
    }
}
