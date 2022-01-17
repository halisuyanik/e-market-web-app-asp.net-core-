using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace on_e_commerce.Models
{
    public class Uye
    {
        public int UyeId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz !")]
        [StringLength(255, ErrorMessage = "Şifreniz Min 8 karakter olmak zorundadır", MinimumLength = 8)]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Şifreler uyuşmuyor !")]
        [Compare("Sifre", ErrorMessage = "Şifreler birbirleriyle uyuşmuyor !")]
        public string ConfirmPassword { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> OlusturulmaTarihi { get; set; }
        public Nullable<System.DateTime> DegistirilmeTarihi { get; set; }
        public Nullable<int> yetkiid { get; set; }

    }
}