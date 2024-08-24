namespace ImageVault.UserService.Extension;

/// <summary>
/// a class that helps you quickly read specific values from the configuration using <see cref="IConfiguration"/>
/// </summary>
public static class ConfigurationExtension
{
    /// <summary>
    ///  Reads RabbitMQ username from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Username</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string? GetRabbitMqUsername(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Username"]  ?? throw new NullReferenceException();
    }

    /// <summary>
    ///  Reads RabbitMQ Password from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Password</returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>
    public static string? GetRabbitMqPassword(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Password"] ?? throw new NullReferenceException();
    }

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
    ///  Reads RabbitMQ Metrics Service queue name from configuration
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Metrics queue </returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>
    public static string? GetMetricsQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["MetricsQueue"] ?? throw new NullReferenceException(); 
    }
}