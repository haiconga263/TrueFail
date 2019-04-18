using System;
using System.Collections.Generic;
using System.Text;

namespace Abivin.Integration.UI.ViewModels
{
    public class Organization
    {
        public string Code { set; get; }
        public string Name { set; get; }
        public string ParentCode { set; get; }
        public string PhoneNumber { set; get; }
        public string Address { set; get; }
        public double Longitude { set; get; }
        public double Latitude { set; get; }
        public string OpenTime { set; get; }
        public string CloseTime { set; get; }
        public string MinTime { set; get; }
        public string MaxTime { set; get; }
    }
}
