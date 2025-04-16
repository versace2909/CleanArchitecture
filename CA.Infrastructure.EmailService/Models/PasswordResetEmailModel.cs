namespace CA.Infrastructure.EmailService.Models;

public class PasswordResetEmailModel : BaseEmailModel
{
    public override string TemplateName => "password-reset";
    public override string Title => "Password Reset Request";

    public string UserName { get; set; }
    public string ResetUrl { get; set; }
    public int? ExpiresInMinutes { get; set; } // Nullable for optional display
}