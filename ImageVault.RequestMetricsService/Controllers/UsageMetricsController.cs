using System.Security.Claims;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.RequestMetricsService.Controllers;

[Authorize]
[Controller]
public class UsageMetricsController : ControllerBase
{
    
    private readonly ILogger<UsageMetricsController> _logger;

    private readonly IUsageMetricsRepository _metricsRepository; 
    
    public UsageMetricsController(ILogger<UsageMetricsController> logger, IUsageMetricsRepository metricsRepository )
    {
        _logger = logger; 
        _metricsRepository = metricsRepository; 
    }

    [HttpPost]
    public async Task<IActionResult> GetUsageMetrics()
    {
        try
        {
            var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

            var result = await _metricsRepository.GetTotalUsageMetrics(id);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, "Internal server error, please try again later.");
        }
    }

    [HttpPost]
    public async Task<IActionResult> GetUsageStatisticsForLastDays([FromBody] int dayRange)
    {
        try 
        {
            var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

            var result = await _metricsRepository.GetDailyUsageMetrics(id,dayRange);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, "Internal server error, please try again later.");
        }
    }
    
    
}