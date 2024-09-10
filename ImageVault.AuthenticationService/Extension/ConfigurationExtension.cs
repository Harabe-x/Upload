
namespace ImageVault.AuthenticationService.Configuration;


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
    ///  Reads RabbitMQ User Service queue name  
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq  </returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>
    public static string? GetUserQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["UserDataQueue"] ?? throw new NullReferenceException();
    }

    /// <summary>
    ///  Reads RabbitMQ Metrics Service queue name from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Metrics queue </returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>
    public static string? GetMetricsQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["MetricsQueue"] ?? throw new NullReferenceException(); 
    }

    /// <summary>
    ///  Reads RabbitMQ Repository Service JWT queue name from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string? GetUploadServiceJwtQueue(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["UploadServiceJwtQueue"] ?? throw new NullReferenceException();
    }
}