using System.Net.Mail;
using System.Net;

namespace To_Do.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string emailAddr, string subject, string htmlMessage);
}

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string emailAddr, string subject, string htmlMessage)
    {
        SmtpClient client = new SmtpClient
        {
            Port = 587,
            Host = "smtp.gmail.com", //or another email sender provider
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("your email sender", "password")
        };

        return client.SendMailAsync("your email sender", emailAddr, subject, htmlMessage);
    }
}