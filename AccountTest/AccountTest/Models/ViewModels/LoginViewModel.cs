using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountTest.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "請輸入正確Email格式")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
