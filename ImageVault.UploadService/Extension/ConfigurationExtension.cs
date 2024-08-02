namespace ImageVault.UploadService.Extension;

public static class ConfigurationExtension
{
    public static string? GetS3BucketName(this IConfiguration configuration )
    {
        return configuration.GetSection("AmazonS3")["BucketName"];
    }
}