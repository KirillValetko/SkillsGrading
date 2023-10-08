using System.Text.Json;
using AutoMapper;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.BusinessLogic.Responses;
using SkillsGrading.BusinessLogic.Services.Interfaces;
using SkillsGrading.Common.Constants;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.DataModels;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.DataAccess.Models;
using SkillsGrading.DataAccess.Repositories.Interfaces;

namespace SkillsGrading.BusinessLogic.Services
{
    public class EmployeeService :
        BaseService<Employee, EmployeeDataModel, EmployeeModel, EmployeeFilter>,
        IEmployeeService
    {
        private readonly IGradeTemplateRepository _gradeTemplateRepository;
        private readonly IGradedSkillSetRepository _gradedSkillSetRepository;
        private readonly ITokenService _tokenService;
        private readonly HttpClient _httpClient;

        public EmployeeService(IEmployeeRepository repository,
            IGradeTemplateRepository gradeTemplateRepository,
            IGradedSkillSetRepository gradedSkillSetRepository,
            ITokenService tokenService,
            HttpClient httpClient,
            IUnitOfWork unitOfWork,
            IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _gradeTemplateRepository = gradeTemplateRepository;
            _gradedSkillSetRepository = gradedSkillSetRepository;
            _tokenService = tokenService;
            _httpClient = httpClient;
        }

        public override async Task UpdateAsync(EmployeeModel item)
        {
            var employeesIds = new List<Guid> { item.Id, item.GraderId.Value }.Distinct().ToList();
            var idsQueryParams = string.Empty;
            
            foreach (var id in employeesIds)
            {
                if (!string.IsNullOrEmpty(idsQueryParams))
                {
                    idsQueryParams += RequestConstants.Ampersand;
                }

                idsQueryParams += RequestConstants.IdsQueryParam + RequestConstants.EqualsSign + id;
            }

            var request = new HttpRequestMessage(HttpMethod.Get,
                RequestConstants.EmployeePath + idsQueryParams);
            var token = _tokenService.GetToken();
            request.Headers.Add(AuthorizationConstants.Authorization,token);
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var apiResponse = await JsonSerializer.DeserializeAsync<ApiResponse<PaginationResponse<EmployeeModel>>>(responseStream, options);
                var employees = apiResponse.Payload.PaginatedData;

                if (employees.Count != employeesIds.Count)
                {
                    throw new Exception(ExceptionMessageConstants.EntitiesAreNotFound);
                }
            }
            else
            {
                throw new Exception(ExceptionMessageConstants.NotAuthorized);
            }

            var dbItem = await _repository.GetByFilterAsync(new EmployeeFilter { Id = item.Id });

            if (dbItem == null)
            {
                var mappedItem = _mapper.Map<EmployeeDataModel>(item);
                _repository.Create(mappedItem);
            }
            else
            {
                var mappedItem = _mapper.Map<EmployeeDataModel>(item);
                await _repository.UpdateAsync(mappedItem);
            }

            await _unitOfWork.SaveAsync();
        }

        public async Task GradeAsync(GradingModel item)
        {
            var employee = await _repository.GetByFilterAsync(new EmployeeFilter { Id = item.EmployeeId });

            if (employee == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            if (!employee.GraderId.HasValue)
            {
                throw new Exception(ExceptionMessageConstants.NoGrader);
            }

            var gradeTemplate = await _gradeTemplateRepository.GetByFilterAsync(
                new GradeTemplateFilter { Id = item.GradeTemplateId});

            if (gradeTemplate == null)
            {
                throw new Exception(ExceptionMessageConstants.EntityIsNotFound);
            }

            var gradedSkillSets = await _gradedSkillSetRepository.GetAllByFilterAsync(
                new GradedSkillSetFilter { GradeTemplateId = item.GradeTemplateId });

            var gradingSkillsIds = item.SkillSets
                .Select(skillSet => skillSet.SkillId)
                .ToList();
            var dbSkills = gradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.SkillId)
                .Distinct()
                .ToList();

            if (gradingSkillsIds.Count != dbSkills.Count)
            {
                throw new Exception(ExceptionMessageConstants.WrongSkills);
            }

            var gradingSkillLevelsIds = item.SkillSets
                .Select(skillSet => skillSet.SkillLevelId)
                .Distinct()
                .ToList();
            var dbSkillLevels = gradedSkillSets
                .Select(gradedSkillSet => gradedSkillSet.Skill)
                .DistinctBy(skill => skill.SkillGroup)
                .SelectMany(skill => skill.SkillGroup.SkillLevels)
                .Where(skillLevel => gradingSkillLevelsIds.Contains(skillLevel.Id))
                .DistinctBy(skillLevel => skillLevel.Id)
                .ToList();

            if (gradingSkillLevelsIds.Count != dbSkillLevels.Count)
            {
                throw new Exception(ExceptionMessageConstants.WrongSkillLevels);
            }

            var gradeLevelPositionSum = .0;

            foreach (var skillSet in item.SkillSets)
            {
                var gradedSkillSetWithMinGradeLevelPosition = gradedSkillSets
                    .Where(gradedSkillSet => gradedSkillSet.SkillId.Equals(skillSet.SkillId) 
                                             && gradedSkillSet.SkillLevelId.Equals(skillSet.SkillLevelId))
                    .MinBy(gradedSkillSet => gradedSkillSet.GradeLevelPosition);

                if (gradedSkillSetWithMinGradeLevelPosition == null)
                {
                    var currentSkillLevel = dbSkillLevels        
                        .FirstOrDefault(skillLevel => skillLevel.Id.Equals(skillSet.SkillLevelId));
                    var previousOrEqualSkillLevel = gradedSkillSets
                        .Where(gradedSkillSet => gradedSkillSet.SkillId.Equals(skillSet.SkillId)
                                                 && gradedSkillSet.SkillLevel.LevelValue <=
                                                 currentSkillLevel.LevelValue)
                        .Select(gradedSkillSet => gradedSkillSet.SkillLevel)
                        .MaxBy(skillLevel => skillLevel.LevelValue);

                    if (previousOrEqualSkillLevel != null)
                    {
                        var gradedSkillSetWithMaxGradeLevelPosition = gradedSkillSets
                            .Where(gradedSkillSet => gradedSkillSet.SkillId.Equals(skillSet.SkillId) &&
                                                     gradedSkillSet.SkillLevelId.Equals(previousOrEqualSkillLevel.Id))
                            .MaxBy(gradedSkillSet => gradedSkillSet.GradeLevelPosition);
                        gradeLevelPositionSum += gradedSkillSetWithMaxGradeLevelPosition.GradeLevelPosition;
                    }
                }
                else
                {
                    var gradedSkillSetWithMaxGradeLevelPosition = gradedSkillSets
                        .Where(gradedSkillSet => gradedSkillSet.SkillId.Equals(skillSet.SkillId) &&
                                                 gradedSkillSet.SkillLevelId.Equals(skillSet.SkillLevelId))
                        .MaxBy(gradedSkillSet => gradedSkillSet.GradeLevelPosition);
                    gradeLevelPositionSum += (gradedSkillSetWithMinGradeLevelPosition.GradeLevelPosition +
                                                 gradedSkillSetWithMaxGradeLevelPosition!.GradeLevelPosition) / 2.0;
                }
            }
            
            var averageGradeLevelPosition = (int) Math.Ceiling(gradeLevelPositionSum / gradingSkillsIds.Count);
            var newGradeLevel = gradedSkillSets
                .Where(gradedSkillSet => gradedSkillSet.GradeLevelPosition ==  averageGradeLevelPosition)
                .Select(gradedSkillSet => gradedSkillSet.GradeLevelId)
                .FirstOrDefault();
            gradeTemplate.IsUsed = true;
            employee.Grades.Add(new GradeDataModel
            {
                GradeDate = DateTime.UtcNow,
                GradeTemplateId = item.GradeTemplateId,
                NewGradeLevelId = newGradeLevel,
                EmployeeId = employee.Id,
                IsActive = true,
                GradeTemplate = gradeTemplate
            });
            await _repository.UpdateAsync(employee);
            await _unitOfWork.SaveAsync();
        }
    }
}
