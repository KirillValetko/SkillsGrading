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

        public override async Task DeleteAsync(Guid id)
        {
            var skillGroup = await _repository.GetByFilterAsync(new SkillGroupFilter { Id = id });

            if (skillGroup == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var skillLevelIds = skillGroup.SkillLevels.Select(x => x.Id).ToList();

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
