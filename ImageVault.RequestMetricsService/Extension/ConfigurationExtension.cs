namespace ImageVault.RequestMetricsService.Extension;

public static class ConfigurationExtension
{
    public static IEnumerable<string>? GetImageUploadEndpoints(this IConfiguration configuration)
    {
        return configuration?.GetSection("ImageUploadEndpoints").Get<string[]>();
    }
    public static string? GetRabbitMqUsername(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Username"];
    }
    
    public static string? GetRabbitMqPassword(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Password"];
    }
    public static string? GetRabbitMqHostName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Host"];
    }
    public static string? GetRequestQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["RequestQueue"];
    }


}