using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class TblSiparisUrunleri
    {
        public int SiparisId { get; set; }
        public int? UrunId { get; set; }
        public int? Adet { get; set; }
        public string UyeId { get; set; }
        public int? SiparisDetayId { get; set; }
        public string SiparisBarkod { get; set; }

        public virtual AppUser User { get; set; }
        public virtual TblUrun Urun { get; set; }
    }
}
