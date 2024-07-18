using System.Security.Claims;
using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Interfaces;
using ImageVault.RequestMetricsService.Extension;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Identity.Client;

namespace ImageVault.RequestMetricsService.Middleware;

public class RequestMiddleware
{
    private readonly RequestDelegate _next;

    private readonly ILogger<RequestMiddleware> _logger; 
    
    public RequestMiddleware(RequestDelegate next, ILogger<RequestMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context,IRequestRepository requestRepository, IUserRequestMetricsRepository userRequestMetricsRepository)
    {

        var request = new RequestDto(context.User.GetClaimValue(ClaimTypes.NameIdentifier),DateTime.Now,context.Request.GetDisplayUrl(),context.Connection.RemoteIpAddress?.ToString(), context.Request.Method);
        
        _logger.LogInformation($"[ {request.ToString()} ]  ");

        try
        { 
            var addingRequestStatus  = await requestRepository.AddRequest(request);
            var userMetricsUpdateStaus = await userRequestMetricsRepository.UpdateUserStatstics(request);
             
            if ( ! addingRequestStatus || ! userMetricsUpdateStaus) _logger.LogCritical("Request was not added to the database");
        }
        catch (Exception e)
        {
            _logger.LogError($"{e.Message} {e.Source} ");    
        }
        
        await _next(context);
    }
}