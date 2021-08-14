using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using on_e_commerce.data;

namespace on_e_commerce.Models
{
    public class ItemSehir
    {
        public int SehirId { get; set; }
        public string SehirAdi { get; set; }
        public byte PlakaNo { get; set; }
        public int TelefonKodu { get; set; }
        public int RowNumber { get; set; }
    }
}