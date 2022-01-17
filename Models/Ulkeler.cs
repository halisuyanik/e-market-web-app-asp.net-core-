using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class Ulkeler
    {
        public int UlkeId { get; set; }
        public string IkiliKod { get; set; }
        public string UcluKod { get; set; }
        public string UlkeAdi { get; set; }
        public string TelKodu { get; set; }
    }
}
