using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Countries
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int CountryId { set; get; }
        public DeleteCommand(int countryId)
        {
            CountryId = countryId;
        }
    }
}
