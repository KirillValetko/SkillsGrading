using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.BusinessLogic.Services.Interfaces;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.Web.Models.DtoModels;
using SkillsGrading.Web.Models.ViewModels;

namespace SkillsGrading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillGroupController : BaseController
    {
        private readonly ISkillGroupService _skillGroupService;

        public SkillGroupController(ISkillGroupService skillGroupService,
            IMapper mapper,
            ILogger<BaseController> logger) : base(mapper, logger)
        {
            _skillGroupService = skillGroupService;
        }

        [HttpGet]
        public Task<IActionResult> GetAsync([FromQuery]PaginationRequest<SkillGroupFilter> request)
        {
            return ProcessRequest<PaginationResponse<SkillGroupModel>, PaginationResponse<SkillGroupViewModel>>(() => 
                _skillGroupService.GetPaginatedAsync(request));
        }

        [HttpPost]
        public Task<IActionResult> PostAsync(SkillGroupDto item)
        {
            var mappedItem = _mapper.Map<SkillGroupModel>(item);

            return ProcessRequest<SkillGroupViewModel>(() => _skillGroupService.CreateAsync(mappedItem));
        }

        [HttpPut]
        public Task<IActionResult> PutAsync(SkillGroupDto item)
        {
            var mappedItem = _mapper.Map<SkillGroupModel>(item);

            return ProcessRequest<SkillGroupViewModel>(() => _skillGroupService.UpdateAsync(mappedItem));
        }

        [HttpDelete]
        public Task<IActionResult> DeleteAsync(Guid id)
        {
            return ProcessRequest<SkillGroupViewModel>(() => _skillGroupService.DeleteAsync(id));
        }
    }
}
