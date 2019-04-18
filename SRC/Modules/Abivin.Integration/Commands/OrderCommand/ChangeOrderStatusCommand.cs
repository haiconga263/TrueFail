using Abivin.Integration.UI;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Abivin.Integration.Commands.OrderCommand
{
    public class ChangeOrderStatusCommand : BaseCommand<int>
    {
        public string OrderCode { set; get; }
        public OrderStatuses Status { set; get; }
        public string Reason { set; get; }
        public string Key { set; get; }
        public string Sign { set; get; }
        public ChangeOrderStatusCommand(string orderCode, OrderStatuses status, string reason, string key, string sign)
        {
            OrderCode = orderCode;
            Status = status;
            Reason = reason;
            Key = key;
            Sign = sign;
        }
    }
}
