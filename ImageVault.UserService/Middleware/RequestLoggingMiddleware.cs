using System.Security.Claims;
using ImageVault.UserService.Data.Dtos.Request;
using ImageVault.UserService.Data.Interfaces;
using ImageVault.UserService.Extension;

namespace ImageVault.UserService.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration, IRabbitMqMessageSender sender)
    {
        var requestDtp = CreateRequestDto(context);

        sender.SendMessage(requestDtp, configuration.GetMetricsQueueName());

        await _next(context);
    }

    private RequestDto CreateRequestDto(HttpContext context)
    {
        return new RequestDto(context.User.GetClaimValue(ClaimTypes.NameIdentifier), DateTime.Now, context.Request.Path,
            context.Connection.RemoteIpAddress.ToString(), context.Request.Method);
    }
}