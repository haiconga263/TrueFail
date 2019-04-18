using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;
using Web.Controls;

namespace Admin.Commands.GeoCommand.Provinces
{
    public class DeleteCommand : BaseCommand<int>
    {
        public int ProvinceId { set; get; }
        public DeleteCommand(int provinceId)
        {
            ProvinceId = provinceId;
        }
    }
}
