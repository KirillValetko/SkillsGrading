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
    public class GradeTemplateService :
        BaseService<GradeTemplate, GradeTemplateDataModel, GradeTemplateModel, GradeTemplateFilter>,
        IGradeTemplateService
    {
        private readonly IGradedSkillSetRepository _gradedSkillSetRepository;
        private readonly IGradeLevelRepository _gradeLevelRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ISkillLevelRepository _skillLevelRepository;

        public GradeTemplateService(IGradeTemplateRepository repository,
            IGradedSkillSetRepository gradedSkillSetRepository,
            IGradeLevelRepository gradeLevelRepository,
            ISkillRepository skillRepository,
            ISkillLevelRepository skillLevelRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _gradedSkillSetRepository = gradedSkillSetRepository;
            _gradeLevelRepository = gradeLevelRepository;
            _skillRepository = skillRepository;
            _skillLevelRepository = skillLevelRepository;
        }

        public override async Task<GradeTemplateModel> GetByFilterAsync(GradeTemplateFilter filter)
        {
            var gradeTemplate = await _repository.GetByFilterAsync(filter);

            if (gradeTemplate == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var gradedSkillSets = await _gradedSkillSetRepository.GetAllByFilterAsync(
                new GradedSkillSetFilter { GradeTemplateId = filter.Id });
            gradeTemplate.GradedSkillSets = gradedSkillSets;
            var mappedGradeTemplate = _mapper.Map<GradeTemplateModel>(gradeTemplate);

            return mappedGradeTemplate;
        }

        public override async Task CreateAsync(GradeTemplateModel item)
        {
            var templateGradeLevelsIds = item.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.GradeLevelId)
                .Distinct()
                .ToList();
            var maxGradeLevelPosition = item.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.GradeLevelPosition)
                .Max();

            if (templateGradeLevelsIds.Count != maxGradeLevelPosition)
            {
                throw new Exception(ExceptionMessageConstants.IdenticalGradeLevels);
            }

            var dbGradeLevels = await _gradeLevelRepository.GetAllByFilterAsync(new GradeLevelFilter());

            if (templateGradeLevelsIds.Count != dbGradeLevels.Count)
            {
                throw new Exception(ExceptionMessageConstants.WrongGradeLevels);
            }

            var templateSkillsIds = item.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.SkillId)
                .Distinct()
                .ToList();
            var maxSkillPosition = item.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.SkillPosition)
                .Max();

            if (templateSkillsIds.Count != maxSkillPosition)
            {
                throw new Exception(ExceptionMessageConstants.IdenticalSkills);
            }

            var dbSkills = await _skillRepository.GetAllByFilterAsync(
                new SkillFilter { Ids = templateSkillsIds });

            if (templateSkillsIds.Count != dbSkills.Count)
            {
                throw new Exception(ExceptionMessageConstants.WrongSkills);
            }

            var templateSkillLevelsIds = item.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.SkillLevelId)
                .Distinct()
                .ToList();
            var dbSkillLevels = await _skillLevelRepository.GetAllByFilterAsync(
                new SkillLevelFilter { Ids = templateSkillLevelsIds });

            if (templateSkillLevelsIds.Count != dbSkillLevels.Count)
            {
                throw new Exception(ExceptionMessageConstants.WrongSkillLevels);
            }

            var mappedItem = _mapper.Map<GradeTemplateDataModel>(item);
            _repository.Create(mappedItem);
            _gradeLevelRepository.SetGradeLevelsUsed(dbGradeLevels);
            _skillRepository.SetSkillsUsed(dbSkills);
            _skillLevelRepository.SetSkillLevelsUsed(dbSkillLevels);
            await _unitOfWork.SaveAsync();
        }

        public override async Task UpdateAsync(GradeTemplateModel item)
        {
            var dbItem = await _repository.GetByFilterAsync(
               new GradeTemplateFilter { Id = item.Id, IsTracking = true, IncludeGradedSkillSets = true });

            if (dbItem == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var templateGradedSkillSetsIds = item.GradedSkillSets
                .Where(gradedSkillSet => !gradedSkillSet.Id.Equals(Guid.Empty))
                .Select(gradedSkillSet => gradedSkillSet.Id)
                .ToList();
            var dbGradedSkillSetsIds = dbItem.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.Id)
                .Where(gradedSkillSetId => templateGradedSkillSetsIds.Contains(gradedSkillSetId))
                .ToList();

            if (templateGradedSkillSetsIds.Count != dbGradedSkillSetsIds.Count)
            {
                throw new Exception(ExceptionMessageConstants.WrongGradedSkillSets);
            }

            var maxGradeLevelPosition = item.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.GradeLevelPosition)
                .Max();
            var gradeLevelsCount = item.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.GradeLevelId)
                .Distinct()
                .Count();

            if (gradeLevelsCount != maxGradeLevelPosition)
            {
                throw new Exception(ExceptionMessageConstants.IdenticalGradeLevels);
            }

            var templateGradeLevelsIds = item.GradedSkillSets
                .Where(gradedSkillSet => !gradedSkillSet.Id.Equals(Guid.Empty))
                .Select(gradedSkillSet => gradedSkillSet.GradeLevelId)
                .Distinct()
                .ToList();
            var dbGradeLevelsIds = dbItem.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.GradeLevelId)
                .Distinct()
                .Where(gradeLevelId => templateGradeLevelsIds.Contains(gradeLevelId))
                .ToList();

            if (templateGradeLevelsIds.Count != dbGradeLevelsIds.Count)
            {
                throw new Exception(ExceptionMessageConstants.WrongGradeLevels);
            }

            var newTemplateGradeLevelsIds = item.GradedSkillSets
                .Where(gradedSkillSet => gradedSkillSet.Id.Equals(Guid.Empty))
                .Select(gradedSkillSet => gradedSkillSet.GradeLevelId)
                .Distinct()
                .ToList();

            if (newTemplateGradeLevelsIds.Count != 0)
            {
                var newDbGradeLevels = await _gradeLevelRepository.GetAllByFilterAsync(
                    new GradeLevelFilter { Ids = newTemplateGradeLevelsIds });

                if (newTemplateGradeLevelsIds.Count != newDbGradeLevels.Count)
                {
                    throw new Exception(ExceptionMessageConstants.WrongGradeLevels);
                }

                _gradeLevelRepository.SetGradeLevelsUsed(newDbGradeLevels);
            }

            var maxSkillPosition = item.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.SkillPosition)
                .Max();
            var skillsCount = item.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.SkillId)
                .Distinct()
                .Count();

            if (skillsCount != maxSkillPosition)
            {
                throw new Exception(ExceptionMessageConstants.IdenticalSkills);
            }

            var templateSkillsIds = item.GradedSkillSets
                .Where(gradedSkillSet => !gradedSkillSet.Id.Equals(Guid.Empty))
                .Select(gradedSkillSet => gradedSkillSet.SkillId)
                .Distinct()
                .ToList();
            var dbSkillsIds = dbItem.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.SkillId)
                .Distinct()
                .Where(skillId => templateSkillsIds.Contains(skillId))
                .ToList();

            if (templateSkillsIds.Count != dbSkillsIds.Count)
            {
                throw new Exception(ExceptionMessageConstants.WrongSkills);
            }

            var newTemplateSkillsIds = item.GradedSkillSets
                .Where(gradedSkillSet => gradedSkillSet.Id.Equals(Guid.Empty))
                .Select(gradedSkillSet => gradedSkillSet.SkillId)
                .Distinct()
                .ToList();

            if (newTemplateSkillsIds.Count != 0)
            {
                var newDbSkills = await _skillRepository.GetAllByFilterAsync(
                    new SkillFilter { Ids = newTemplateSkillsIds });

                if (newTemplateSkillsIds.Count != newDbSkills.Count)
                {
                    throw new Exception(ExceptionMessageConstants.WrongSkills);
                }

                _skillRepository.SetSkillsUsed(newDbSkills);
            }

            var templateSkillLevelsIds = item.GradedSkillSets
                .Where(gradedSkillSet => !gradedSkillSet.Id.Equals(Guid.Empty))
                .Select(gradedSkillSet => gradedSkillSet.SkillLevelId)
                .Distinct()
                .ToList();
            var dbSkillLevelsIds = dbItem.GradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.SkillLevelId)
                .Distinct()
                .ToList();

            if (templateSkillLevelsIds.Count != dbSkillLevelsIds.Count)
            {
                throw new Exception(ExceptionMessageConstants.WrongSkillLevels);
            }

            var newTemplateSkillLevelsIds = item.GradedSkillSets
                .Where(gradedSkillSet => !gradedSkillSet.Id.Equals(Guid.Empty))
                .Select(gradedSkillSet => gradedSkillSet.SkillLevelId)
                .Distinct()
                .ToList();

            if (newTemplateSkillLevelsIds.Count != 0)
            {
                var newDbSkillLevels = await _skillLevelRepository.GetAllByFilterAsync(
                    new SkillLevelFilter { Ids = newTemplateSkillLevelsIds, OnlyActive = false });

                if (newTemplateSkillLevelsIds.Count != newDbSkillLevels.Count)
                {
                    throw new Exception(ExceptionMessageConstants.WrongSkillLevels);
                }

                _skillLevelRepository.SetSkillLevelsUsed(newDbSkillLevels);
            }

            var mappedGradeTemplate = _mapper.Map<GradeTemplateDataModel>(item);
            await _repository.UpdateAsync(mappedGradeTemplate);
            await _unitOfWork.SaveAsync();
        }

        public override async Task DeleteAsync(Guid id)
        {
            var gradeTemplate = await _repository.GetByFilterAsync(new GradeTemplateFilter { Id = id });

            if (gradeTemplate == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            if (gradeTemplate.IsUsed)
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
