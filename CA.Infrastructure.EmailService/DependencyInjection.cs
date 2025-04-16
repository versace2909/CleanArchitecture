using CA.Infrastructure.EmailService.Implementations;
using CA.Infrastructure.EmailService.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CA.Infrastructure.EmailService;

public static class DependencyInjection
{
    public static IServiceCollection AddEmailService(this IServiceCollection services)
    {
        services.AddScoped<IEmailTemplateService, HandlebarEmailTemplateService>();
        services.AddScoped<ISendMailService, SmtpSendMailService>();
        return services;
    }
}