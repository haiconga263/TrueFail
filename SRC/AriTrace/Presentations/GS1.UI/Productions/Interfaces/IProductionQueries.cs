using Common.Interfaces;
using GS1.UI.Productions.Models;
using GS1.UI.Productions.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GS1.UI.Productions.Interfaces
{
    public interface IProductionQueries : IBaseQueries
    {
        Task<ProductionInformation> GetByIdAsync(int id);

        Task<IEnumerable<ProductionInformation>> GetsAsync(int? partnerId = null);
        Task<IEnumerable<ProductionInformation>> GetAllAsync(int? partnerId = null);
    }
}
