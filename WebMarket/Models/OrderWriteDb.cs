using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMarket.Models
{
    public class OrderWriteDb
    {
        public OrderWriteDb() { }
        public int Id_client { get; set; }
        public DateTime DateOrder { get; set; }
        public DateTime DateDelivery { get; set; }
        public string MethodOfIssue { get; set; }
        
    }
}