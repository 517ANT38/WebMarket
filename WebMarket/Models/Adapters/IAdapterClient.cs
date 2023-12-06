using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.Models.Adapters
{
    internal interface IAdapterClient : IAdapter<Client>
    {
        Client Get(string email);
    }
}
