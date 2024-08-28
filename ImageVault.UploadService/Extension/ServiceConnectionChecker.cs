using ImageVault.UploadService.Data;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;

namespace ImageVault.UploadService.Extension;

public static class ServiceConnectionChecker
{

    public static async Task<IApplicationBuilder> CheckServicesConnection(this IApplicationBuilder app)
    {
        var dbContext = app.ApplicationServices.GetService<ApplicationDbContext>();
        var s3Client = app.ApplicationServices.GetService<IAmazonS3Connection>();
        var logger = app.ApplicationServices.GetService<ILogger>();
        
       await CheckS3Connection(s3Client,logger);

       await CheckDatabaseConnection(dbContext,logger);

        return app; 
    }

    private static async Task CheckS3Connection(IAmazonS3Connection s3Connection,ILogger logger)
    {
        try
        {
            await s3Connection.S3Client.ListBucketsAsync();
            logger.LogInformation("Connection to s3 has been successfully established");
        }
        catch (Exception e)
        {
            logger.LogError("An error occurred while trying to connect to S3");            
            logger.LogError(e.ToString());
        }
    }

    private static async Task CheckDatabaseConnection(ApplicationDbContext dbContext,ILogger logger )
    {
        var connectionStatus = await dbContext.Database.CanConnectAsync();

        if (connectionStatus)
        {
            logger.LogInformation("The applications successfully connected to the database");    
        }
        else
        {
            logger.LogError("The application is not connected to the database");
        }
    }
}