using Common.Interfaces;
using MDM.UI.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MDM.UI.Common.Interfaces
{
    public interface ICFShippingRepository : IBaseRepository
    {
        Task<int> Add(CFShipping shipping);
        Task<int> Update(CFShipping shipping);
        Task<int> Delete(CFShipping shipping);

        Task<int> AddItem(CFShippingItem shippingItem);
        Task<int> UpdateItem(CFShippingItem shippingItem);
        Task<int> DeleteItem(long cfShippingItemId);
        Task<int> DeleteItems(long cfShippingId);
    }
}
