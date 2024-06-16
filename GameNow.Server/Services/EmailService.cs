using System.Net;
using System.Net.Mail;
using System.Reflection;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GameNow.Server.Services;

public class EmailService : IEmailSender
{
    private IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string username, string to, string callbackUrl)
    {

        MailMessage msg = new MailMessage();
        msg.Subject = "Account Confirmation Email";
        msg.From = new MailAddress(_configuration["EmailCredentials:Sender"]!, "GameNow");
        msg.To.Add(to);
        msg.IsBodyHtml = true;
        msg.Body = GetHtml(username, callbackUrl);

        using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
        {
            smtp.Credentials = new NetworkCredential(_configuration["EmailCredentials:Sender"], _configuration["EmailCredentials:Password"]);
            smtp.EnableSsl = true;
            smtp.Send(msg);
        }

        await Task.Delay(1);
    }

    string GetHtml(string userName, string callbackUrl)
    {
        string? template;
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("GameNow.Server.emailTemplate.html");

        using (StreamReader reader = new StreamReader(stream!))
        {
            template = reader.ReadToEnd();
        }

        template = template.Replace("{USER_NAME}", userName);
        template = template.Replace("{CONFIRMATION_URL}", callbackUrl);

        return template;
    }
}
