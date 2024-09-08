using ImageVault.RequestMetricsService.Data;
using ImageVault.RequestMetricsService.Data.Dtos.Log;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ImageVault.RequestMetricsService.Controllers;

[Controller]
[Route("/api/v1/logs/apikey/get")]
public class ApiKeyLogsController : ControllerBase
{

    private readonly ILogger<ApiKeyLogsController> _logger;

    private readonly IApiKeyLogsRepository _apiKeyLogsRepository;

    public ApiKeyLogsController(ILogger<ApiKeyLogsController> logger, IApiKeyLogsRepository apiKeyLogsRepository)
    {
        _logger = logger;
        _apiKeyLogsRepository = apiKeyLogsRepository;
    }


    [HttpPost]
    public async Task<IActionResult> GetApiKeyLogs([FromBody] GetApiKeyLog apiKeyLogData)
    {
        try
        {
            var result =
                await _apiKeyLogsRepository.GetLogs(apiKeyLogData.ApiKey, apiKeyLogData.Limit, apiKeyLogData.Page);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error); 
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, new Error("Internal server error."));
        }
    }
    
}