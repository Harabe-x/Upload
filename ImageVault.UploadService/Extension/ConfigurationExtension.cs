namespace ImageVault.UploadService.Extension;


/// <summary>
/// a class that helps you quickly read specific values from the configuration using <see cref="IConfiguration"/>
/// </summary
public static class ConfigurationExtension
{
    /// <summary>
    ///  Reads S3 Bucket name  from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns>S3 bucket name </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static string GetS3BucketName(this IConfiguration configuration)
    {
        return configuration.GetSection("AmazonS3")["BucketName"] ??
               throw new InvalidOperationException("BucketName is not configured in Amazon section.");
    }


    /// <summary>
    ///  Reads RabbitMQ Host from configuration 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Host</returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>
    public static string GetRabbitMqHostName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Host"] ?? throw new NullReferenceException();
    }

    /// <summary>
    ///  Reads RabbitMQ Repository Service JWT queue name from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string GetRabbitMqJwtTokenQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["UploadServiceJwtQueue"] ??
               throw new NullReferenceException();
    }

    /// <summary>
    ///  Reads Apikey service API key usage queue name 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> API key usage queue name</returns>
    public static string GetApiKeyUsageQueue(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ApiKeyUsageQueue"] ??
               throw new NullReferenceException();
    }

    /// <summary>
    ///  Reads allowed file extensions 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns>Allowed file extensions</returns>
    public static IEnumerable<string> GetAllowedFileExtensions(this IConfiguration configuration)
    {
        return configuration.GetSection("AppConfig").GetSection("AllowedExtensions").Get<string[]>() ??
               throw new NullReferenceException();
    }

    /// <summary>
    ///  Reads ImageData queue   
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static string GetImageQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ImageQueue"] ??
               throw new NullReferenceException();
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

    /// <summary>
    ///  Reads RabbitMQ Image Service API key queue name  
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Image Service API key queue name  </returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>
    public static string? GetApiKeyQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ApiQueue"] ??
               throw new NullReferenceException();
    }
     
} 