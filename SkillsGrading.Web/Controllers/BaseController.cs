using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SkillsGrading.Common.Models;
using SkillsGrading.Web.Responses;

namespace SkillsGrading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly ILogger<BaseController> _logger;

        protected BaseController(IMapper mapper,
            ILogger<BaseController> logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        protected async Task<IActionResult> ProcessRequest<TModel, TViewModel>(Func<Task<TModel>> func)
        {
            try
            {
                var result = await func();
                var mappedResult = _mapper.Map<TViewModel>(result);

                return Ok(new ApiResponse<TViewModel>(mappedResult));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
        }

        protected async Task<IActionResult> ProcessRequest<TViewModel>(Func<Task> func)
        {
            try
            {
                await func();

                return Ok(new ApiResponse<TViewModel>(default(TViewModel)));
            }
            catch (SqlException ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex.Message);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
        }

        protected IActionResult ProcessRequest<TViewModel>(Func<TViewModel> func)
        {
            try
            {
                var list = func();

                return Ok(new ApiResponse<TViewModel>(list));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                Console.WriteLine(ex);

                return BadRequest(new ApiResponse<TViewModel>(ex.Message));
            }
        }
    }
}
