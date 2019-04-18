using Common.Interfaces;
using Fulfillments.UI.Models;
using Fulfillments.UI.ViewModels;
using MDM.UI.Collections.Models;
using MDM.UI.Fulfillments.ViewModels;
using MDM.UI.Products.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fulfillments.UI.Interfaces
{
    public interface IFulfillmentCollectionQueries : IBaseQueries
    {
        Task<IEnumerable<FulfillmentViewModel>> GetFulfillment();

        Task<IEnumerable<FulfillmentCollectionViewModel>> GetOrderFromCollection();

        Task<IEnumerable<FulfillmentCollectionViewModel>> GetOrderFromCollectionById(string id);
        Task<IEnumerable<Collection>> GetCollection();

        Task<IEnumerable<FCViewModel>> GetOrderFromCollectionByFcId(string id);

        Task<IEnumerable<FulfillmentCollectionStatus>> GetFulfillmentCollectionStatus();

        Task<IEnumerable<Product>> GetAllFCProduct();
    }
}
