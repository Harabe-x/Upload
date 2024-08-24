using System.Security.Claims;
using ImageVault.UserService.Data.Dtos.Request;
using ImageVault.UserService.Data.Interfaces;
using ImageVault.UserService.Extension;

namespace ImageVault.UserService.Middleware;



/// <summary>
///  Middleware responsible for logging incoming requests to User Service
/// </summary>
public class RequestLoggingMiddleware
{
    
    /// <summary>
    ///  RequestDelegate
    /// </summary>
    private readonly RequestDelegate _next;

    /// <summary>
    ///  
    /// </summary>
    /// <param name="next"> RequestDelegate </param>
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// Sends message via AMQP to Metrics Service
    /// </summary>
    /// <param name="context">Request data</param>
    /// <param name="configuration">Object that allows read configuration</param>
    /// <param name="sender"> Object for sending messages via AMQP</param>
    public async Task InvokeAsync(HttpContext context, IConfiguration configuration, IRabbitMqMessageSender sender)
    {
        var requestDtp = CreateRequestDto(context);

        sender.SendMessage(requestDtp, configuration.GetMetricsQueueName());

        await _next(context);
    }

    /// <summary>
    ///  Creates Request object to send via AMQP
    /// </summary>
    /// <param name="context">Request data</param>
    /// <returns></returns>
    private static Request CreateRequestDto(HttpContext context)
    {
        return new Request(context.User.GetClaimValue(ClaimTypes.NameIdentifier) ?? "User not authenticated",
            DateTime.Now, context.Request.Path, context.Connection.RemoteIpAddress.ToString(), context.Request.Method);
    }
}
