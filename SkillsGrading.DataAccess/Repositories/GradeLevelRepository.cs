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
            items = items.OrderBy(gradeLevel => gradeLevel.LevelValue);

            return items;
        }

        public void SetGradeLevelsUsed(List<GradeLevelDataModel> gradeLevels)
        {
            gradeLevels.ForEach(gradeLevel => gradeLevel.IsUsed = true);
            var mappedGradeLevels = _mapper.Map<List<GradeLevel>>(gradeLevels);

            foreach (var gradeLevel in mappedGradeLevels)
            {
                _gradingContext.Entry(gradeLevel).State = EntityState.Modified;
            }
        }
    }
}
