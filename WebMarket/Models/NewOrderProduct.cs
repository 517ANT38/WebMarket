using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMarket.Models
{
    public class NewOrderProduct
    {
        public int Id_order { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
    }
}