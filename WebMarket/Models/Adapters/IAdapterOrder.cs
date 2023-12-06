using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.Models.Adapters
{
    
    internal interface IAdapterOrder
    {
        int Insert(OrderWriteDb order);
        void InsertProductInOrder(IList<Product> products, int id_order);
        List<Order> Select();

    }
}
