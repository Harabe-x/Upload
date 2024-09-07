namespace ImageVault.ImageService.Extension;

/// <summary>
/// a class that helps you quickly read specific values from the configuration using <see cref="IConfiguration"/>
/// </summary>
public static class ConfigurationExtension
{

    /// <summary>
    ///  Reads RabbitMQ Host from configuration 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Host</returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>
    public static string? GetRabbitMqHostName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Host"] ?? throw new NullReferenceException();
    }

    /// <summary>
    ///  Reads Image Queue name
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns>Image queue name</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string? GetImageQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ImageQueue"] ?? throw new NullReferenceException();
        ;
    }
    
    /// <summary>
    ///  Reads API queue name
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> API queue name</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string? GetApiKeyQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ApiQueue"] ?? throw new NullReferenceException();
    }
   
    /// <summary>
    ///  Reads S3 Bucket name  from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns>S3 bucket name </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string GetS3BucketName(this IConfiguration configuration)
    {
        return configuration.GetSection("AmazonS3")["BucketName"] ?? throw new NullReferenceException("BucketName is not configured in Amazon section.");
    }
    
    /// <summary>
    ///  Reads API key exchange name 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns>API key exchange name</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string GetApiKeyExchangeName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Exchanges")["ApiKeyExchange"] ??
               throw new NullReferenceException();
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