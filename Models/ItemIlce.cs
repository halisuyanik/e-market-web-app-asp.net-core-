using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using on_e_commerce.data;

namespace on_e_commerce.Models
{
    public class ItemIlce
    {
        public SemtMah SemtMah { get; set; }
        public int ilceId { get; set; }
        public int SehirId { get; set; }
        public string IlceAdi { get; set; }
        public string SehirAdi { get; set; }
    }
}