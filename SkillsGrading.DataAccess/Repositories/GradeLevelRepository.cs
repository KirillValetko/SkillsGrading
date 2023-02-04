using AutoMapper;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Infrastructure;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.DataAccess.Repositories
{
    public class GradeLevelRepository : 
        BaseRepository<GradeLevel, GradeLevelDataModel, GradeLevelFilter>,
        IGradeLevelRepository
    {
        public GradeLevelRepository(GradingContext gradingContext,
            IPaginationHelper<GradeLevel> paginationHelper,
            IMapper mapper) : base(gradingContext, paginationHelper, mapper)
        {
        }

        protected override IQueryable<GradeLevel> AddFilterConditions(IQueryable<GradeLevel> items, GradeLevelFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.LevelName))
            {
                items = items.Where(gradeLevel => gradeLevel.LevelName.Contains(filter.LevelName));
            }

            if (filter.LevelValue.HasValue)
            {
                items = items.Where(gradeLevel => gradeLevel.LevelValue > filter.LevelValue.Value);
            }

            if (filter.Salary.HasValue)
            {
                items = items.Where(gradeLevel => gradeLevel.Salary > filter.Salary.Value);
            }

            if (filter.GradeRevisionInMonths.HasValue)
            {
                items = items.Where(gradeLevel => gradeLevel.GradeRevisionInMonths > filter.GradeRevisionInMonths.Value);
            }

            if (filter.SpecialtyId.HasValue)
            {
                items = items.Where(gradeLevel => gradeLevel.GradeLevelGroup.SpecialtyId.Equals(filter.SpecialtyId.Value));
            }

            items = items.OrderBy(gradeLevel => gradeLevel.LevelValue);

            return items;
        }
    }
}
