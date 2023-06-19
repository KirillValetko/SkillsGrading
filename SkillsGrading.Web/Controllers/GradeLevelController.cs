using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.BusinessLogic.Services.Interfaces;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.Web.Models.ViewModels;

namespace SkillsGrading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeLevelController : BaseController
    {
        private readonly IGradeLevelService _gradeLevelService;

        public GradeLevelController(IGradeLevelService gradeLevelService,
            IMapper mapper,
            ILogger<BaseController> logger) : base(mapper, logger)
        {
            _gradeLevelService = gradeLevelService;
        }

        [HttpGet]
        public Task<IActionResult> GetAsync(Guid specialtyId)
        {
            return ProcessRequest<List<GradeLevelModel>, List<GradeLevelViewModel>>(() =>
                _gradeLevelService.GetAllByFilterAsync(new GradeLevelFilter { SpecialtyId = specialtyId }));
        }
    }
}
