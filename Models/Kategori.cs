using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace on_e_commerce.Models
{
    public class Kategori
    {
        public int KategoriId { get; set; }
        [Required(ErrorMessage = "Kategori adı girmek zorunludur.")]
        [StringLength(100, ErrorMessage = "Kategori adı Min 3 ve Max 100 karakter içermelidir.", MinimumLength = 3)]
        public string KategoriAdi { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    }
    public class Urun
    {
        public int UrunId { get; set; }
        [Required(ErrorMessage = "Ürün adı girmek zorunludur.")]
        [StringLength(100, ErrorMessage = "Ürün adı Min 3 ve Max 100 karakter içermelidir.", MinimumLength = 3)]
        public string UrunAdi { get; set; }
        [Required]
        public Nullable<int> KategoriId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> OlusturulmaTarihi { get; set; }
        public Nullable<System.DateTime> DegistirilmeTarihi { get; set; }
        [Required]
        [Range(typeof(decimal),"1","200000",ErrorMessage ="Geçersiz fiyat")]
        public Nullable<decimal> Fiyat { get; set; }
        [Required(ErrorMessage ="Ürün açıklaması yazmak zorunludur.")]
        public string Acıklama { get; set; }
        public string UrunResim { get; set; }
        public Nullable<bool> OneCıkan { get; set; }
        public Nullable<int> Miktar { get; set; }
        public Nullable<decimal> Agırlık { get; set; }
        public SelectList Kategoriler { get; set; }
    }
}