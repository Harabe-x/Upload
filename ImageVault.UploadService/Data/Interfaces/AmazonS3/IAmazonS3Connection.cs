using Amazon.S3;

namespace ImageVault.UploadService.Data.Interfaces.AmazonS3;

public interface IAmazonS3Connection
{
    AmazonS3Client S3Client { get; }
}