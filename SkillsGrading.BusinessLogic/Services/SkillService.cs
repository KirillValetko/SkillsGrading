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
    public class SkillService :
        BaseService<Skill, SkillDataModel, SkillModel, SkillFilter>,
        ISkillService
    {
        private readonly ISkillGroupRepository _skillGroupRepository;

        public SkillService(ISkillRepository repository,
            ISkillGroupRepository skillGroupRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _skillGroupRepository = skillGroupRepository;
        }

        public override async Task CreateAsync(SkillModel item)
        {
            var skillGroup = await _skillGroupRepository.GetByFilterAsync(
                new SkillGroupFilter { Id = item.GroupId });

            if (skillGroup == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var mappedItem = _mapper.Map<SkillDataModel>(item);
            mappedItem.SkillGroup = skillGroup;
            _repository.Create(mappedItem);
            await _unitOfWork.SaveAsync();
        }

        public override async Task UpdateAsync(SkillModel item)
        {
            var dbItem = await _repository.GetByFilterAsync(
                new SkillFilter { Id = item.Id });

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var mappedItem = _mapper.Map<SkillDataModel>(item);
            var skillGroup = await _skillGroupRepository.GetByFilterAsync(
                new SkillGroupFilter { Id = item.GroupId });

            if (skillGroup == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            mappedItem.SkillGroup = skillGroup;

            if (dbItem.IsUsed)
            {
                await _repository.SoftDeleteAsync(mappedItem.Id);
                _repository.Create(mappedItem);
            }
            else
            {
                await _repository.UpdateAsync(mappedItem);
            }

            await _unitOfWork.SaveAsync();
        }

        public override async Task DeleteAsync(Guid id)
        {
            var dbItem = await _repository.GetByFilterAsync(
                new SkillFilter { Id = id });

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            if (dbItem.IsUsed)
            {
                await _repository.SoftDeleteAsync(id);
            }
            else
            {
                await _repository.HardDeleteAsync(id);
            }
        }
    }
}
