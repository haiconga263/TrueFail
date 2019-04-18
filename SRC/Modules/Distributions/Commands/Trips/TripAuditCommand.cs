using Distributions.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Web.Controllers;

namespace Distributions.Commands.Trips
{
    public class TripAuditCommand : BaseCommand<int>
    {
        public IEnumerable<TripAudit> Audits { set; get; }
        public TripAuditCommand(IEnumerable<TripAudit> audits)
        {
            Audits = audits;
        }
    }
}
