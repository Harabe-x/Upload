using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.Server.Controllers;


[Route("api/collections")]
[ApiController]
public class CollectionController : ControllerBase 
{
    
    public CollectionController(IImageCollectionRepository imageCollectionRepository)
    {
        _imageCollectionRepository = imageCollectionRepository;
    }

    [HttpGet]
    public IActionResult GetImageCollection(string collectionName)
    {
        return Ok(new ImageCollection());
    }

    private readonly IImageCollectionRepository _imageCollectionRepository; 
}