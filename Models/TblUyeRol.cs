using System;
using System.Collections.Generic;

#nullable disable

namespace on_e_commerce.Models
{
    public partial class TblUyeRol
    {
        public int UyeRolId { get; set; }
        public int? UyeId { get; set; }
        public int? RolId { get; set; }
    }
}
