using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.BusinessLogic.Services.Interfaces;
using SkillsGrading.Common.Helpers.Interfaces;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.Web.Enums;
using SkillsGrading.Web.Models.DtoModels;
using SkillsGrading.Web.Models.ViewModels;

namespace SkillsGrading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeLevelGroupController : BaseController
    {
        private readonly IGradeLevelGroupService _gradeLevelGroupService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IEnumHelper<GradeLevelGroupValues> _enumHelper;

        public GradeLevelGroupController(IGradeLevelGroupService gradeLevelGroupService,
            ISpecialtyService specialtyService,
            IEnumHelper<GradeLevelGroupValues> enumHelper,
            IMapper mapper,
            ILogger<BaseController> logger) : base(mapper, logger)
        {
            _gradeLevelGroupService = gradeLevelGroupService;
            _specialtyService = specialtyService;
            _enumHelper = enumHelper;
        }

        [HttpGet]
        public Task<IActionResult> GetAsync([FromQuery] PaginationRequest<GradeLevelGroupFilter> request)
        {
            return ProcessRequest<PaginationResponse<GradeLevelGroupModel>, PaginationResponse<GradeLevelGroupViewModel>>(() =>
                _gradeLevelGroupService.GetPaginatedAsync(request));
        }

        [HttpPost]
        public Task<IActionResult> PostAsync(GradeLevelGroupDto item)
        {
            var mappedItem = _mapper.Map<GradeLevelGroupModel>(item);

            return ProcessRequest<GradeLevelGroupViewModel>(() => _gradeLevelGroupService.CreateAsync(mappedItem));
        }

        [HttpPut]
        public Task<IActionResult> PutAsync(GradeLevelGroupDto item)
        {
            var mappedItem = _mapper.Map<GradeLevelGroupModel>(item);

            return ProcessRequest<GradeLevelGroupViewModel>(() => _gradeLevelGroupService.UpdateAsync(mappedItem));
        }

        [HttpDelete]
        public Task<IActionResult> DeleteAsync(Guid id)
        {
            return ProcessRequest<GradeLevelGroupViewModel>(() => _gradeLevelGroupService.DeleteAsync(id));
        }

        [HttpGet("GroupValues")]
        public IActionResult Get()
        {
            return ProcessRequest(() => _enumHelper.GetAllEnumValues());
        }

        [HttpGet("Specialties")]
        public Task<IActionResult> GetAsync()
        {
            return ProcessRequest<List<SpecialtyModel>, List<SpecialtyViewModel>>(() =>
                _specialtyService.GetAllByFilterAsync(new SpecialtyFilter { IsFull = false }));
        }
    }
}
