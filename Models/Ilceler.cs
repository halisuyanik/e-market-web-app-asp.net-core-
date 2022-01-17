using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class Ilceler
    {
        public Ilceler()
        {
            SemtMahs = new HashSet<SemtMah>();
        }

        public int ilceId { get; set; }
        public int SehirId { get; set; }
        public string IlceAdi { get; set; }
        public string SehirAdi { get; set; }

        public virtual Sehirler Sehir { get; set; }
        public virtual ICollection<SemtMah> SemtMahs { get; set; }
    }
}
