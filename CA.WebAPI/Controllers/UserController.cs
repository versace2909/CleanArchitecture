using CA.Application.Interfaces;
using CA.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace CA.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(ILogger<UserController> logger, IUserService userService) : ControllerBase
{

    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> CreateUser([FromBody]UserCreatedModel userCreatedModel)
    {
        await userService.CreateUserAsync(userCreatedModel);
        return Ok("Ok");
    }
}