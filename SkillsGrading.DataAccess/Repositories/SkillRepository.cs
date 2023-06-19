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
                items = items.Where(skill => skill.SkillName.Contains(filter.SkillName));
            }

            items = items.Include(skill => skill.SkillGroup)
                .ThenInclude(skillGroup => skillGroup.SkillLevels
                    .Where(skillLevel => skillLevel.IsActive)
                    .OrderBy(skillLevel => skillLevel.LevelValue));

            return items;
        }

        protected override void SaveImportantInfo(Skill beforeSave, SkillDataModel forSave)
        {
            base.SaveImportantInfo(beforeSave, forSave);
            forSave.IsUsed = beforeSave.IsUsed;
            forSave.SkillGroup.IsUsed = true;
            var mappedSkillGroup = _mapper.Map<SkillGroup>(forSave.SkillGroup);
            _gradingContext.Entry(mappedSkillGroup).State = EntityState.Modified;
        }

        protected override void PrepareForCreation(Skill item)
        {
            base.PrepareForCreation(item);
            item.IsUsed = false;
            item.SkillGroup.IsUsed = true;
            _gradingContext.Entry(item.SkillGroup).State = EntityState.Modified;
        }

        public void SetSkillsUsed(List<SkillDataModel> skills)
        {
            skills.ForEach(skill => skill.IsUsed = true);
            var mappedSkills = _mapper.Map<List<Skill>>(skills);

            foreach (var skill in mappedSkills)
            {
                _gradingContext.Entry(skill).State = EntityState.Modified;
            }
        }
    }
}
