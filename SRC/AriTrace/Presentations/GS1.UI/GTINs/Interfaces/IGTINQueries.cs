using Common.Interfaces;
using GS1.UI.GTINs.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GS1.UI.GTINs.Interfaces
{
    public interface IGTINQueries : IBaseQueries
    {
        Task<GTIN> GetByIdAsync(int id);
        Task<IEnumerable<GTIN>> GetsAsync();
        Task<IEnumerable<GTIN>> GetAllAsync();
        Task<IEnumerable<GTIN>> GetByCompanyCodeAsync(int gS1Code);
    }
}
