using Microsoft.AspNetCore.Mvc;

namespace SoftwareMarket.App.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class AuthController: ControllerBase
{
    [HttpGet]
    [Route("login")]
    public IActionResult Login(string userName, string password)
    {
        Response.Cookies.Append("username",userName);
        Response.Cookies.Append("userpassword", password);
        return StatusCode(StatusCodes.Status200OK);
    }
}