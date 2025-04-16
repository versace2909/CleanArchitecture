using CA.Infrastructure.EmailService.Models;

namespace CA.Infrastructure.EmailService.Interfaces;

public interface ISendMailService
{
    Task<bool> SendEmailAsync(SendEmailModel model);
}