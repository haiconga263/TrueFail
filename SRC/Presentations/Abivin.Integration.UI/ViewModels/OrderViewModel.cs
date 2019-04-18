using System;
using System.Collections.Generic;
using System.Text;

namespace Abivin.Integration.UI.ViewModels
{
    public class OrderViewModel
    {
        public string OrderDate { set; get; }
        public string OrderCode { set; get; }
        public string OrderType { set; get; }
        public string PartnerCode { set; get; }
        public string ProductCode { set; get; }
        public int NumberOfCases { set; get; }
        public int NumberOfItems { set; get; }
        public decimal TotalPrice { set; get; }
        public double CustomerDiscount { set; get; }
        public double SaleDiscount { set; get; }
        public double PromotionDiscount { set; get; }
        public double IMVDDiscount { set; get; }
        public short PickupOrder { set; get; }
        public int ServiceTime { set; get; }
        public bool Splitted { set; get; }
        public string TimeWindow { set; get; }
        public string LotNumber { set; get; }
        public string ExpiredDate { set; get; }
    }
}
