using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMarket.Models.Adapters
{
    internal interface IAdapterFind
    {
        AuthorizedUser findCheck(EmailPassword emailPassword);
    }
}
