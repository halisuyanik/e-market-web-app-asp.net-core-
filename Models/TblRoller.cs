using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class TblRoller
    {
        public TblRoller()
        {
            TblUyes = new HashSet<TblUye>();
        }

        public int RolId { get; set; }
        public string RolAdi { get; set; }

        public virtual ICollection<TblUye> TblUyes { get; set; }
    }
}
