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
    public class SkillController : BaseController
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService,
            IMapper mapper,
            ILogger<BaseController> logger) : base(mapper, logger)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public Task<IActionResult> GetAsync([FromQuery] PaginationRequest<SkillFilter> request)
        {
            return ProcessRequest<PaginationResponse<SkillModel>, PaginationResponse<SkillViewModel>>(() =>
                _skillService.GetPaginatedAsync(request));
        }

        [HttpPost]
        public Task<IActionResult> PostAsync(SkillDto item)
        {
            var mappedItem = _mapper.Map<SkillModel>(item);

            return ProcessRequest<SkillViewModel>(() => _skillService.CreateAsync(mappedItem));
        }

        [HttpPut]
        public Task<IActionResult> PutAsync(SkillDto item)
        {
            var mappedItem = _mapper.Map<SkillModel>(item);

            return ProcessRequest<SkillViewModel>(() => _skillService.UpdateAsync(mappedItem));
        }

        [HttpDelete]
        public Task<IActionResult> DeleteAsync(Guid id)
        {
            return ProcessRequest<SkillViewModel>(() => _skillService.DeleteAsync(id));
        }
    }
}
