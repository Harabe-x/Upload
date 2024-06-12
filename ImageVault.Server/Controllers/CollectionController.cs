using ImageVault.Server.Data;
using ImageVault.Server.Data.Interfaces;
using ImageVault.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.Server.Controllers;


[Route("api/collections")]
[ApiController]
public class CollectionController : ControllerBase 
{
    
    public CollectionController(IImageCollectionRepository imageCollectionRepository,ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext; 
        _imageCollectionRepository = imageCollectionRepository;
    }

    [HttpGet]
    public IActionResult GetImageCollection(string collectionName)
    {
        return Ok(new ImageCollection());
    }

    [HttpPost("Add")]
    public IActionResult InsertImage([FromBody] Image image)
    {


        _applicationDbContext.Images.Add(image);
        _applicationDbContext.SaveChanges();

        return Ok("Success");
    }


    private readonly ApplicationDbContext _applicationDbContext;
    
    private readonly IImageCollectionRepository _imageCollectionRepository; 
}