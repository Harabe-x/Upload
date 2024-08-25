using Amazon.S3;

namespace ImageVault.UploadService.Data.Interfaces.AmazonS3;

/// <summary>
///   Service representing correctly configured  S3 client
/// </summary>
public interface IAmazonS3Connection
{
    AmazonS3Client S3Client { get; }
}