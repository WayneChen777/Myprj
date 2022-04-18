using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Mail;
using AccountTest.Models;
using MimeKit;
using MailKit.Net.Smtp;

namespace AccountTest.Controllers
{
    public class SendMailController : Controller
    {
        private readonly AppDb _appDb;
        public SendMailController(AppDb appDb)
        {
            _appDb = appDb;
        }
        public async Task< IActionResult> SendMailer()
        {
            EmailSender emailSender = new EmailSender();
            
            return Ok();
        }
    }
}
