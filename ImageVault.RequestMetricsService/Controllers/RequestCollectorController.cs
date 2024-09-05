using System.Security.Claims;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ImageVault.RequestMetricsService.Controllers;

[Authorize]
[Controller]
[Route("/api/v1/requestCollector/collect")]
public class RequestCollectorController : ControllerBase
{
    private readonly ILogger<RequestCollectorController> _logger; 
    
    private readonly IDailyUsageMetricsRepository _dailyMetricsRepository; 
    
    public RequestCollectorController(IDailyUsageMetricsRepository dailyMetricsRepository, ILogger<RequestCollectorController> logger)
    {
        _dailyMetricsRepository = dailyMetricsRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> CollectGetRequest()
    {
        await CollectRequest(); 
        return NoContent();
    }
    
    [HttpPost]
    public async Task<IActionResult> CollectPostRequest()
    {
        await CollectRequest(); 
        return NoContent();
    }
    
    [HttpPatch]
    public async Task<IActionResult> CollectPatchRequest()
    {
        await CollectRequest(); 
        return NoContent();
    }
    
    [HttpDelete]
    public async Task<IActionResult> CollectDeleteRequest()
    {
        await CollectRequest();  
        return NoContent();
    }
    
    private async Task CollectRequest()
    {
        try
        {
            var request = CreateRequestDto();
            _logger.LogInformation(request.ToString());
            await _dailyMetricsRepository.AddRequest(request);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString()); 
        }
    }
    
    /// <summary>
    ///  Creates Request object to send via AMQP
    /// </summary>
    /// <param name="HttpContext">Request data</param>
    /// <returns></returns>
    private Request CreateRequestDto()
    {
        var id  = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return new Request(id,
            DateTime.Now, HttpContext.Request.Path, HttpContext.Connection.RemoteIpAddress.ToString(), HttpContext.Request.Method);
    }
}