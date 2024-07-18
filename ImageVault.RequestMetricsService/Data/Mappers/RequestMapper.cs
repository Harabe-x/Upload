using ImageVault.RequestMetricsService.Data.Dtos;
using ImageVault.RequestMetricsService.Data.Models;

namespace ImageVault.RequestMetricsService.Data.Mappers;

public static class RequestMapper
{
    public static Requests MapRequestDtoToRequest(this RequestDto request)
    {
        return new Requests()
        {
            UserId = request.UserId, 
            Endpoint = request.Endpoint, 
            Method = request.Method,
            Ip =  request.Ip,
            TimeStamp = request.TimeStamp 
        };
    }
}