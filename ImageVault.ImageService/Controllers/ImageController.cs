using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Interfaces.Image;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.ImageService.Controllers;

[Controller] 
[Route("/api/v1/image/")]
public class ImageController : ControllerBase
{
    private ILogger<ImageController> _logger; 
    
    private readonly IImageManagerRepository _imageManager;

    public ImageController(IImageManagerRepository imageManager,ILogger<ImageController>  logger)
    {
        _imageManager = imageManager;
        _logger = logger; 
    }
    
    
    [HttpPost("/GetImage")]
    public async Task<IActionResult>GetImage([FromBody] GetImageDto imageData) 
    {
        try
        {
            var result = await _imageManager.GetImage(imageData.ApiKey, imageData.ImageKey, imageData.CollectionName);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, "Server error, we will try to fix it as soon as possible\n ");
        }
    }
    
    [HttpPost("/GetImages")]
    public async Task<IActionResult>GetImages([FromBody] GetImagesDto imageData)
    {
        try
        {
            var result = await _imageManager.GetImages(imageData.ApiKey,imageData.CollectionName);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, "Server error, we will try to fix it as soon as possible\n ");
        }
    }
    
    
    [HttpPatch("/EditImage")]
    public async Task<IActionResult> EditImage([FromBody] EditImageDto imageData)
    {
        try
        {
            var result = await _imageManager.EditImage(imageData.ApiKey, imageData.ImageKey,imageData.NewTitle, imageData.NewDescription);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, "Server error, we will try to fix it as soon as possible\n ");
        }
    }
    
    [HttpDelete("/DeleteImage")]
    public async Task<IActionResult>DeleteImage([FromBody] DeleteImageDto imageData)
    {
         try
        {
            var result = await _imageManager.DeleteImage(imageData.ApiKey,imageData.ImageKey,imageData.ImageKey);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            return StatusCode(500, "Server error, we will try to fix it as soon as possible\n ");
        }
    }

    
}