using Amazon.S3;

namespace ImageVault.ImageService.Data.Interfaces.Amazon;

public interface IAmazonS3Connection
{
    public AmazonS3Client S3Client { get; }
}