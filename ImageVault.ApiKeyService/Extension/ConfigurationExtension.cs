namespace ImageVault.ApiKeyService.Extension;

public static class ConfigurationExtension
{
    public static string? GetRabbitMqHostName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Host"];
    }

    public static string? GetRabbitMqUsername(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Username"];
    }

    public static string? GetRabbitMqPassword(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Password"];
    }

    public static string? GetMetricsQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["MetricsQueue"];
    }
}