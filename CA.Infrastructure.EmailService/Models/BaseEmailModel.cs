namespace CA.Infrastructure.EmailService.Models;

public abstract class BaseEmailModel
{
    public abstract string TemplateName { get; }
    public abstract string Title { get; }
}