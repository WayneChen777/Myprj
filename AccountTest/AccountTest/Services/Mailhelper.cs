using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace AccountTest.Services
{
    public class Mailhelper
    {
        private MailMessage mailMessage;
        private readonly SmtpClient client;

        public Mailhelper()
        {
            client = new SmtpClient("smtp.gmail.com", 587);
        }

        public Mailhelper(MailMessage mail)
        {
            client = new SmtpClient("smtp.gmail.com", 587);
            this.mailMessage = mail;
        }

        public bool Send()
        {
            client.Credentials = new NetworkCredential("silcy1111@gmail.com", "mxbillqqekknssna"); //發送信箱的認證
            client.EnableSsl = true; //Secure Sockets Layer (SSL) 加密連線
            client.Send(mailMessage); //寄出
            return true;
        }

        public void CreateMail(string to, string subject, string body)
        {
            var mail = new MailMessage();
            mail.IsBodyHtml = true; //true=訊息主體是html格式 
            mail.From = new MailAddress("silcy1111@gmail.com"); //發送者信箱
            //foreach(var item in to)
            //{
            //    mail.To.Add(item);
            //}
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            this.mailMessage = mail;
        }

        internal void CreateMail(string email, string v, object body)
        {
            throw new NotImplementedException();
        }
    }
}
