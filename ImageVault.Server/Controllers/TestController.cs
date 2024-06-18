using ImageVault.ClassLibrary.Validation.Classes;
using ImageVault.ClassLibrary.Validation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.Server.Controllers;

[Route("/api/test")]
[Controller]
public class TestController : ControllerBase
{
    public TestController(IDataValidator dataValidator)
    {
        _dataValidator = dataValidator;
    }

    [HttpPost("validate")]
    public async Task<IActionResult> TestEndpoint([FromHeader] string data)
    {
        var result = _dataValidator.ValidateData("ValidateName", data);
        
        return Ok(result);
    }


    private readonly IDataValidator _dataValidator;
}