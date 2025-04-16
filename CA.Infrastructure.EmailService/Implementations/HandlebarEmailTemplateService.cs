using System.Reflection;
using CA.Infrastructure.EmailService.Interfaces;
using CA.Infrastructure.EmailService.Models;
using HandlebarsDotNet;

namespace CA.Infrastructure.EmailService.Implementations;

public class HandlebarEmailTemplateService : IEmailTemplateService
{
    public async Task<string> RenderTemplateAsync<T>(T model) where T : BaseEmailModel
    {
        var executingAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (executingAssemblyLocation == null)
        {
            throw new InvalidOperationException("Could not determine executing assembly location.");
        }

        var templateFolderPath = Path.Combine(executingAssemblyLocation, "Templates");

        if (!Directory.Exists(templateFolderPath))
        {
            throw new InvalidOperationException($"Warning: Email template folder not found at {templateFolderPath}");
        }

        var templateName = model.TemplateName;

        var templateFilePath = Path.Combine(templateFolderPath, $"{templateName}.hbs");
        if (!File.Exists(templateFilePath))
        {
            templateFilePath = Path.Combine(templateFolderPath, $"{templateName}.html");
            if (!File.Exists(templateFilePath))
            {
                throw new FileNotFoundException(
                    $"Email template '{templateName}.hbs' or '{templateName}.html' not found in '{templateFolderPath}'.");
            }
        }

        // 2. Read the template content
        var templateContent = await File.ReadAllTextAsync(templateFilePath);

        // 3. Compile the template
        var compiledTemplate = Handlebars.Compile(templateContent);
        
        var emailContent = compiledTemplate(model);
        return emailContent;
    }
}