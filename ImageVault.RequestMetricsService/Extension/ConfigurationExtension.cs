namespace ImageVault.RequestMetricsService.Extension;

public static class ConfigurationExtension
{
    public static string? GetRabbitMqHostName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Host"];
    }

    public static string? GetApiKeyLogQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ApiKeyLogQueue"];
    }
    
    public static string? GetApiKeyResourceUsageQueue(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ApiKeyResourceUsageQueue"];
    }
    
}