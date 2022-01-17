using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace on_e_commerce.Models
{
    public class AppUser:IdentityUser<Guid>
    {
        public List<UserAddress>  Adresler { get; set; }


        public string Ad { get; set; }
        public string Soyad { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? OlusturulmaTarihi { get; set; }
        public DateTime? DegistirilmeTarihi { get; set; }
        public virtual ICollection<TblSiparisDetay> TblSiparisDetays { get; set; }
        public virtual ICollection<TblSiparisUrunleri> TblSiparisUrunleris { get; set; }


    }
}
