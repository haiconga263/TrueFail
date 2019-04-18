using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Countries
{
    public class AddCommand : BaseCommand<int>
    {
        public Country Country { set; get; }
        public AddCommand(Country country)
        {
            Country = country;
        }
    }
}
