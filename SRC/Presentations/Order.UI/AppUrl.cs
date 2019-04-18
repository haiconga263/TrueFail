using System;
using System.Collections.Generic;
using System.Text;

namespace Order.UI
{
    public class AppUrl
    {
        public const string GetEmployeeByUser = "api/employee/get/byuser";
        public const string GetRetailerByUser = "api/retailer/get/byuser";
        public const string GetFarmerByUser = "api/farmer/get/byuser";
        public const string GetRetailerById = "api/retailer/get";
        public const string GetProduct = "api/product/get";
        public const string GetDistributionsByUser = "api/distribution/gets/bysupervisor";
        public const string SyncToCollectionOrder = "/api/collection/purchase-order/sync-farmer-order";
        public const string SetCollectionInventory = "/api/collectioninventory/update";
    }
}
