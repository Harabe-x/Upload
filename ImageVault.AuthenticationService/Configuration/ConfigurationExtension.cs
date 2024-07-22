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
}