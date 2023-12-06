using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.Models.Adapters
{
    internal interface IAdapterProducts:IAdapter<Product>
    {
        void Update(int id, int count);
        void Insert(NewProduct product);
    }
}
