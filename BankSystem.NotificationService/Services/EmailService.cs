using BankSystem.NotificationService.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Configuration;


namespace BankSystem.NotificationService.Services
{
    public class EmailService
    {
        public static void SendEmail(Notifications model)
        {

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Prince Bank", "prince.uluka@icloud.com"));
            message.To.Add(new MailboxAddress("",model.To));
            message.Subject = model.Subject;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = model.Body
            };

            message.Body = bodyBuilder.ToMessageBody();

            using(var client = new SmtpClient())
            {
                client.Connect("smtp.mail.me.com", 465,true);
                client.Authenticate("prince.uluka@icloud.com","Amarachi1965$");
                client.Send(message);

                client.Disconnect(true);
            }

        }
    }
}
