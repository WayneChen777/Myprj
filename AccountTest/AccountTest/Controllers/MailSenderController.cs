using AccountTest.Models.ViewModels;
using AccountTest.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountTest.Controllers
{
    public class MailSenderController : Controller
    {
        [HttpPost]
        public IActionResult SendMail([FromBody] RegisterViewModel mails)
        {
            //var mails = new string[] { "silcy1111@gmail.com" };
            var mailhelper = new Mailhelper();
            mailhelper.CreateMail(mails.Email, "標題", "<h1>辛辛苦苦寄信</h1>");
            mailhelper.Send();

            return Content("寄信");
        }
    }
}
