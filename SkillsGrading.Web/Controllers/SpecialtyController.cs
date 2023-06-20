using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.BusinessLogic.Services.Interfaces;
using SkillsGrading.Common.Models;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.Web.Models.ViewModels;

namespace SkillsGrading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : BaseController
    {
        private readonly ISpecialtyService _specialtyService;

        public SpecialtyController(ISpecialtyService specialtyService,
            IMapper mapper,
            ILogger<BaseController> logger) : base(mapper, logger)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet]
        public Task<IActionResult> GetAsync([FromQuery] PaginationRequest<SpecialtyFilter> request)
        {
            return ProcessRequest<PaginationResponse<SpecialtyModel>, PaginationResponse<SpecialtyViewModel>>(() =>
                _specialtyService.GetPaginatedAsync(request));
        }
    }
}
