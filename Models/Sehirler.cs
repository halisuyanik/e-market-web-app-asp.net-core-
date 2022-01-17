using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class Sehirler
    {
        public Sehirler()
        {
            Ilcelers = new HashSet<Ilceler>();
        }

        public int SehirId { get; set; }
        public string SehirAdi { get; set; }
        public byte PlakaNo { get; set; }
        public int TelefonKodu { get; set; }
        public int RowNumber { get; set; }

        public virtual ICollection<Ilceler> Ilcelers { get; set; }
    }
}
