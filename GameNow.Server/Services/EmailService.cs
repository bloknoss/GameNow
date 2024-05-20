using System.Net;
using System.Net.Mail;
using System.Reflection;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace GameNow.Server.Services;

public class EmailService : IEmailSender
{
    private IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task SendEmailAsync(string username, string to, string callbackUrl)
    {
        SmtpClient client = new SmtpClient
        {
            Port = 587,
            Host = "smtp.gmail.com", //or another email sender provider
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(_configuration["EmailCredentials:Sender"], _configuration["EmailCredentials:Password"])
        };

        MailMessage msg = new MailMessage();
        msg.Subject = "Account Confirmation Email";
        msg.From = new MailAddress("gamenow@sender.com", "GameNow");
        msg.To.Add(to);
        msg.IsBodyHtml = true;
        msg.Body = GetHtml(username, callbackUrl);

        return client.SendMailAsync(msg);
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