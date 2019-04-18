using Common.Interfaces;
using MDM.UI.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Common.Interfaces
{
    public interface ICFShippingQueries : IBaseQueries
    {
        Task<string> GenarateCode();
        Task<CFShipping> Get(long shippingId);
        Task<IEnumerable<CFShipping>> Gets(string condition = "");

        Task<IEnumerable<CFShippingItem>> GetItems(long shippingId);
        Task<IEnumerable<CFShippingItem>> GetItems(string condition = "");
        Task<CFShippingItem> GetItem(long shippingItemId);
    }
}
