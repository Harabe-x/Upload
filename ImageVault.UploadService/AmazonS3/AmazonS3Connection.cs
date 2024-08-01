using Amazon;
using Amazon.S3;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;

namespace ImageVault.UploadService.AmazonS3;

public class AmazonS3Connection : IAmazonS3Connection
{
    private AmazonS3Client _s3Client;

    public AmazonS3Client S3Client => _s3Client;

    
    public AmazonS3Connection()
    {
        InitializeConnection();
    }

    private void InitializeConnection()
    {
        _s3Client = new AmazonS3Client(RegionEndpoint.EUNorth1); 
    }
}