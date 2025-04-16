namespace CA.Infrastructure.EmailService.Models;

public class SendEmailModel
{
    public string Title { get; set; } = string.Empty;
    public string EmailContent { get; set; } = string.Empty;
    public List<string> RecipientEmails { get; set; } = [];
    public List<string> CCs { get; set; } = [];
}