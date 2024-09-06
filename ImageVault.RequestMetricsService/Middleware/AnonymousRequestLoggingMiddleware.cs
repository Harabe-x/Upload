using ImageVault.RequestMetricsService.Data.Dtos.Log;
using ImageVault.RequestMetricsService.Data.Interfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace ImageVault.RequestMetricsService.Middleware;

    /// <summary>
    ///  Middleware responsible for logging incoming requests 
    /// </summary>
public class AnonymousRequestLoggingMiddleware
{
        /// <summary>
        ///  RequestDelegate
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        ///  
        /// </summary>
        /// <param name="next"> RequestDelegate </param>
        public AnonymousRequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Adds anonymous request to the database
        /// </summary>
        /// <param name="context">Request data</param>
        /// <param name="anonymousRequestRepository">Repository that handles adding request to the database</param>
        public async Task InvokeAsync(HttpContext context, IAnonymousRequestRepository anonymousRequestRepository)
        {
            if (context.Request.Headers.Count == 0)
            {
                var request = CreateAnonymousRequest(context);
                await anonymousRequestRepository.AddAnonymousRequest(request);
            }

            await _next(context);
        }

        /// <summary>
        ///  Creates Request object to add to the database
        /// </summary>
        /// <param name="context">Request data</param>
        /// <returns></returns>
        private static AnonymousRequest CreateAnonymousRequest(HttpContext context)
        {
            return new AnonymousRequest(DateTime.Now, context.Request.Path, context.Connection.RemoteIpAddress.ToString(), context.Request.Method);
        }
}