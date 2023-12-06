using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMarket.Models.Adapters
{
    internal interface IAdapterBasketProduct
    {
        IEnumerable<Product> Select(string email);
        void Insert(ProductBasket productBasket);
        void Insert(IList<ProductBasket> productBaskets);
        void InsertSafe(params ProductBasket[] products);
        void UpdateCount(int id, string email, int count);
        void Delete(string email);
        void Delete(string email, int id);
    }
}