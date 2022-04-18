using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace AccountTest.Models
{
    public class EmailSender : ISender
    {
        void ISender.Sender(string email, string subject, string message)
        {
            var emailmessage = new MimeMessage();
            emailmessage.From.Add(new MailboxAddress("網站管理者", "silcy1111@gmail.com"));
            emailmessage.To.Add(new MailboxAddress("用戶", email));
            emailmessage.Subject = subject;  //Title
            emailmessage.Body = new TextPart("html") { Text = message };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("silcy1111@gmail.com", "15262830w");
                client.Send(emailmessage);
                client.Disconnect(true);
            }
        }
    }

    public interface ISender
    {
        void Sender(string email, string subject, string message);
    }
}
