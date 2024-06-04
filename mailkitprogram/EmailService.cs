using MailKit.Net.Smtp;
using MimeKit;
using System.Configuration;
using System.Threading.Tasks;

namespace mailkitprogram
{
    public class EmailService
    {
       
            private readonly string _smtpServer;
            private readonly int _port;
            private readonly string _userName;
            private readonly string _password;

        public EmailService()
        {
            _smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
            _port = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            _userName = ConfigurationManager.AppSettings["SmtpUserName"];
            _password = ConfigurationManager.AppSettings["SmtpPassword"];
        }

        public async Task SendEmailAsync(string from, string to, string subject, string body)
            {
                var client = new SmtpClient();
                client.Connect(_smtpServer, _port, true);
                client.Authenticate(_userName, _password);
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(from, "Sender Name")); // Use the "from" parameter here
                message.To.Add(new MailboxAddress(to, ""));
                message.Subject = subject;
                message.Body = new TextPart("plain") { Text = body };

                await client.SendAsync(message);
            }
        }

}