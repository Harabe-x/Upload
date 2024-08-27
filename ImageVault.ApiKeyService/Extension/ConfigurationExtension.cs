namespace ImageVault.ApiKeyService.Extension;

/// <summary>
/// a class that helps you quickly read specific values from the configuration
/// </summary>
public static class ConfigurationExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static string? GetRabbitMqHostName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq")["Host"];
    }
    

    
    /// <summary>
    ///  Reads RabbitMQ Metrics Service request queue name  
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Metrics Service queue name  </returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>
    public static string? GetMetricsQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["MetricsQueue"] ?? throw new NullReferenceException();
    }
    
    
    /// <summary>
    ///  Reads RabbitMQ Key Service usage queue name  
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Key Service usage queue name  </returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>

    public static string? GetApiKeyUsageQueue(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ApiKeyUsageQueue"] ?? throw new NullReferenceException();
    }

    /// <summary>
    ///  Reads RabbitMQ Image Service API key queue name  
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns> RabbitMq Image Service API key queue name  </returns>
    /// <exception cref="NullReferenceException">Occurs when IConfiguration was unable to read the specified value</exception>
    public static string? GetApiKeyQueueName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Queues")["ApiQueue"] ?? throw new NullReferenceException();
    }

    
    /// <summary>
    ///  Reads API key exchange name 
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns>API key exchange name</returns>
    /// <exception cref="NullReferenceException"></exception>
    public static string GetApiKeyExchangeName(this IConfiguration configuration)
    {
        return configuration.GetSection("RabbitMq").GetSection("Exchanges")["ApiKeyExchange"] ?? throw new NullReferenceException(); 
    }
    
    
}
