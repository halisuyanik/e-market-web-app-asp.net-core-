using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace on_e_commerce.Models
{
    public class Register
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SurName { get; set; }
        [Required] 
        [EmailAddress(ErrorMessage = "Lütfen e-mail adresinizi giriniz: ex@abc.xyz")]
        public string Email { get; set; }
        [Required] 
        public string Password { get; set; }
        [Required] 
        [Compare("Password",ErrorMessage = "Lütfen şifrenizi doğrulayınız.")]
        public string RePassword { get; set; }
    }
}