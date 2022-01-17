using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class TblUye
    {
        public TblUye()
        {
            TblSiparisDetays = new HashSet<TblSiparisDetay>();
            TblSiparisUrunleris = new HashSet<TblSiparisUrunleri>();
        }

        public int UyeId { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string Email { get; set; }
        public string Sifre { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
        public DateTime? DegistirilmeTarihi { get; set; }
        public int? Yetkiid { get; set; }
        public string Adres { get; set; }

        public virtual TblRoller Yetki { get; set; }
        public virtual ICollection<TblSiparisDetay> TblSiparisDetays { get; set; }
        public virtual ICollection<TblSiparisUrunleri> TblSiparisUrunleris { get; set; }
    }
}
