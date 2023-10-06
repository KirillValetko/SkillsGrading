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
    public class GradeTemplateRepository :
        BaseRepository<GradeTemplate, GradeTemplateDataModel, GradeTemplateFilter>,
        IGradeTemplateRepository
    {
        public GradeTemplateRepository(GradingContext gradingContext,
            IPaginationHelper<GradeTemplate> paginationHelper,
            IMapper mapper) : base(gradingContext, paginationHelper, mapper)
        {
        }

        public override async Task SoftDeleteAsync(Guid id)
        {
            var dbItem = await _gradingContext.GradeTemplates
                .Include(gradeTemplate => gradeTemplate.GradedSkillSets
                    .Where(gradedSkillSet => gradedSkillSet.IsActive))
                .FirstOrDefaultAsync(i => i.Id.Equals(id));

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            dbItem.IsActive = false;
            dbItem.GradedSkillSets.ForEach(skillLevel => skillLevel.IsActive = false);
        }

        protected override IQueryable<GradeTemplate> AddFilterConditions(IQueryable<GradeTemplate> items, GradeTemplateFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.TemplateName))
            {
                items = items.Where(gradeTemplate => gradeTemplate.TemplateName.Contains(filter.TemplateName));
            }

            if (filter.IncludeGradedSkillSets.HasValue && filter.IncludeGradedSkillSets.Value)
            {
                items = items.Include(gradeTemplate => gradeTemplate.GradedSkillSets
                    .Where(gradedSkillSet => gradedSkillSet.IsActive)
                    .OrderBy(gradedSkillSet => gradedSkillSet.SkillPosition)
                    .ThenBy(gradedSkillSet => gradedSkillSet.GradeLevelPosition));
            }

            return items;
        }

        protected override void PrepareForCreation(GradeTemplate item)
        {
            base.PrepareForCreation(item);
            item.IsUsed = false;

            foreach (var gradedSkillSet in item.GradedSkillSets)
            {
                gradedSkillSet.Id = Guid.NewGuid();
                gradedSkillSet.IsActive = true;
            }
        }

        protected override void SaveImportantInfo(GradeTemplate beforeSave, GradeTemplateDataModel forSave)
        {
            base.SaveImportantInfo(beforeSave, forSave);
            forSave.IsUsed = beforeSave.IsUsed;
        }
    }
}
