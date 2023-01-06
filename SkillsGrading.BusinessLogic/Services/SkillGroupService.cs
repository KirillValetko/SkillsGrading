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
                .Where(skillLevel => skillLevel.Id.Equals(Guid.Empty))
                .ToList();
            var skillLevelsToDelete = dbItem.SkillLevels
                .ExceptBy(mappedSkillGroup.SkillLevels.Select(skillLevel => skillLevel.Id), skillLevel => skillLevel.Id)
                .ToList();

            if (dbItem.IsUsed)
            {
                var skillLevelsToCheck = dbItem.SkillLevels.Except(skillLevelsToDelete).ToList();
                var usedSkillLevelsToUpdate = new List<SkillLevelDataModel>();
                var unusedSkillLevelsToUpdate = new List<SkillLevelDataModel>();

                foreach (var skillLevelToCheck in skillLevelsToCheck)
                {
                    var modifiedSkillLevel = mappedSkillGroup.SkillLevels
                        .FirstOrDefault(skillLevel => skillLevel.Id.Equals(skillLevel.Id));
                    if (!skillLevelToCheck.Equals(modifiedSkillLevel))
                    {
                        if (skillLevelToCheck.IsUsed)
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
                    .Select(skillLevel => skillLevel.Id)
                    .ToList();

                var skillLevelsIdsToHardDelete = skillLevelsToDelete
                    .Where(skillLevel => !skillLevel.IsUsed)
                    .Select(skillLevel => skillLevel.Id)
                    .ToList();
                await _skillLevelRepository.HardDeleteManyAsync(skillLevelsIdsToHardDelete);

                var skillLevelIdsToSoftDelete = skillLevelsToDelete
                    .Select(skillLevel => skillLevel.Id)
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
                var skillLevelsIdsToDelete = skillLevelsToDelete.Select(skillLevel => skillLevel.Id).ToList();
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

            var skillLevelIds = skillGroup.SkillLevels.Select(skillLevel => skillLevel.Id).ToList();

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
    }
}
