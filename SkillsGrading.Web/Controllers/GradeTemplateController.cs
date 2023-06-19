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
    public class GradeTemplateController : BaseController
    {
        private readonly IGradeTemplateService _gradeTemplateService;

        public GradeTemplateController(IMapper mapper,
            ILogger<BaseController> logger,
            IGradeTemplateService gradeTemplateService) : base(mapper, logger)
        {
            _gradeTemplateService = gradeTemplateService;
        }

        [HttpGet]
        public Task<IActionResult> GetAsync([FromQuery] PaginationRequest<GradeTemplateFilter> request)
        {
            return ProcessRequest<PaginationResponse<GradeTemplateModel>, PaginationResponse<GradeTemplateViewModel>>(() =>
                _gradeTemplateService.GetPaginatedAsync(request));
        }

        [HttpGet("{id}")]
        public Task<IActionResult> GetAsync(Guid id)
        {
            return ProcessRequest<GradeTemplateModel, GradeTemplateViewModel>(() =>
                _gradeTemplateService.GetByFilterAsync(new GradeTemplateFilter { Id = id }));
        }

        [HttpPost]
        public Task<IActionResult> PostAsync(GradeTemplateDto item)
        {
            var mappedItem = _mapper.Map<GradeTemplateModel>(item);

            return ProcessRequest<GradeTemplateModel>(() => _gradeTemplateService.CreateAsync(mappedItem));
        }

        [HttpPut]
        public Task<IActionResult> PutAsync(GradeTemplateDto item)
        {
            var mappedItem = _mapper.Map<GradeTemplateModel>(item);

            return ProcessRequest<GradeTemplateModel>(() => _gradeTemplateService.UpdateAsync(mappedItem));
        }

        [HttpDelete]
        public Task<IActionResult> DeleteAsync(Guid id)
        {
            return ProcessRequest<GradeTemplateModel>(() => _gradeTemplateService.DeleteAsync(id));
        }
    }
}
