namespace ImageVault.UploadService.Extension;

public static class ConfigurationExtension
{
    public static string GetS3BucketName(this IConfiguration configuration)
    {
        return configuration.GetSection("AmazonS3")["BucketName"] ?? throw new InvalidOperationException("BucketName is not configured in AmazonS3 section.");
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

    public static string? GetRabbitMqJwtTokenQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["UploadServiceJwtQueue"];
    }

    public static string? GetApiKeyServiceEndpoint(this IConfiguration configuration)
    {
        return configuration.GetSection("Endpoints").GetSection("ApiKeyService")["GetApiKeyEndpoint"];
    }

    public static string? GetApiKeyUsageQueue(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ApiKeyUsage"];
    }

    public static IEnumerable<string> GetAllowedFileExtensions(this IConfiguration configuration)
    {
        return configuration.GetSection("AppConfig").GetValue<IEnumerable<string>>("AllowedExtenions");
    }

}