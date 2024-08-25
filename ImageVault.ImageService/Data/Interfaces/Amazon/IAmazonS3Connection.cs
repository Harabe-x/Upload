using Amazon.S3;

namespace ImageVault.ImageService.Data.Interfaces.Amazon;

/// <summary>
///   Service representing correctly configured  S3 client
/// </summary>
public interface IAmazonS3Connection
{
    AmazonS3Client S3Client { get; }
}