using ImageVault.UploadService.Data.Interfaces.AmazonS3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.UploadService.Controllers;

[Route("/api/upload")]
[Controller]
public class UploadController : ControllerBase
{

    public IAmazonS3Connection _amazonS3Connection;

    public UploadController(IAmazonS3Connection amazonS3Connection )
    {
        _amazonS3Connection = amazonS3Connection;
    }
    
    [HttpGet("Test")]
    public async Task<IActionResult> TestController()
    {
        return Ok(await _amazonS3Connection.S3Client.ListBucketsAsync()); 
    }
}