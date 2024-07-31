using System.Security.Claims;
using ImageVault.ApiKeyService.Controllers;
using ImageVault.ApiKeyService.Data.Dtos.Request;
using ImageVault.ApiKeyService.Data.Interfaces.RabbitMq;
using ImageVault.ApiKeyService.Extension;

namespace ImageVault.ApiKeyService.Middleware;

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