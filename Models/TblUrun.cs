using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class TblUrun
    {
        public TblUrun()
        {
            TblSepets = new HashSet<TblSepet>();
            TblSiparisUrunleris = new HashSet<TblSiparisUrunleri>();
        }

        public int UrunId { get; set; }
        public string UrunAdi { get; set; }
        public int? KategoriId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
        public DateTime? DegistirilmeTarihi { get; set; }
        public decimal? Fiyat { get; set; }
        public string Acıklama { get; set; }
        public string UrunResim { get; set; }
        public bool? OneCıkan { get; set; }
        public int? Miktar { get; set; }
        public string Agırlık { get; set; }

        public virtual TblKategori Kategori { get; set; }
        public virtual ICollection<TblSepet> TblSepets { get; set; }
        public virtual ICollection<TblSiparisUrunleri> TblSiparisUrunleris { get; set; }
    }
}
