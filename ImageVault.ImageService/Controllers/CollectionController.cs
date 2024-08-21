using ImageVault.ImageService.Data.Dtos.Collection;
using ImageVault.ImageService.Data.Dtos.Image;
using ImageVault.ImageService.Data.Interfaces.Image;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.ImageService.Controllers;

[Controller]
[Route("/api/v1/collection")]
public class CollectionController : ControllerBase 
{
   private ILogger<ImageController> _logger; 
    
    private readonly IImageManagerRepository _imageManager;

    public CollectionController(IImageManagerRepository imageManager,ILogger<ImageController>  logger)
    {
        _imageManager = imageManager;
        _logger = logger; 
    }




    [HttpPost("/ListCollections")]
    public async Task<IActionResult> ListCollection([FromBody] string apiKey)
    {
        try
        {
            var result = await _imageManager.ListCollections(apiKey);
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
    
    [HttpPost("/CreateCollection")]
    public async Task<IActionResult>CreateCollection([FromBody] CreateCollectionDto collectionData)
    {
        try
        {
            var result = await _imageManager.CreateCollection(collectionData.ApiKey, collectionData.CollectionName, 
                collectionData.CollectionDescription);

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
    
    
    [HttpPatch("/EditCollection")]
    public async Task<IActionResult> EditCollection([FromBody] EditCollectionDto collectionData)
    {
        try
        {
            var result = await _imageManager.EditCollection(collectionData.ApiKey, collectionData.CollectionName,
                collectionData.CollectionName, collectionData.NewCollectionDescription);

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
    
    [HttpDelete("/DeleteCollection")]
    public async Task<IActionResult>DeleteCollection([FromBody] DeleteCollectionDto collectionData)
    {
         try
         {
             var result = await _imageManager.DeleteCollection(collectionData.ApiKey, collectionData.CollectionName);
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