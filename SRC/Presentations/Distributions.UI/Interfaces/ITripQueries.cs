using Common.Interfaces;
using Distributions.UI.Models;
using Distributions.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Distributions.UI.Interfaces
{
    public interface ITripQueries : IBaseQueries
    {
        Task<string> GenarateCode();
        Task<IEnumerable<TripViewModel>> Gets(string conditions);
        Task<IEnumerable<TripViewModel>> GetHistorys(string conditions);
        Task<TripViewModel> Get(int id);
        Task<IEnumerable<TripViewModel>> GetByDeliveryMan(int id);
        Task<IEnumerable<TripViewModel>> GetByDriver(int id);

        Task<IEnumerable<TripStatus>> GetStatuses();
    }
}
