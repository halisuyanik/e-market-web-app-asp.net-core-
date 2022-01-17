using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class SemtMah
    {
        public int SemtMahId { get; set; }
        public string SemtAdi { get; set; }
        public string MahalleAdi { get; set; }
        public string PostaKodu { get; set; }
        public int IlceId { get; set; }

        public virtual Ilceler Ilce { get; set; }
    }
}
