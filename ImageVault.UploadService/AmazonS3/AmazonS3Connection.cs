using Amazon;
using Amazon.S3;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;

namespace ImageVault.UploadService.AmazonS3;


/// <summary>
///  <inheritdoc cref="IAmazonS3Connection"/>
/// </summary>
public class AmazonS3Connection : IAmazonS3Connection
{
    public  AmazonS3Client S3Client { get; private set; }

    public AmazonS3Connection()
    {
        InitializeConnection();
    }
    
    private void InitializeConnection()
    {
        S3Client= new AmazonS3Client(RegionEndpoint.EUNorth1);
    }
}