using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace on_e_commerce.Models
{
    public class Alisveris
    {
        public int SiparisDetayId { get; set; }
        [Required]
        public Nullable<int> UyeId { get; set; }
        [Required]
        public string Adres { get; set; }
        [Required]
        public string Sehir { get; set; }
        [Required]
        public string Ilce { get; set; }
        [Required]
        public string Ulke { get; set; }
        [Required]
        public string PostaKodu { get; set; }
        public Nullable<int> SiparisId { get; set; }
        public Nullable<decimal> OdenenMiktar { get; set; }
        [Required]
        public string OdemeTipi { get; set; }
    }
}