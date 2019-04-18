using System;
using System.Collections.Generic;
using System.Text;

namespace Distributions.UI
{
    public class AppUrl
    {
        public const string GetEmployeeByUser = "api/employee/get/byuser";
        public const string GetEmployee = "api/employee/get";
        public const string GetVehicle = "api/vehicle/get";
        public const string GetRetailerOrders = "api/retailerorder/gets/bydelivery";
        public const string GetRetailerOrder = "api/retailerorder/get";
        public const string GetOrderHistorys = "api/retailerorder/gets/completed/bydelivery";

        public const string UpdateRetailerOrderStatus = "api/retailerorder/update/status";
    }
}
