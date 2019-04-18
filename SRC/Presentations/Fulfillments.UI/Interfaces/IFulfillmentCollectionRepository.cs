using Common.Interfaces;
using Fulfillments.UI.Models;
using Fulfillments.UI.ViewModels;
using MDM.UI.Fulfillments.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fulfillments.UI.Interfaces
{
    public interface IFulfillmentCollectionRepository : IBaseRepository
    {
        Task<bool> Update(FulfillmentCollection fulCol);
        Task<int> Add(FulfillmentCollection fulCol);
        //Task<bool> UpdateItems(FulfillmentCollectionViewModel fulColItem);
        Task<int> AddItems(FulfillmentCollectionItem fulCoItem);
        Task<int> PrintReceipt();
    }
}
