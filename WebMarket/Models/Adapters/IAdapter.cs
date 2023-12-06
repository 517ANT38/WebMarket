using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.Models.Adapters
{
    internal interface IAdapter<T>
    {

        IEnumerable<T> Select();
        T Get(int id);
        void Delete(int id);
        bool Insert(T item);
        void Update(T item);
        
    }
}
