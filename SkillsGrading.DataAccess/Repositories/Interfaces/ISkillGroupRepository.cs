﻿using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;

namespace SkillsGrading.DataAccess.Repositories.Interfaces
{
    public interface ISkillGroupRepository : IBaseRepository<SkillGroup, SkillGroupDataModel, SkillGroupFilter>
    {
    }
}
