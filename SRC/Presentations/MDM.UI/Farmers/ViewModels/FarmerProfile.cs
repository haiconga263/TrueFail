using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Farmers.ViewModels
{
    public class FarmerProfile
    {
        public int FarmerId { get; set; }

        public string ImageURL { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }

        public int CountryId { get; set; }

        public int ProvinceId { get; set; }

        public int DistrictId { get; set; }

        public int WardId { get; set; }

        public string Street { get; set; }
    }
}
