using CA.Infrastructure.EmailService.Models;

namespace CA.Infrastructure.EmailService.Interfaces;

public interface IEmailTemplateService
{
    Task<string> RenderTemplateAsync<T>(T model) where T : BaseEmailModel;
}