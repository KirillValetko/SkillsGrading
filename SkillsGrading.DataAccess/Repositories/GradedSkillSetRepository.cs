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
    public class GradedSkillSetRepository :
        BaseRepository<GradedSkillSet, GradedSkillSetDataModel, GradedSkillSetFilter>,
        IGradedSkillSetRepository
    {
        public GradedSkillSetRepository(GradingContext gradingContext,
            IPaginationHelper<GradedSkillSet> paginationHelper,
            IMapper mapper) : base(gradingContext, paginationHelper, mapper)
        {
        }

        protected override IQueryable<GradedSkillSet> AddFilterConditions(IQueryable<GradedSkillSet> items, GradedSkillSetFilter filter)
        {
            if (filter.GradeTemplateId.HasValue)
            {
                items = items.Where(gradedSkillSet =>
                    gradedSkillSet.GradeTemplateId.Equals(filter.GradeTemplateId.Value));
            }

            items = items
                .Include(gradedSkillSet => gradedSkillSet.SkillLevel)
                .Include(gradedSkillSet => gradedSkillSet.GradeLevel)
                .Include(gradedSkillSet => gradedSkillSet.Skill.SkillGroup.SkillLevels
                    .Where(skillLevel => skillLevel.IsActive))
                .OrderBy(gradedSkillSet => gradedSkillSet.SkillPosition)
                .ThenBy(gradedSkillSet => gradedSkillSet.GradeLevelPosition);

            return items;
        }
    }
}
