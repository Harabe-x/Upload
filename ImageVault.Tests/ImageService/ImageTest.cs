using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace ImageVault.Tests.ImageService;

public class ImageTest
{

    private readonly IServiceProvider _serviceProvider;
    
    
    public ImageTest()
    {
        _serviceProvider = ImageServiceTestConfiguration.Instance.GetServiceProvider(); 
    }
    
    
}