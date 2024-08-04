namespace ImageVault.UploadService.Extension;

public static class ConfigurationExtension
{
    public static string? GetS3BucketName(this IConfiguration configuration )
    {
        return configuration.GetSection("AmazonS3")["BucketName"];
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

}