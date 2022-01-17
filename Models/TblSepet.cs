using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class TblSepet
    {
        public int SepetId { get; set; }
        public int? UrunId { get; set; }
        public int? UyeId { get; set; }
        public int? SepetDurumuId { get; set; }
        public int? SiparisMiktari { get; set; }

        public virtual TblUrun Urun { get; set; }
    }
}
