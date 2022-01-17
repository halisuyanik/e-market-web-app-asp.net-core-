using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace on_e_commerce.Models
{
    public class AppRole:IdentityRole<Guid>
    {
        public string Aciklama { get; set; }
    }
}
