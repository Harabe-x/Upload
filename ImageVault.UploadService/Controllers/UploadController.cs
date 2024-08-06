using System.Runtime.InteropServices.JavaScript;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using ImageVault.UploadService.Data.Dtos.Upload;
using ImageVault.UploadService.Data.Interfaces.AmazonS3;
using ImageVault.UploadService.Data.Interfaces.Upload;
using ImageVault.UploadService.Data.Models;
using ImageVault.UploadService.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.UploadService.Controllers;

[Route("/api/upload")]
[Controller]
[AllowAnonymous]
public class UploadController : ControllerBase
{
    private readonly IImageUploadRepository _uploadRepository;

    private readonly ILogger<UploadController> _logger;
    
    public UploadController(IImageUploadRepository uploadRepository, ILogger<UploadController> logger)
    {
        _uploadRepository = uploadRepository;
        _logger = logger;
    }
    
    [HttpPost("Upload")]
    public async Task<IActionResult> TestController(ImageUploadData imageUploadData)
    {
        try
        {
            var result = await _uploadRepository.UploadImage(imageUploadData);
            
            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        catch (Exception e)
        {
            _logger.LogError($"Unhandled error occured | Message {e.Message} | Source : {e.Source} StackTrace : {e.StackTrace}"  );
            return StatusCode(500, new Error("An unexpected error occurred. Please try again later.") );
        }
    }
}