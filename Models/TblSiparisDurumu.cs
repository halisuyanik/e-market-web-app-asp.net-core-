using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class TblSiparisDurumu
    {
        public TblSiparisDurumu()
        {
            TblSiparisDetays = new HashSet<TblSiparisDetay>();
        }

        public int Id { get; set; }
        public string SiparisDurumu { get; set; }

        public virtual ICollection<TblSiparisDetay> TblSiparisDetays { get; set; }
    }
}
