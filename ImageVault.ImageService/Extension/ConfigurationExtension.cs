namespace ImageVault.ImageService.Extension;

public static class ConfigurationExtension
{
    
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

    public static string? GetImageQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ImageQueue"];
    }

    public static string? GetApiKeyQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ApiQueue"];
    }

    public static string GetS3BucketName(this IConfiguration configuration)
    {
        return configuration.GetSection("Amazon")["BucketName"];  
    }

}