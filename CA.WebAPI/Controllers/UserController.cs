using CA.Application.Interfaces;
using CA.Application.Models;
using CA.Infrastructure.EmailService.Interfaces;
using CA.Infrastructure.EmailService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CA.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(
    ILogger<UserController> logger,
    IUserService userService,
    IEmailTemplateService emailTemplateService,
    ISendMailService sendMailService) : ControllerBase
{
    [HttpPost(Name = "CreateUser")]
    public async Task<ActionResult> CreateUser([FromBody] UserCreatedModel userCreatedModel)
    {
        await userService.CreateUserAsync(userCreatedModel);
        return Ok("Ok");
    }

    [HttpGet(Name = "GetUser")]
    public async Task<ActionResult> GetUUser()
    {
        var emailModel = new PasswordResetEmailModel()
        {
            UserName = "Test",
            ResetUrl = "https:/xxxx.abc/reset&token=123123123",
            ExpiresInMinutes = 30
        };
        var emailContent = await emailTemplateService.RenderTemplateAsync(emailModel);

        await sendMailService.SendEmailAsync(new SendEmailModel()
        {
            Title = emailModel.Title,
            EmailContent = emailContent,
            RecipientEmails = new List<string>() {"dungvv@hblab.vn"}
        });
        return Ok("Ok");
    }
}