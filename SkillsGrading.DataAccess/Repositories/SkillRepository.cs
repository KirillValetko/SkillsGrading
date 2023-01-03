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
    public class SkillRepository :
        BaseRepository<Skill, SkillDataModel, SkillFilter>,
        ISkillRepository
    {
        public SkillRepository(GradingContext gradingContext,
            IPaginationHelper<Skill> paginationHelper,
            IMapper mapper) : base(gradingContext, paginationHelper, mapper)
        {
        }

        protected override IQueryable<Skill> AddFilterConditions(IQueryable<Skill> items, SkillFilter filter)
        {
            if (!string.IsNullOrEmpty(filter.SkillName))
            {
                items = items.Where(i => i.SkillName.Contains(filter.SkillName));
            }

            items = items.Include(i => i.SkillGroup);

            return items;
        }

        protected override void SaveImportantInfo(Skill beforeSave, Skill forSave)
        {
            base.SaveImportantInfo(beforeSave, forSave);
            forSave.SkillGroup.IsUsed = true;
        }

        protected override void PrepareForCreation(Skill item)
        {
            base.PrepareForCreation(item);
            item.IsUsed = false;
            item.SkillGroup.IsUsed = true;
        }
    }
}
