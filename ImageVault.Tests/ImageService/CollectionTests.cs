namespace ImageVault.Tests.ImageService;

public class CollectionTests
{
    private readonly IServiceProvider _serviceProvider;

    public CollectionTests()
    {
        _serviceProvider = ImageServiceTestConfiguration.Instance.GetServiceProvider(); 
    }
     
}