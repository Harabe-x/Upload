using ImageVault.UploadService.Data.Dtos.Upload;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.Upload;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.UploadService.Controllers;

[Route("/api/upload")]
[Controller]
public class UploadController : ControllerBase
{

    private readonly IImageUploadRepository _uploadRepository;

    public UploadController(IImageUploadRepository uploadRepository )
    {
        _uploadRepository = uploadRepository;
    }
    
    [HttpPost("Test")]
    public async Task<IActionResult> TestController(IFormFile file)
    {

        var operation = await _uploadRepository.UploadImage(new ImageUpload(file, "", "", "", false));
        
        return Ok(operation.IsSuccess); 
    }
}