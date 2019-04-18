using MDM.UI.Vehicles.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Vehicles.ViewModels
{
    public class VehicleViewModel : Vehicle
    {
        public VehicleType Type { set; get; }

        //mapping
        public string ImageData { set; get; }
    }
}
