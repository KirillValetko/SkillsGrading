using AutoMapper;
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

        protected override IQueryable<GradeTemplate> AddFilterConditions(IQueryable<GradeTemplate> items, GradeTemplateFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.TemplateName))
            {
                items = items.Where(gradeTemplate => gradeTemplate.TemplateName.Contains(filter.TemplateName));
            }

            return items;
        }

        protected override void PrepareForCreation(GradeTemplate item)
        {
            base.PrepareForCreation(item);
            item.IsUsed = false;
        }
    }
}
