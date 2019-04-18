using Common;
using Common.Exceptions;
using Distributions.UI;
using Distributions.UI.Interfaces;
using MDM.UI.Distributions.Interfaces;
using MDM.UI.Employees.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Controllers;
using Web.Helpers;

namespace Distributions.Commands.Trips
{
    public class TripAuditCommandHandler : BaseCommandHandler<TripAuditCommand, int>
    {
        private readonly ITripRepository tripRepository = null;
        public TripAuditCommandHandler(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }
        public override async Task<int> HandleCommand(TripAuditCommand request, CancellationToken cancellationToken)
        {
            if (request.Audits == null)
            {
                throw new BusinessException("AddWrongInformation");
            }

            foreach (var audit in request.Audits)
            {
                await tripRepository.CreateTripAudit(audit);
            }

            return 0;
        }
    }
}
