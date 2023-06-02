using System.Net.Mail;
using System.Net;
using To_Do.API.Helpers;

namespace To_Do.Services;

public interface IEmailSender
{
    Task SendEmailAsync(string emailAddr, string subject, string htmlMessage);
}

public class EmailSender : IEmailSender
{
    private readonly IConfiguration configuration;

    public EmailSender(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public Task SendEmailAsync(string emailAddr, string subject, string htmlMessage)
    {
        var email = configuration[Constants.SENDER_EMAIL];
        var key = configuration[Constants.SENDER_KEY];

        SmtpClient client = new SmtpClient
        {
            Port = 587,
            Host = "smtp.gmail.com", //or another email sender provider
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(email, key)
        };

        return client.SendMailAsync(email, emailAddr, subject, htmlMessage);
    }
}