using Common.Interfaces;
using GS1.UI.Processes.Models;
using GS1.UI.Processes.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GS1.UI.Processes.Interfaces
{
    public interface IProcessQueries : IBaseQueries
    {
        Task<ProcessInformation> GetByIdAsync(int id);

        Task<IEnumerable<ProcessInformation>> GetsAsync(int? partnerId = null);
        Task<IEnumerable<ProcessInformation>> GetAllAsync(int? partnerId = null);
    }
}
