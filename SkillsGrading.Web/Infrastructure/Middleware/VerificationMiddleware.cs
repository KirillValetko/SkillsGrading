using System.Text.Json;
using SkillsGrading.BusinessLogic.Services.Interfaces;
using SkillsGrading.Common.Constants;
using SkillsGrading.BusinessLogic.Responses;

namespace SkillsGrading.Web.Infrastructure.Middleware
{
    public class VerificationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenService _tokenService;

        public VerificationMiddleware(RequestDelegate next,
            IHttpClientFactory httpClientFactory,
            ITokenService tokenService)
        {
            _next = next;
            _httpClientFactory = httpClientFactory;
            _tokenService = tokenService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Headers.Authorization
                .FirstOrDefault(h => h.Contains(AuthorizationConstants.Bearer));

            if (string.IsNullOrEmpty(token))
            {
                throw new BadHttpRequestException(ExceptionMessageConstants.NotAuthorized, 401);
            }

            var request = new HttpRequestMessage(HttpMethod.Get,
                RequestConstants.VerificationPath);
            request.Headers.Add(AuthorizationConstants.Authorization, token);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                await using var responseStream = await response.Content.ReadAsStreamAsync();
                var apiResponse = await JsonSerializer.DeserializeAsync<ApiResponse<string>>(responseStream);
                _tokenService.SetToken(token);
            }
            else
            {
                throw new BadHttpRequestException(ExceptionMessageConstants.NotAuthorized, 403);
            }

            await _next(context);
        }
    }
}
