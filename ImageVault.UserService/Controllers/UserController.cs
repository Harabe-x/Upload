using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImageVault.UserService.Controllers;


[Controller]
[Route("/api/user")]
public class UserController : ControllerBase
{
        
    [HttpGet]
    public async Task<IActionResult> TestServer()
    {
        return Ok("Success");
    }
}