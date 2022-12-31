using AutoMapper;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.BusinessLogic.Services.Interfaces;
using SkillsGrading.Common.Constants;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.BusinessLogic.Services
{
    public class SkillGroupService :
        BaseService<SkillGroup, SkillGroupDataModel, SkillGroupModel, SkillGroupFilter>,
        ISkillGroupService
    {
        private readonly ISkillLevelRepository _skillLevelRepository;

        public SkillGroupService(ISkillGroupRepository repository,
            ISkillLevelRepository skillLevelRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _skillLevelRepository = skillLevelRepository;
        }

        public override async Task UpdateAsync(SkillGroupModel item)
        {
            var dbItem = await _repository.GetByFilterAsync(new SkillGroupFilter { Id = item.Id });

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var mappedSkillGroup = _mapper.Map<SkillGroupDataModel>(item);
            var skillLevelsToCreate = mappedSkillGroup.SkillLevels
                .Where(i => i.Id.Equals(Guid.Empty))
                .ToList();
            var skillLevelsToDelete = dbItem.SkillLevels
                .ExceptBy(mappedSkillGroup.SkillLevels.Select(i => i.Id), i => i.Id)
                .ToList();

            if (dbItem.IsUsed)
            {
                var skillLevelsToCheck = dbItem.SkillLevels.Except(skillLevelsToDelete).ToList();
                var usedSkillLevelsToUpdate = new List<SkillLevelDataModel>();
                var unusedSkillLevelsToUpdate = new List<SkillLevelDataModel>();

                foreach (var skillLevel in skillLevelsToCheck)
                {
                    var modifiedSkillLevel = mappedSkillGroup.SkillLevels
                        .FirstOrDefault(i => i.Id.Equals(skillLevel.Id));
                    if (!skillLevel.Equals(modifiedSkillLevel))
                    {
                        if (skillLevel.IsUsed)
                        {
                            usedSkillLevelsToUpdate.Add(modifiedSkillLevel);
                        }
                        else
                        {
                            unusedSkillLevelsToUpdate.Add(modifiedSkillLevel);
                        }
                    }
                }

                await _skillLevelRepository.UpdateManyAsync(unusedSkillLevelsToUpdate);

                var usedSkillLevelIdsToUpdate = usedSkillLevelsToUpdate
                    .Select(i => i.Id)
                    .ToList();

                var skillLevelsIdsToHardDelete = skillLevelsToDelete
                    .Where(i => !i.IsUsed)
                    .Select(i => i.Id)
                    .ToList();
                await _skillLevelRepository.HardDeleteManyAsync(skillLevelsIdsToHardDelete);

                var skillLevelIdsToSoftDelete = skillLevelsToDelete
                    .Select(i => i.Id)
                    .Except(skillLevelsIdsToHardDelete)
                    .ToList();
                skillLevelIdsToSoftDelete.AddRange(usedSkillLevelIdsToUpdate);
                await _skillLevelRepository.SoftDeleteManyAsync(skillLevelIdsToSoftDelete);

                usedSkillLevelsToUpdate.AddRange(skillLevelsToCreate);
                _skillLevelRepository.CreateMany(usedSkillLevelsToUpdate);
            }
            else
            {
                _skillLevelRepository.CreateMany(skillLevelsToCreate);
                var skillLevelsIdsToDelete = skillLevelsToDelete.Select(i => i.Id).ToList();
                await _skillLevelRepository.HardDeleteManyAsync(skillLevelsIdsToDelete);
            }

            await _repository.UpdateAsync(mappedSkillGroup);
            await _unitOfWork.SaveAsync();
        }

        public override async Task DeleteAsync(Guid id)
        {
            var skillGroup = await _repository.GetByFilterAsync(new SkillGroupFilter { Id = id });

            if (skillGroup == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var skillLevelIds = skillGroup.SkillLevels.Select(i => i.Id).ToList();

            if (skillGroup.IsUsed)
            {
                await _repository.SoftDeleteAsync(id);
                await _skillLevelRepository.SoftDeleteManyAsync(skillLevelIds);
            }
            else
            {
                await _repository.HardDeleteAsync(id);
                await _skillLevelRepository.HardDeleteManyAsync(skillLevelIds);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task<List<SkillLevelModel>> GetSkillLevelsAsync(Guid id)
        {
            var skillGroup = await GetByFilterAsync(new SkillGroupFilter { Id = id });
            var skillLevels = skillGroup.SkillLevels;

            return skillLevels;
        }
    }
}
