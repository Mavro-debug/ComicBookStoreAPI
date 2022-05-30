using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;


namespace ComicBookStoreAPI.Services
{
    public class EmailService
    {
        private  readonly SmtpClient _smtpClient;
        private readonly IConfiguration _config;
        public EmailService(IConfiguration configuration)
        {
            _config = configuration;

            _smtpClient = new SmtpClient("smtp.gmail.com", 587);

            _smtpClient.Credentials = new NetworkCredential()
            {
                UserName = _config.GetSection("SmtpClient").GetSection("UserName").Value,
                Password = _config.GetSection("SmtpClient").GetSection("Password").Value
            };

            _smtpClient.EnableSsl = true;
        }

        public void Send(string subject, string body, string destynationEmail)
        {
            MailMessage mailMessage = new MailMessage(_config.GetSection("SmtpClient").GetSection("Email").Value, destynationEmail);

            mailMessage.Subject = subject;

            mailMessage.Body = body;

            _smtpClient.Send(mailMessage);
        }
    }
}
