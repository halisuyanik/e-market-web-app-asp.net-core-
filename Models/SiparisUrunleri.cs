using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using on_e_commerce.data;

namespace on_e_commerce.Models
{
    public class SiparisUrunleri
    {
        public int SiparisId { get; set; }
        public Nullable<int> UrunId { get; set; }
        public Nullable<int> Adet { get; set; }
        public Nullable<int> UyeId { get; set; }
        public Nullable<int> SiparisDetayId { get; set; }

        public  Tbl_Urun Tbl_Urun { get; set; }
        public  Tbl_Uye Tbl_Uye { get; set; }
        public  Tbl_SiparisDetay Tbl_SiparisDetay { get; set; }
    }
}