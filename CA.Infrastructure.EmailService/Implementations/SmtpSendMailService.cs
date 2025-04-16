using System.Net;
using System.Net.Mail;
using CA.Infrastructure.EmailService.Interfaces;
using CA.Infrastructure.EmailService.Models;
using Microsoft.Extensions.Configuration;

namespace CA.Infrastructure.EmailService.Implementations;

public class SmtpSendMailService : ISendMailService
{
    private readonly IConfiguration _configuration;

    public SmtpSendMailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SendEmailAsync(SendEmailModel model)
    {
        var smtpSettings = _configuration.GetSection("SmtpSettings");
        var host = smtpSettings["Host"];
        var port = int.Parse(smtpSettings["Port"] ?? "587");
        var fromAddress = smtpSettings["FromAddress"];
        var password = smtpSettings["Password"];
        var enableSsl = bool.Parse(smtpSettings["EnableSsl"] ?? "true");

        if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(fromAddress) || string.IsNullOrEmpty(password))
        {
            throw new InvalidOperationException("SMTP settings are not configured properly in appsettings.json");
        }

        using var client = new SmtpClient(host, port);
        
        client.Credentials = new NetworkCredential(fromAddress, password);
        client.EnableSsl = enableSsl;

        var mailMessage = new MailMessage
        {
            From = new MailAddress(fromAddress!),
            Subject = model.Title,
            Body = model.EmailContent,
            IsBodyHtml = true, // Set based on your template type
        };
        foreach (var email in model.RecipientEmails)
        {
            mailMessage.To.Add(email);
        }
            
        foreach (var cc in model.CCs)
        {
            mailMessage.CC.Add(cc);
        }

        await client.SendMailAsync(mailMessage);
        return true;
    }
}