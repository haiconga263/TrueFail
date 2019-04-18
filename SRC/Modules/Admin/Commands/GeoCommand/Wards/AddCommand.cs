using MDM.UI.Employees.Models;
using MDM.UI.Employees.ViewModels;
using MDM.UI.Geographical.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Admin.Commands.GeoCommand.Wards
{
    public class AddCommand : BaseCommand<int>
    {
        public Ward Ward { set; get; }
        public AddCommand(Ward ward)
        {
            Ward = ward;
        }
    }
}
