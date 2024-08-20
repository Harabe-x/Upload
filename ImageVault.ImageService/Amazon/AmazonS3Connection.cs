using Amazon;
using Amazon.S3;
using ImageVault.ImageService.Data.Interfaces.Amazon;

namespace ImageVault.ImageService.Amazon;

public class AmazonS3Connection : IAmazonS3Connection
{
    public AmazonS3Connection()
    {
        InitializeConnection();
    }

    public AmazonS3Client S3Client { get; private set; }

    private void InitializeConnection()
    {
        S3Client = new AmazonS3Client(RegionEndpoint.EUNorth1);
    }    
}