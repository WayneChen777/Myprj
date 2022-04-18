using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using AccountTest.Models;
using MimeKit;
using MailKit.Net.Smtp;
using AccountTest.Services;

namespace AccountTest.Controllers
{
    public class SendMailController : Controller
    {
        public ActionResult SendMail()
        {
            var mails = new string[] { "silcy1111@gmail.com" };
            var mailhelper = new Mailhelper();
            mailhelper.CreateMail(mails, "標題", "<h1>辛辛苦苦寄信</h1>");
            mailhelper.Send();

            return Content("寄信");
        }
    }
}
