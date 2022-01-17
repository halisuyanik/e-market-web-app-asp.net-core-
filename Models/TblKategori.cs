using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class TblKategori
    {
        public TblKategori()
        {
            TblUruns = new HashSet<TblUrun>();
        }

        public int KategoriId { get; set; }
        public string KategoriAdi { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public string KategoriResim { get; set; }

        public virtual ICollection<TblUrun> TblUruns { get; set; }
    }
}
