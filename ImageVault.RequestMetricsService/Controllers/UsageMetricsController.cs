using System.Security.Claims;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Data.Models;
using ImageVault.RequestMetricsService.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.RequestMetricsService.Controllers;

[Authorize]
[Route("/api/v1/metrics")]
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

    [HttpPost("get")]
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
            return StatusCode(500, new Error("Internal server error."));
        }
    }

    [HttpPost("daily/get")]
    public async Task<IActionResult> GetUsageStatisticsForLastDays([FromBody] GetDailyMetrics data)
    {
        try 
        {
            var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

            var result = await _metricsRepository.GetDailyUsageMetrics(id,data.DayRange);

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