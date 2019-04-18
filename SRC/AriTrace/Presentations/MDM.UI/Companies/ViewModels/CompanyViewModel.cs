using MDM.UI.Addresses.Models;
using MDM.UI.Companies.Models;
using MDM.UI.CompanyTypes.Models;
using MDM.UI.Contacts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDM.UI.Companies.ViewModels
{
    public class CompanyViewModel : Company
    {
        public Contact Contact { set; get; }

        public Address Address { set; get; }

        public CompanyType CompanyType { set; get; }

        public bool IsChangedImage { get; set; }
        public string ImageData { get; set; }
    }
}
