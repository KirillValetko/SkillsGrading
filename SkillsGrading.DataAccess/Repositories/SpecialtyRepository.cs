using AutoMapper;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Infrastructure;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.DataAccess.Repositories
{
    public class SpecialtyRepository : 
        BaseRepository<Specialty, SpecialtyDataModel, SpecialtyFilter>,
        ISpecialtyRepository
    {
        public SpecialtyRepository(GradingContext gradingContext,
            IPaginationHelper<Specialty> paginationHelper,
            IMapper mapper) : base(gradingContext, paginationHelper, mapper)
        {
        }

        protected override IQueryable<Specialty> AddFilterConditions(IQueryable<Specialty> items, SpecialtyFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.SpecialtyName))
            {
                items = items.Where(specialty => specialty.SpecialtyName.Contains(filter.SpecialtyName));
            }

            if (filter.HasGradeLevelGroup.HasValue)
            {
                if (filter.HasGradeLevelGroup.Value)
                {
                    items = items.Where(specialty =>
                        specialty.GradeLevelGroups.Any(gradeLevelGroup => gradeLevelGroup.IsActive));
                }
                else
                {
                    items = items.Where(specialty =>
                        !specialty.GradeLevelGroups.Any(gradeLevelGroup => gradeLevelGroup.IsActive));
                }
            }

            if (filter.IsUsed.HasValue && !filter.IsUsed.Value)
            {
                items = items.Where(specialty => !specialty.IsUsed);
            }

            return items;
        }
    }
}
