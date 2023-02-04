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

            if (filter.IsFull.HasValue)
            {
                if (filter.IsFull.Value)
                {
                    items = items.Where(specialty =>
                        specialty.GradeLevelGroups.Count(gradeLevel => gradeLevel.IsActive) == 3);
                }
                else
                {
                    items = items.Where(specialty =>
                        specialty.GradeLevelGroups.Count(gradeLevel => gradeLevel.IsActive) != 3);
                }
            }

            return items;
        }
    }
}
