using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace on_e_commerce.Models
{
    public class LoginViewModel
    {
        

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Persistent { get; set; }
        public bool Lock { get; set; }
    }
}
