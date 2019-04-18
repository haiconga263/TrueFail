using Distributions.UI.Models;
using MDM.UI.Employees.Models;
using MDM.UI.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Distributions.UI.ViewModels
{
    public class TripViewModel : Trip
    {
        public TripStatus Status { set; get; }
        public RouterViewModel Router { set; get; }
        public Employee DeliveryMan { set; get; }
        public Employee Driver { set; get; }
        public Vehicle Vehicle { set; get; }
    }
}
