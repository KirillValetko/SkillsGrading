﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SkillsGrading.BusinessLogic.Responses;
using Npgsql;
using SkillsGrading.Web.Responses;

namespace SkillsGrading.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
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
            catch (NpgsqlException ex)
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
            catch (NpgsqlException ex)
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
    }
}
