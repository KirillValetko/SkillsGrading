using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SkillsGrading.BusinessLogic.Models;
using SkillsGrading.BusinessLogic.Services.Interfaces;
using SkillsGrading.DataAccess.Filters;
using SkillsGrading.Web.Models.DtoModels;
using SkillsGrading.Web.Models.ViewModels;

namespace SkillsGrading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IMapper mapper,
            ILogger<BaseController> logger,
            IEmployeeService employeeService) : base(mapper, logger)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public Task<IActionResult> GetAsync(Guid id)
        {
            return ProcessRequest<EmployeeModel, EmployeeViewModel>(() => _employeeService.GetByFilterAsync(
                new EmployeeFilter { Id = id }));
        }

        [HttpPut]
        public Task<IActionResult> PutAsync(EmployeeDto item)
        {
            var mappedItem = _mapper.Map<EmployeeModel>(item);

            return ProcessRequest<EmployeeModel>(() => _employeeService.UpdateAsync(mappedItem));
        }

        [HttpPost]
        public Task<IActionResult> PostAsync(GradingDto item)
        {
            var mappedItem = _mapper.Map<GradingModel>(item);

            return ProcessRequest<EmployeeModel>(() => _employeeService.GradeAsync(mappedItem));
        }
    }
}
