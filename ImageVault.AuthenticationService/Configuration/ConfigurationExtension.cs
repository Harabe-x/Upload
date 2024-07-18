namespace ImageVault.AuthenticationService.Configuration;

public static class ConfigurationExtension
{
    public static string? GetEndpointUrl(this IConfiguration configuration, string endpointName)
    {
        return configuration?.GetSection("Endpoints")[endpointName];
    }
}