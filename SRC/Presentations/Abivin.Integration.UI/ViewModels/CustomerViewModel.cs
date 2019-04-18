using System;
using System.Collections.Generic;
using System.Text;

namespace Abivin.Integration.UI.ViewModels
{
    public class CustomerViewModel
    {
        public string OrganizationCode { set; get; }
        public string PartnerCode { set; get; }
        public string PartnerName { set; get; }
        public string PartnerGroupCode { set; get; }
        public string Title { set; get; }
        public string PhoneNumber { set; get; }
        public string City { set; get; }
        public string Address { set; get; }
        public double Latitude { set; get; }
        public double Longitude { set; get; }
        public string Email { set; get; }
        public string OpenTime { set; get; }
        public string CloseTime { set; get; }
        public string LunchTime { set; get; }
        public string MinTime { set; get; }
        public string MaxTime { set; get; }
        public string TimeWindow { set; get; }
        public bool BikeOnly { set; get; }
        public bool TruckOnly { set; get; }
        public string SalesCode { set; get; }
        public string SerialNumber { set; get; }
        public string Comment { set; get; }
    }
}
