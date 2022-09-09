using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop4DVDS.Models
{
    public class Cart
    {
        public int prodid { get; set; }

        public string proname { get; set; }

        public int qty { get; set; }

        public int price { get; set; }

        public int bill { get; set; }

        public string Image { get; set; }
    }
}