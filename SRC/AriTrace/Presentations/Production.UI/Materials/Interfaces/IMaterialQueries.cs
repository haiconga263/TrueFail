using Common.Interfaces;
using Production.UI.Materials.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Production.UI.Materials.Interfaces
{
    public interface IMaterialQueries : IBaseQueries
    {
        Task<Material> GetByIdAsync(int id);
        Task<IEnumerable<Material>> GetsAsync(int? partnerId = null);
        Task<IEnumerable<Material>> GetAllAsync(int? partnerId = null);
    }
}
