using ImageVault.ImageService.Data.Interfaces.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.ImageService.Controllers;

[Controller] 
[Route("/api/image/")]
public class ImageController : ControllerBase
{

    private readonly IImageManagerRepository _imageManager;

    public ImageController(IImageManagerRepository imageManager)
    {
        _imageManager = imageManager; 
    }
    
    
    [HttpGet("get")]
    public async Task<IActionResult> GetImage(string apiKey, string collectionName,int pageNumber, int limit)
    {
        return Ok(await _imageManager.GetPagedImages(apiKey,pageNumber,limit,collectionName)); 
    }
}