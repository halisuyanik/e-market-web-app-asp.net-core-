﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using on_e_commerce.Models;

namespace on_e_commerce.Models
{
    public class Item
    {
        public TblUrun Urun { get; set; }
        public int UrunId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Count { get; set; }

    }
}