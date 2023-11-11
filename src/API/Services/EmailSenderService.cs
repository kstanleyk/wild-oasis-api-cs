using System;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace WildOasis.API.Services;

public class EmailSenderService : IEmailSenderService
{
    public async Task SendAsync(string email, string messageBody, string subject)
    {
        const string apiKey = "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";
        const string apiKeyEmail = "no-reply@dama.com";

        var client = new SendGridClient(apiKey);

        var from = new EmailAddress(apiKeyEmail, "dama");

        var to = new EmailAddress(email, email);

        var plainTextContent = messageBody;

        var htmlContent = messageBody;

        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

        try
        {
            await client.SendEmailAsync(msg);
        }
        catch (Exception)
        {
            // ignored
        }
    }
}