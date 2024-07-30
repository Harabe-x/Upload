namespace ImageVault.AuthenticationService.Configuration;

public static class ConfigurationExtension
{
    public static string? GetEndpointUrl(this IConfiguration configuration, string endpointName)
    {
        return configuration?.GetSection("Endpoints")[endpointName];
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

    public static string? GetUserQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["UserDataQueue"];
    }

    public static string? GetMetricsQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["MetricsQueue"];
    }
}