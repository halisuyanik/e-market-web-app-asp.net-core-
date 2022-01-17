using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class TblSiparisDetay
    {
        public int SiparisDetayId { get; set; }
        public string UyeId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Ilce { get; set; }
        public string Ulke { get; set; }
        public string PostaKodu { get; set; }
        public string TelefonNo { get; set; }
        public string Email { get; set; }
        public DateTime? SiparisTarihi { get; set; }
        public string OdemeTipi { get; set; }
        public int? ToplamFiyat { get; set; }
        public int? SiparisDurumu { get; set; }
        public string SiparisBarkod { get; set; }


        public virtual AppUser User { get; set; }
        public virtual TblSiparisDurumu SiparisDurumuNavigation { get; set; }
    }
}
