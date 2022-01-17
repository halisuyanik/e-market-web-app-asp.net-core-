using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace on_e_commerce.Models
{
    public class RegisterViewModel
    {
            [Required]
            public string Name { get; set; }
            [Required]
            public string Surname { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Lütfen min 6 karakterli güçlü bir şifre oluşturun.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Şifreler birbirleriyle uyuşmuyor.")]
            public string ConfirmPassword { get; set; }
    }
}
