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

        public GradeTemplateService(IGradeTemplateRepository repository,
            IGradedSkillSetRepository gradedSkillSetRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _gradedSkillSetRepository = gradedSkillSetRepository;
        }

        public async Task<GradeTemplateModel> GetGradedSkillSets(Guid gradeTemplateId)
        {
            var gradeTemplate = await _repository.GetByFilterAsync(new GradeTemplateFilter { Id = gradeTemplateId });

            if (gradeTemplate == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var gradedSkillSets = await _gradedSkillSetRepository.GetAllByFilterAsync(new GradedSkillSetFilter
                { GradeTemplateId = gradeTemplateId });
            gradeTemplate.GradedSkillSets = gradedSkillSets;
            var mappedGradeTemplate = _mapper.Map<GradeTemplateModel>(gradeTemplate);

            return mappedGradeTemplate;
        }
    }
}
