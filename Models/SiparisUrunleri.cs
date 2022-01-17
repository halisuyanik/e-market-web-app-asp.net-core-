using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using on_e_commerce.Models;

namespace on_e_commerce.Models
{
    public class SiparisUrunleri
    {
        public int SiparisId { get; set; }
        public int UrunId { get; set; }
        public int Adet { get; set; }
        public string UyeId { get; set; }
        public int SiparisDetayId { get; set; }
        public string SiparisBarkod { get; set; }

        public  TblUrun Tbl_Urun { get; set; }
        public  TblUye Tbl_Uye { get; set; }
    }
}