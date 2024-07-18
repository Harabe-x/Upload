using System.Security.Claims;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.RequestMetricsService.Controllers;

[Controller]
[Route("/api/user/metrics")]
public class UserRequestMetricsController : ControllerBase
{
    private readonly ILogger<UserRequestMetricsController> _logger;  
    
    private readonly IUserRequestMetricsRepository _metricsRepository; 

    public UserRequestMetricsController(IUserRequestMetricsRepository metricsRepository, ILogger<UserRequestMetricsController> logger)
    {
        _metricsRepository = metricsRepository;
        _logger = logger; 
    }
    
    
    [HttpGet]
    [Authorize]
    [Route("/get")]
    public async Task<IActionResult> GetUserMetrics()
    {
        var id = User.GetClaimValue(ClaimTypes.NameIdentifier);

        try
        {
            var metrics = await _metricsRepository.GetRequestMetrics(id);

            return metrics != null ? Ok(metrics) : StatusCode(500, "Something went wrong"); 
        }
        catch (Exception e)
        {
            _logger.LogError($" {e.Message} | {e.Source}"); 
            // returning string as error message is temporary 
            return StatusCode(500, "Internal server error");
        }

    }
    
}