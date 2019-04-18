using Common.Interfaces;
using Distributions.UI.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Distributions.UI.Interfaces
{
    public interface ITripRepository : IBaseRepository
    {
        #region Trip
        Task<int> Create(Trip trip);
        Task<int> Update(Trip trip);
        Task<int> Delete(Trip trip);
        #endregion

        #region TripAudit
        Task<int> CreateTripAudit(TripAudit audit);
        #endregion
    }
}
