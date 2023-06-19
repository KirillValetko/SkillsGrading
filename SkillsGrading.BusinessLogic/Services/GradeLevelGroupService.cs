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
    public class GradeLevelGroupService :
        BaseService<GradeLevelGroup, GradeLevelGroupDataModel, GradeLevelGroupModel, GradeLevelGroupFilter>,
        IGradeLevelGroupService
    {
        private readonly ISpecialtyRepository _specialtyRepository;

        public GradeLevelGroupService(IGradeLevelGroupRepository repository,
            ISpecialtyRepository specialtyRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _specialtyRepository = specialtyRepository;
        }

        public override async Task CreateAsync(GradeLevelGroupModel item)
        {
            var specialty = await _specialtyRepository.GetByFilterAsync(new SpecialtyFilter { Id = item.SpecialtyId });

            if (specialty == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var mappedItem = _mapper.Map<GradeLevelGroupDataModel>(item);
            _repository.Create(mappedItem);
            await _unitOfWork.SaveAsync();
        }

        public override async Task UpdateAsync(GradeLevelGroupModel item)
        {
            var dbItem = await _repository.GetByFilterAsync(new GradeLevelGroupFilter
                { Id = item.Id, IsTracking = true });

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            if (dbItem.SpecialtyId != item.SpecialtyId)
            {
                throw new Exception(ExceptionMessageConstants.DifferentSpecialties);
            }

            var mappedGradeLevelGroup = _mapper.Map<GradeLevelGroupDataModel>(item);
            var isUsed = dbItem.GradeLevels
                .Any(gradeLevel => gradeLevel.IsUsed);

            if (isUsed)
            {
                var gradeLevels = new List<GradeLevelDataModel>();
                var gradeLevelsToCreate = mappedGradeLevelGroup.GradeLevels
                    .Where(gradeLevel => gradeLevel.Id.Equals(Guid.Empty))
                    .ToList();
                var gradeLevelsToSoftDelete = dbItem.GradeLevels
                    .Where(gradeLevel => gradeLevel.IsUsed)
                    .ExceptBy(mappedGradeLevelGroup.GradeLevels.Select(gradeLevel => gradeLevel.Id), gradeLevel => gradeLevel.Id)
                    .ToList();
                var gradeLevelsToCheck = dbItem.GradeLevels
                    .IntersectBy(mappedGradeLevelGroup.GradeLevels.Select(gradeLevel => gradeLevel.Id), gradeLevel => gradeLevel.Id)
                    .ToList();

                foreach (var srcGradeLevel in gradeLevelsToCheck)
                {
                    var destGradeLevel = mappedGradeLevelGroup.GradeLevels
                        .FirstOrDefault(gradeLevel => gradeLevel.Id.Equals(srcGradeLevel.Id));

                    if (srcGradeLevel.IsUsed)
                    {
                        if (srcGradeLevel.Equals(destGradeLevel))
                        {
                            gradeLevelsToCreate.Add(srcGradeLevel);
                        }
                        else
                        {
                            gradeLevelsToSoftDelete.Add(srcGradeLevel);
                            destGradeLevel!.Id = Guid.Empty;
                            gradeLevelsToCreate.Add(destGradeLevel);
                        }
                    }
                    else
                    {
                        gradeLevelsToCreate.Add(destGradeLevel);
                    }
                }

                gradeLevels.AddRange(gradeLevelsToCreate);
                gradeLevelsToSoftDelete.ForEach(skillLevel => skillLevel.IsActive = false);
                gradeLevels.AddRange(gradeLevelsToSoftDelete);
                mappedGradeLevelGroup.GradeLevels = gradeLevels;
            }

            await _repository.UpdateAsync(mappedGradeLevelGroup);
            await _unitOfWork.SaveAsync();
        }

        public override async Task DeleteAsync(Guid id)
        {
            var gradeLevelGroup = await _repository.GetByFilterAsync(new GradeLevelGroupFilter { Id = id });

            if (gradeLevelGroup == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var isUsed = gradeLevelGroup.GradeLevels
                .Any(gradeLevel => gradeLevel.IsUsed);

            if (isUsed)
            {
                await _repository.SoftDeleteAsync(id);
            }
            else
            {
                await _repository.HardDeleteAsync(id);
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
