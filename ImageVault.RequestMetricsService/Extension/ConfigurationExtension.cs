namespace ImageVault.RequestMetricsService.Extension;

public static class ConfigurationExtension
{
    public static IEnumerable<string>? GetImageUploadEndpoints(this IConfiguration configuration)
    {
        return configuration?.GetSection("ImageUploadEndpoints").Get<string[]>();
    }
}