using Order.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Order.Commands.RetailerOrder
{
    public class ProcessCommand : BaseCommand<int>
    {
        public RetailerOrderProcessingViewModel Processing { set; get; }
        public ProcessCommand(RetailerOrderProcessingViewModel processing)
        {
            Processing = processing;
        }
    }
}
