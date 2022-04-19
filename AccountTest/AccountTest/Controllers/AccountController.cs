using AccountTest.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using AccountTest.Services;
using AccountTest.Models.ViewModels;

namespace AccountTest.Controllers
{
    public class AccountController : Controller
    {
        private AppDb _appdb;
        public AccountController(AppDb  appDb)
        {
            _appdb = appDb;
        }
        public IActionResult LoginView()
        {
            return View();
        }
        public IActionResult RegisterView()
        {
            return View();
        }

        
        public bool Register([FromBody] Models.ViewModels.RegisterViewModel registerViewModel)
        {
            var userRegister = _appdb.Users.FirstOrDefault(x => x.Email == registerViewModel.Email);
            if (userRegister == null)
            {
                string password = registerViewModel.Password;
                byte[] pwGetbyte = Encoding.UTF8.GetBytes(password);
                byte[] pwHash = new SHA256Managed().ComputeHash(pwGetbyte);
                string Hashstr = Convert.ToBase64String(pwHash);

                registerViewModel.Password = Hashstr;

                _appdb.Users.Add(new Models.Data.User()
                {
                    Email = registerViewModel.Email,
                    Password = registerViewModel.Password
                });
                _appdb.SaveChanges();

                var mailhelper = new Mailhelper();
                mailhelper.CreateMail(registerViewModel.Email, "標題", "<h1>辛辛苦苦寄信</h1>");
                mailhelper.Send();
                return true;
            }
            else
            {
                return false;
            }
        }
        //[HttpPost]
        //public IActionResult SendMail([FromBody] RegisterViewModel mails)
        //{
        //    //var mails = new string[] { "silcy1111@gmail.com" };
        //    var mailhelper = new Mailhelper();
        //    mailhelper.CreateMail(mails.Email, "標題", "<h1>辛辛苦苦寄信</h1>");
        //    mailhelper.Send();

        //    return Content("寄信");
        //}

        [HttpPost]
        public async Task<IActionResult> LoginAsync([FromBody]Models.ViewModels.LoginViewModel loginViewModel)
        {
            string password = loginViewModel.Password;
            byte[] loginGetbyte = Encoding.UTF8.GetBytes(password);
            byte[] loginHash = new SHA256Managed().ComputeHash(loginGetbyte);
            string loginHashstr = Convert.ToBase64String(loginHash);

            loginViewModel.Password = loginHashstr;

            var userLogin = _appdb.Users.FirstOrDefault(x => x.Email == loginViewModel.Email && x.Password == loginViewModel.Password);
            if (userLogin == null)
            {
                return BadRequest(400);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userLogin.Email),
                new Claim("User_Id", userLogin.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(claimsPrincipal);
            return Ok(200);
        }

        [Authorize(AuthenticationSchemes = "Cookies")]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
